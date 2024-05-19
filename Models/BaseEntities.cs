namespace WebAppManager.Models
{
    public class BaseEntities
    {
        #region Public Properties

        public ulong Id { get; set; }

        /// <summary>
        /// Hết hiệu lực: 0, có hiệu lực: 1
        /// </summary>
        public bool IsAble { get; set; } = true;

        /// <summary>
        /// Thời gian cập nhật dữ liệu
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        #endregion Public Properties
    }
}
