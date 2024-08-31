using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections;
using System.Diagnostics;
using System.Text;
using WebAppManager.Models;

namespace WebAppManager.Filters
{
    public class ExceptionHandler : IExceptionFilter
    {
        #region Private Fields

        private readonly ILogger<ExceptionHandler> _logger;

        #endregion Private Fields



        #region Private Methods

        /// <summary> Chuyển IDictionary sang IDictionary<string, object> </summary> <param name="data">Dữ liệu cần chuyển</param> <returns>IDictionary<string, object></returns>
        private IDictionary<string, object> ConvertToGenericDictionary(IDictionary? data = null)
        {
            Dictionary<string, object> result = [];
            if (data is null)
            {
                return result;
            }

            foreach (DictionaryEntry entry in data)
            {
                if (entry.Key is string key)
                {
                    result[key] = entry.Value ?? null!;
                }
            }
            return result;
        }

        #endregion Private Methods

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            try
            {
                _logger.LogError(context.Exception, $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] Lỗi {context.Exception.Message}");
            }
            catch (Exception)
            {
                // Ghi log vào file nếu ghi vào Event Log thất bại
                string pathSave = Path.Combine(Directory.GetCurrentDirectory(), "WebAppManager_Logs");
                if (!Directory.Exists(pathSave))
                {
                    _ = Directory.CreateDirectory(pathSave);
                }

                _ = File.AppendAllTextAsync(Path.Combine(pathSave, $"{DateTime.Now.Ticks}_Error.log"), $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] Lỗi {context.Exception.Message}: {context.Exception}", Encoding.UTF8);
            }
            context.ExceptionHandled = true;
            context.Result = new ViewResult
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ViewName = "Error",
                ViewData = new ViewDataDictionary<ErrorViewModel>(new EmptyModelMetadataProvider(), context.ModelState)
                {
                    Model = new ErrorViewModel
                    {
                        RequestId = Activity.Current?.Id ?? context?.HttpContext?.TraceIdentifier,
                        Data = ConvertToGenericDictionary(context?.Exception?.Data),
                        HelpLink = context?.Exception?.HelpLink,
                        HResult = context?.Exception?.HResult.ToString(),
                        InnerException = context?.Exception?.InnerException?.ToString(),
                        Message = context?.Exception?.Message,
                        Source = context?.Exception?.Source,
                        StackTrace = context?.Exception?.StackTrace,
                        TargetSite = context?.Exception?.TargetSite?.ToString(),
                    }
                }
            };
        }
    }
}