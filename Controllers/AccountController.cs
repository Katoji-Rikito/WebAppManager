using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppManager.Models;
using WebAppManager.Models.DTOs;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Controllers
{
    public class AccountController(IUnitOfWork unitOfWork) : Controller
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly HashType pwdHashType = HashType.SHA512;
        private readonly string MSG_AccountUnauthorized = "Thông tin đăng nhập không hợp lệ";

        #endregion Private Fields
        #region Public Constructors

        #endregion Public Constructors



        #region Public Methods
        public async Task<IActionResult> Index()
        {
            return await Task.Run(View);
        }

        /// <summary>
        /// Đăng xuất ứng dụng
        /// </summary>
        /// <returns>Xoá thông tin đăng nhập</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return await Task.Run(() => RedirectToAction("Index", "Account"));
        }

        /// <summary>
        /// Đăng nhập ứng dụng
        /// </summary>
        /// <param name="userName">Tên đăng nhập</param>
        /// <param name="userPass">Mật khẩu</param>
        /// <returns>Kết quả đăng nhập</returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ThongTinTaiKhoanDto taiKhoan)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid || string.IsNullOrEmpty(taiKhoan.UserName) || string.IsNullOrEmpty(taiKhoan.UserPass))
                return await Task.Run(() => BadRequest(CommonMessages.ParamsIsNullOrEmpty));

            return await VerifyAccount(taiKhoan);
        }

        #endregion Public Methods



        #region Private Methods
        /// <summary>
        /// Tạo hoặc cập nhật tài khoản đăng nhập
        /// </summary>
        /// <param name="userName">Tên tài khoản cần tạo hoặc cập nhật</param>
        /// <param name="userPass">Mật khẩu đăng nhập</param>
        /// <returns></returns>
        [Authorize]
        private async Task CreateOrUpdateAccount(ThongTinTaiKhoanDto taiKhoan)
        {
            // Xử lý dữ liệu trước khi nấu
            if (!ModelState.IsValid || string.IsNullOrEmpty(taiKhoan.UserName) || string.IsNullOrEmpty(taiKhoan.UserPass))
                throw new Exception(CommonMessages.ParamsIsNullOrEmpty);
            taiKhoan.UserName = taiKhoan.UserName.Trim().ToUpper();
            taiKhoan.UserPass = taiKhoan.UserPass.Trim();

            // Tìm xem tài khoản đã tồn tại hay chưa
            DsTaikhoan? record = await _unitOfWork.GetRepository<DsTaikhoan>().GetDataAsync(r => r.TenDangNhap == taiKhoan.UserName);

            await _unitOfWork.BeginTransactionAsync();
            if (record is null)
            {
                // Nếu chưa có tài khoản thì tạo mới
                record = new DsTaikhoan
                {
                    TenDangNhap = taiKhoan.UserName,
                    MatKhau = BCrypt.Net.BCrypt.EnhancedHashPassword(taiKhoan.UserPass, pwdHashType), // Tạo mật khẩu đã mã hoá cùng salt
                    HashSalt = BCrypt.Net.BCrypt.GenerateSalt(), // Không cần sử dụng nữa do thư viện tự lấy rồi. Thêm vào cho có thôi hoặc để lừa mấy thằng hắc cơ lỏd 🤣
                    IsAble = true,
                    UpdatedAt = DateTime.UtcNow,
                };
                await _unitOfWork.GetRepository<DsTaikhoan>().CreateAsync(record);
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
        }

        /// <summary>
        /// Kiểm tra tài khoản đăng nhập ứng dụng
        /// </summary>
        /// <param name="taiKhoan">Thông tin tài khoản đăng nhập</param>
        /// <returns>Kết quả kiểm tra: True nếu tài khoản hợp lệ</returns>
        private async Task<IActionResult> VerifyAccount(ThongTinTaiKhoanDto taiKhoan)
        {
            // Xử lý chuỗi thô
            taiKhoan.UserName = taiKhoan.UserName.Trim().ToUpper();
            taiKhoan.UserPass = taiKhoan.UserPass.Trim();

            try
            {
                // Lấy thông tin tài khoản trong CSDL
                DsTaikhoan? record = await _unitOfWork.GetRepository<DsTaikhoan>().GetDataAsync(r => r.IsAble && r.TenDangNhap == taiKhoan.UserName);
                if (record is null) throw new Exception();

                // So sánh mật khẩu vừa nhập với dữ liệu từ CSDL
                if (!BCrypt.Net.BCrypt.EnhancedVerify(taiKhoan.UserPass, record.MatKhau, pwdHashType)) throw new Exception();

                // Đăng nhập thành công
                // Tạo các thông tin người dùng
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, taiKhoan.UserName),
                    new Claim("LoggedInAt", DateTime.Now.ToString()),
                };

                // Gán thông tin người dùng vào cookie
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Thiết lập thuộc tính
                AuthenticationProperties authProperties = new AuthenticationProperties();

                // Tạo phiên đăng nhập
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // Chuyển hướng trang về home
                if (string.IsNullOrEmpty(taiKhoan.LastUrl))
                    return await Task.Run(() => RedirectToAction("Index", "Home"));
                else
                    return await Task.Run(() => Redirect(taiKhoan.LastUrl));
            }
            catch (Exception) { return await Task.Run(() => Unauthorized(MSG_AccountUnauthorized)); }
        }

        #endregion Private Methods
    }
}
