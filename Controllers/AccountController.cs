using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppManager.Models;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Controllers
{
    public class AccountController(IUnitOfWork unitOfWork) : Controller
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly HashType pwdHashType = HashType.SHA512;

        #endregion Private Fields



        #region Public Methods

        /// <summary>
        /// View đăng nhập
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(View);
        }

        /// <summary>
        /// Đăng xuất ứng dụng
        /// </summary>
        /// <returns> Xoá thông tin đăng nhập </returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return await Task.Run(() => RedirectToAction("Index", "Home"));
        }

        /// <summary>
        /// Đăng nhập ứng dụng
        /// </summary>
        /// <param name="userName"> Tên đăng nhập </param>
        /// <param name="userPass"> Mật khẩu </param>
        /// <returns> Kết quả đăng nhập </returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ThongTinTaiKhoanDto taiKhoan)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid || string.IsNullOrEmpty(taiKhoan.UserName) || string.IsNullOrEmpty(taiKhoan.UserPass))
                return await Task.Run(() => BadRequest(CommonMessages.ParamsIsNullOrEmpty));

            return await VerifyAccountAsync(taiKhoan);
        }

        #endregion Public Methods



        #region Private Methods

        /// <summary>
        /// Tạo hoặc cập nhật tài khoản đăng nhập
        /// </summary>
        /// <param name="userName"> Tên tài khoản cần tạo hoặc cập nhật </param>
        /// <param name="userPass"> Mật khẩu đăng nhập </param>
        /// <returns> </returns>
        [Authorize]
        private async Task<IActionResult> CreateOrUpdateAccountAsync(ThongTinTaiKhoanDto taiKhoan)
        {
            // Xử lý dữ liệu trước khi nấu
            if (!ModelState.IsValid || string.IsNullOrEmpty(taiKhoan.UserName) || string.IsNullOrEmpty(taiKhoan.UserPass))
                return await Task.Run(() => BadRequest(CommonMessages.ParamsIsNullOrEmpty));
            taiKhoan.UserName = taiKhoan.UserName.Trim().ToUpper();
            taiKhoan.UserPass = taiKhoan.UserPass.Trim();

            // Tìm xem tài khoản đã tồn tại hay chưa
            DsTaikhoan? record = await _unitOfWork.GetRepository<DsTaikhoan>().GetDataAsync(r => r.TenDangNhap == taiKhoan.UserName);

            await _unitOfWork.BeginTransactionAsync();
            if (record is null)
            {
                // Nếu chưa có tài khoản thì tạo mới
                await _unitOfWork.GetRepository<DsTaikhoan>().CreateAsync(new DsTaikhoan
                {
                    TenDangNhap = taiKhoan.UserName,
                    MatKhau = BCrypt.Net.BCrypt.EnhancedHashPassword(taiKhoan.UserPass, pwdHashType), // Tạo mật khẩu đã mã hoá cùng salt
                    HashSalt = BCrypt.Net.BCrypt.GenerateSalt(), // Không cần sử dụng nữa do thư viện tự lấy rồi. Thêm vào cho có thôi hoặc để lừa mấy thằng hắc cơ lỏd 🤣
                    IsAble = true,
                    UpdatedAt = DateTime.UtcNow,
                });
            }
            else
            {
                // Cập nhật lại mật khẩu
                record.MatKhau = BCrypt.Net.BCrypt.EnhancedHashPassword(taiKhoan.UserPass, pwdHashType); // Tạo mật khẩu đã mã hoá cùng salt
                record.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.GetRepository<DsTaikhoan>().UpdateAsync(record);
            }

            // Lưu lại vào CSDL
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return await Task.Run(() => Created(string.Empty, record));
        }

        /// <summary>
        /// Kiểm tra tài khoản đăng nhập ứng dụng
        /// </summary>
        /// <param name="taiKhoan"> Thông tin tài khoản đăng nhập </param>
        /// <returns> Kết quả kiểm tra: True nếu tài khoản hợp lệ </returns>
        private async Task<IActionResult> VerifyAccountAsync(ThongTinTaiKhoanDto taiKhoan)
        {
            // Tạo trước một số thông tin người dùng
            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, taiKhoan.UserName),
                };

            // Xử lý chuỗi thô
            taiKhoan.UserName = taiKhoan.UserName.Trim().ToUpper();
            taiKhoan.UserPass = taiKhoan.UserPass.Trim();

            // Lấy thông tin tài khoản trong CSDL
            DsTaikhoan? record = await _unitOfWork.GetRepository<DsTaikhoan>().GetDataAsync(r => r.IsAble && r.TenDangNhap == taiKhoan.UserName);
            if (record is null)
                return await Task.Run(() => Unauthorized(CommonMessages.AccountUnauthorized));

            // So sánh mật khẩu vừa nhập với dữ liệu từ CSDL
            if (!BCrypt.Net.BCrypt.EnhancedVerify(taiKhoan.UserPass, record.MatKhau, pwdHashType))
                return await Task.Run(() => Unauthorized(CommonMessages.AccountUnauthorized));

            // Đăng nhập thành công, ghi lại các thông tin người dùng
            claims.Add(new Claim("LoggedInAt", DateTime.Now.ToString()));

            // Gán thông tin người dùng vào cookie
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Thiết lập thuộc tính
            AuthenticationProperties authProperties = new AuthenticationProperties();

            // Tạo phiên đăng nhập
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Chuyển hướng trang về trang trước đó hoặc home
            return await Task.Run(() => Ok(string.IsNullOrEmpty(taiKhoan.LastUrl) ? "/" : taiKhoan.LastUrl));
        }

        #endregion Private Methods
    }
}
