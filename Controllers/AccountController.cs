using Microsoft.AspNetCore.Mvc;
using WebAppManager.Models;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return await Task.Run(View);
        }

        public async Task<IActionResult> Login()
        {
            IEnumerable<DsTaikhoan> result = await _unitOfWork.GetRepository<DsTaikhoan>().GetListAsync();
            return await Task.Run(() => Ok("Đăng nhập thành công!"));
        }
        public async Task<IActionResult> Logout()
        {
            IEnumerable<DsTaikhoan> result = await _unitOfWork.GetRepository<DsTaikhoan>().GetListAsync();
            return await Task.Run(() => Ok("Đăng xuất thành công!"));
        }
        public async Task<IActionResult> AccessDenied()
        {
            return await Task.Run(Unauthorized);
        }
    }
}
