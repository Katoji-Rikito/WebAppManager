using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppManager.Models;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Controllers
{
    public class HomeController : Controller
    {
        #region Private Fields

        private readonly IGenericRepository<DmGiaphong> _dmGiaphongRepo;
        private readonly ILogger<HomeController> _logger;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IGenericRepository<DmGiaphong> dmGiaphongRepo)
        {
            _logger = logger;
            _dmGiaphongRepo = dmGiaphongRepo;
        }

        #endregion Public Constructors



        #region Public Methods

        public async Task<IActionResult> Create()
        {
            try
            {
                DmGiaphong record = new DmGiaphong();
                record.TenGiaPhong = "100k/2h";
                record = await _dmGiaphongRepo.CreateAsync(record);

                return await Task.Run(async () => Ok(new { rowAffected = await _dmGiaphongRepo.SaveChangesAsync(), data = record }));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetList()
        {
            try
            {
                var listResult = await _dmGiaphongRepo.GetListAsync();
                return await Task.Run(() => Ok(new { data = listResult }));
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
