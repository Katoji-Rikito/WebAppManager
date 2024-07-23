using Microsoft.AspNetCore.Mvc;
using WebAppManager.Models;

namespace WebAppManager.Controllers
{
    public class HomeController : BaseController<HomeController, DsNguontien>
    {
        #region Public Methods

        public async Task<IActionResult> Privacy()
        {
            return await Task.Run(View);
        }

        #endregion Public Methods
    }
}
