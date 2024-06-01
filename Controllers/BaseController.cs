using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppManager.Models;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Controllers
{
    [Authorize]
    public class BaseController<TController, TEntity> : Controller
        where TController : BaseController<TController, TEntity>
        where TEntity : BaseEntities
    {
        #region Private Fields

        private IUnitOfWork? _unitOfWork;

        #endregion Private Fields

        #region Protected Properties

        protected IUnitOfWork UnitOfWork => _unitOfWork ??= HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

        #endregion Protected Properties

        public async Task<IActionResult> GetList(DataSourceLoadOptions loadOptions)
        {
            IEnumerable<TEntity> listResult = await UnitOfWork.GetRepository<TEntity>().GetListAsync();
            return await Task.Run(() => Ok(DataSourceLoader.Load(listResult, loadOptions)));
        }

        public async Task<IActionResult> Index()
        {
            return await Task.Run(View);
        }
    }
}
