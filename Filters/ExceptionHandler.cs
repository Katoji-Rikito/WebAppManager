﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections;
using System.Diagnostics;
using WebAppManager.Models;

namespace WebAppManager.Filters
{
    public class ExceptionHandler : IExceptionFilter
    {
        #region Private Fields

        private readonly ILogger<ExceptionHandler> _logger;

        #endregion Private Fields

        #region Public Constructors

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        #endregion Public Constructors



        #region Public Methods

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Lỗi " + context.Exception.Message);
            context.ExceptionHandled = true;
            context.Result = new ViewResult
            {
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

        #endregion Public Methods



        #region Private Methods

        /// <summary>
        /// Chuyển IDictionary sang IDictionary<string, object>
        /// </summary>
        /// <param name="data">Dữ liệu cần chuyển</param>
        /// <returns>IDictionary<string, object></returns>
        private IDictionary<string, object> ConvertToGenericDictionary(IDictionary? data = null)
        {
            var result = new Dictionary<string, object>();
            if (data is null) return result;
            foreach (DictionaryEntry entry in data)
            {
                if (entry.Key is string key)
                    result[key] = entry.Value ?? null!;
            }
            return result;
        }

        #endregion Private Methods
    }
}
