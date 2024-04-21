using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppManager.Models;

namespace WebAppManager.Controllers
{
    public class GenericCRUDController<T> : Controller where T : BaseEntities
    {
        #region Private Fields

        private readonly WebappmanagerContext _context;

        #endregion Private Fields

        #region Public Constructors

        public GenericCRUDController(WebappmanagerContext context)
        {
            _context = context;
        }

        #endregion Public Constructors



        #region Public Methods

        #endregion Public Methods



    }
}
