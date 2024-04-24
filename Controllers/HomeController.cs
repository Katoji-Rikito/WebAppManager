using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppManager.Models;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Controllers
{
    public class HomeController : Controller
    {
        #region Private Fields

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors



        #region Public Methods

        public async Task<IActionResult> Create()
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                DmGiaphong record = new DmGiaphong();
                record.TenGiaPhong = "120k/2h";
                record = await _unitOfWork.GetRepository<DmGiaphong>().CreateAsync(record);

                int rowsResult = await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

                return await Task.Run(() => Ok(new { rowAffected = rowsResult, data = record }));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest(ex.Message);
            }
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
                var listResult = await _unitOfWork.GetRepository<DmGiaphong>().GetListAsync();
                var listResult2 = await _unitOfWork.GetRepository<DsNguontien>().GetListAsync();
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
