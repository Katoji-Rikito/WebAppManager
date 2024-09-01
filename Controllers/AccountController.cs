using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppManager.Models;
using WebAppManager.Repositories;

namespace WebAppManager.Controllers
{

    public class AccountController(IUnitOfWork unitOfWork) : Controller
    {

        #region Private Fields

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly HashType pwdHashType = HashType.SHA512;

        #endregion Private Fields



        #region Private Methods

        /// <summary>
        /// Tạo tài khoản cho lần đầu triển khai ứng dụng
        /// </summary>
        /// <param name="taiKhoan"> Thông tin tài khoản cần tạo </param>
        /// <returns> Kết quả tạo tài khoản </returns>
        private async Task<IActionResult> CreateAccountFirstTimeAsync(ThongTinTaiKhoanDto? taiKhoan = null)
        {
            // Kiểm tra xem có phải đây là lần đâu triển khai thật không
            if (!IsFirstTimeDeployAppAsync().Result)
            {
                return BadRequest("🤬 Thằng chó, mày lừa bố mày! Về mua thuốc hồi trinh mà uống! ĐI NẤU ĂN, COOK!");
            }

            // Kiểm tra dữ liệu trước khi nấu
            if (!ModelState.IsValid || taiKhoan is null)
            {
                return await Task.Run(() => BadRequest(CommonMessages.ParamsIsNullOrEmpty));
            }

            await _unitOfWork.BeginTransactionAsync();
            _ = await _unitOfWork.GetRepository<WebappmanagerContext, DsTaikhoan>().CreateAsync(new DsTaikhoan
            {
                TenDangNhap = taiKhoan.UserName.ToUpper(),
                MatKhau = BCrypt.Net.BCrypt.EnhancedHashPassword(taiKhoan.UserPass, pwdHashType), // Tạo mật khẩu đã mã hoá cùng salt
                HashSalt = BCrypt.Net.BCrypt.GenerateSalt(), // Không cần sử dụng nữa do thư viện tự lấy rồi. Thêm vào cho có thôi hoặc để lừa mấy thằng hắc cơ lỏd 🤣
                IsAble = true,
                UpdatedAt = DateTime.Now,
            });
            await _unitOfWork.CommitAsync();
            return await Task.Run(() => Created(string.Empty, taiKhoan));
        }

        /// <summary>
        /// Tạo hoặc cập nhật tài khoản đăng nhập
        /// </summary>
        /// <param name="userName"> Tên tài khoản cần tạo hoặc cập nhật </param>
        /// <param name="userPass"> Mật khẩu đăng nhập </param>
        /// <returns> </returns>
        [Authorize]
        private async Task<IActionResult> CreateOrUpdateAccountAsync(ThongTinTaiKhoanDto? taiKhoan = null)
        {
            // Xử lý dữ liệu trước khi nấu
            if (!ModelState.IsValid || taiKhoan is null)
            {
                return await Task.Run(() => BadRequest(CommonMessages.ParamsIsNullOrEmpty));
            }

            taiKhoan.UserName = taiKhoan.UserName.ToUpper();

            // Tìm xem tài khoản đã tồn tại hay chưa
            DsTaikhoan? record = await _unitOfWork.GetRepository<WebappmanagerContext, DsTaikhoan>().GetDataAsync(r => r.TenDangNhap == taiKhoan.UserName);

            await _unitOfWork.BeginTransactionAsync();
            if (record is null)
            {
                // Nếu chưa có tài khoản thì tạo mới
                _ = await _unitOfWork.GetRepository<WebappmanagerContext, DsTaikhoan>().CreateAsync(new DsTaikhoan
                {
                    TenDangNhap = taiKhoan.UserName,
                    MatKhau = BCrypt.Net.BCrypt.EnhancedHashPassword(taiKhoan.UserPass, pwdHashType), // Tạo mật khẩu đã mã hoá cùng salt
                    HashSalt = BCrypt.Net.BCrypt.GenerateSalt(), // Không cần sử dụng nữa do thư viện tự lấy rồi. Thêm vào cho có thôi hoặc để lừa mấy thằng hắc cơ lỏd 🤣
                    IsAble = true,
                    UpdatedAt = DateTime.Now,
                });
            }
            else
            {
                // Cập nhật lại mật khẩu
                record.HashSalt = BCrypt.Net.BCrypt.GenerateSalt();
                record.MatKhau = BCrypt.Net.BCrypt.EnhancedHashPassword(taiKhoan.UserPass, pwdHashType); // Tạo mật khẩu đã mã hoá cùng salt
                record.UpdatedAt = DateTime.Now;
                _ = await _unitOfWork.GetRepository<WebappmanagerContext, DsTaikhoan>().UpdateAsync(record);
            }

            // Lưu lại vào CSDL
            await _unitOfWork.CommitAsync();

            return await Task.Run(() => Created(string.Empty, record));
        }

