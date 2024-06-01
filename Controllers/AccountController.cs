using Microsoft.AspNetCore.Mvc;
using WebAppManager.Models;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Controllers
{
    public class AccountController : Controller
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors



        #region Public Methods

        public async Task<IActionResult> AccessDenied()
        {
            return await Task.Run(Unauthorized);
        }

        public async Task<IActionResult> Index()
        {
            return await Task.Run(View);
        }

        public async Task<IActionResult> Logout()
        {
            return await Task.Run(() => Ok("Đăng xuất thành công!"));
        }

        public async Task<IActionResult> Login(string userName = "", string userPass = "", string gift = "")
        {
            IEnumerable<DsTaikhoan> result = await _unitOfWork.GetRepository<DsTaikhoan>().GetListAsync();
            await CheckAccount(userName, userPass, gift);
            return await Task.Run(() => Ok("Đăng nhập thành công!"));
        }

        #endregion Public Methods



        #region Private Methods

        private async Task<bool> CheckAccount(string userName = "", string userPass = "", string key = "")
        {
            // TODO: Giải mã mật khẩu gửi về

            // TODO: Mã hoá mật khẩu để check

            // TODO: Lấy thông tin tài khoản trong CSDL

            // TODO: So sánh và đối chiếu
            return true;
        }

        #endregion Private Methods
    }
}
