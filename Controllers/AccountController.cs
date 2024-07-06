using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Login(string userName = "", string userPass = "")
        {
            await CheckAccount(userName, userPass);
            return await Task.Run(() => Ok(new { msg = "Đăng nhập thành công!" }));
        }

        #endregion Public Methods



        #region Private Methods

        private async Task<bool> CheckAccount(string userName = "", string userPass = "")
        {
            // TODO: Mã hoá mật khẩu

            // TODO: Lấy thông tin tài khoản trong CSDL
            //IEnumerable<DsTaikhoan> result = await _unitOfWork.GetRepository<DsTaikhoan>().GetListAsync();
            // TODO: So sánh và đối chiếu
            return await Task.Run(() => true);
        }

        #endregion Private Methods
    }
}
