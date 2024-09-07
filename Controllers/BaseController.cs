using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppManager.Repositories;

namespace WebAppManager.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        #region Private Fields

        private IUnitOfWork? _unitOfWork;

        #endregion Private Fields



        #region Protected Properties

        protected IUnitOfWork UnitOfWork => _unitOfWork ??= HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

        #endregion Protected Properties



        #region Public Methods

        //public async Task<IActionResult> GetList(DataSourceLoadOptions loadOptions)
        //{
        //    IEnumerable<TEntity> listResult = await UnitOfWork.GetRepository<TDbContext, TEntity>().GetListAsync() ?? Enumerable.Empty<TEntity>();
        //    return await Task.Run(() => Ok(DataSourceLoader.Load(listResult, loadOptions)));
        //}

        public async Task<IActionResult> Index()
        {
            return await Task.Run(View);
        }

        #endregion Public Methods
    }
}