namespace WebAppManager.Models
{
    public class ErrorViewModel
    {
        #region Public Properties

        public IDictionary<string, object>? Data { get; set; }

        public string? HelpLink { get; set; }

        public string? HResult { get; set; }

        public string? InnerException { get; set; }

        public string? Message { get; set; }

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? Source { get; set; }

        public string? StackTrace { get; set; }

        public string? TargetSite { get; set; }

        #endregion Public Properties
    }
}