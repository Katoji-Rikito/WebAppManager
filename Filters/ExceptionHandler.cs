using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAppManager.Filters
{
    public class ExceptionHandler : IExceptionFilter
    {
        private readonly ILogger<ExceptionHandler> _logger;
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }
        #region Public Methods

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Lỗi " + context.Exception.Message);
            context.Result = new BadRequestObjectResult(new { data = context.Exception.Message });
            context.ExceptionHandled = true;
        }

        #endregion Public Methods
    }
}
