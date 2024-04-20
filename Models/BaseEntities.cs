namespace WebAppManager.Models
{
    public class BaseEntities
    {
        #region Public Properties

        /// <summary>
        /// GUID
        /// </summary>
        public string Id { get; set; } = null!;

        public DateTime UpdatedAt { get; set; }

        #endregion Public Properties
    }
}