        /// <summary>
        /// Kiểm tra xem đây có phải là lần đầu triển khai ứng dụng hay không
        /// </summary>
        /// <returns> True nếu chưa có tài khoản nào trong CSDL </returns>
        private async Task<bool> IsFirstTimeDeployAppAsync()
        {
            return !await _unitOfWork.GetRepository<WebappmanagerContext, DsTaikhoan>().IsRecordExistAsync(r => r.Id > 0);
        }

        /// <summary>
        /// Đăng xuất tài khoản
        /// </summary>
        /// <returns> </returns>
        private async Task SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Kiểm tra tài khoản đăng nhập ứng dụng
        /// </summary>
        /// <param name="taiKhoan"> Thông tin tài khoản đăng nhập </param>
        /// <returns> Kết quả kiểm tra: True nếu tài khoản hợp lệ </returns>
        private async Task<IActionResult> VerifyAccountAsync(ThongTinTaiKhoanDto taiKhoan)
        {

            // Tạo trước một số thông tin người dùng
            List<Claim> claims =
                [
                    new Claim(ClaimTypes.Name, taiKhoan.UserName),
                ];

            // Xử lý chuỗi thô
            taiKhoan.UserName = taiKhoan.UserName.ToUpper();

            // Lấy thông tin tài khoản trong CSDL So sánh mật khẩu vừa nhập với dữ liệu từ CSDL
            DsTaikhoan? record = await _unitOfWork.GetRepository<WebappmanagerContext, DsTaikhoan>().GetDataAsync(r => r.IsAble && r.TenDangNhap == taiKhoan.UserName);
            if (record is null || !BCrypt.Net.BCrypt.EnhancedVerify(taiKhoan.UserPass, record.MatKhau, pwdHashType))
            {
                await SignOutAsync();
                return await Task.Run(() => Unauthorized(CommonMessages.AccountUnauthorized));
            }

            // Đăng nhập thành công, ghi lại các thông tin người dùng
            claims.Add(new Claim("LoggedInAt", DateTime.Now.ToString()));

            // Gán thông tin người dùng vào cookie
            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Thiết lập thuộc tính
            AuthenticationProperties authProperties = new();

            // Tạo phiên đăng nhập
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Chuyển hướng trang về trang trước đó hoặc home
            return await Task.Run(() => Ok(string.IsNullOrEmpty(taiKhoan.LastUrl) ? "/" : taiKhoan.LastUrl));
        }

        #endregion Private Methods

        /// <summary>
        /// Tạo tài khoản cho lần đầu triển khai ứng dụng
        /// </summary>
        /// <param name="taiKhoan"> Thông tin tài khoản cần tạo </param>
        /// <returns> Kết quả tọ tài khoản </returns>
        [HttpPost]
        public async Task<IActionResult> CreateAccountFirstTime([FromBody] ThongTinTaiKhoanDto? taiKhoan = null)
        {
            return await CreateAccountFirstTimeAsync(taiKhoan);
        }

        /// <summary>
        /// Kiểm tra ứng dụng có phải là lần đầu triển khai không
        /// </summary>
        /// <returns> True nếu là lần đầu </returns>
        [HttpGet]
        public async Task<IActionResult> CheckFirstTimeDeployApp()
        {
            return Ok(await IsFirstTimeDeployAppAsync());
        }

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
            await SignOutAsync();
            return await Task.Run(() => RedirectToAction("Index", "Home"));
        }

        /// <summary>
        /// Đăng nhập ứng dụng
        /// </summary>
        /// <param name="userName"> Tên đăng nhập </param>
        /// <param name="userPass"> Mật khẩu </param>
        /// <returns> Kết quả đăng nhập </returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ThongTinTaiKhoanDto? taiKhoan = null)
        {

            // Kiểm tra dữ liệu đầu vào
            return !ModelState.IsValid || taiKhoan is null
                ? await Task.Run(() => BadRequest(CommonMessages.ParamsIsNullOrEmpty))
                : await VerifyAccountAsync(taiKhoan);
        }
    }
}