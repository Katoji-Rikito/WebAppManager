using Microsoft.AspNetCore.Mvc;
using WebAppManager.Models;

namespace WebAppManager.Controllers
{
    public class HomeController : BaseController<HomeController, DsNguontien>
    {
        public async Task<IActionResult> Privacy()
        {
            return await Task.Run(View);
        }
    }
}
