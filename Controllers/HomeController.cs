using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAppManager.Models;

namespace WebAppManager.Controllers
{
    public class HomeController : GenericCRUDController<DsNguontien>
    {
        #region Private Fields

        private readonly ILogger<HomeController> _logger;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, WebappmanagerContext context) : base(context)
        {
            _logger = logger;
        }

        #endregion Public Constructors



        #region Public Methods

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetList()
        {
            try
            {
                //var temp = await _context.DsNguontiens.ToListAsync();
                var temp = "OK";
                return await Task.Run(() => Ok(temp));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View());
        }

        public async Task<IActionResult> Privacy()
        {
            return await Task.Run(() => View());
        }

        #endregion Public Methods
    }
}
