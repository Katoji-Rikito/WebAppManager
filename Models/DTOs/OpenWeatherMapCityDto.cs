namespace WebAppManager.Models.DTOs
{
    public class OpenWeatherMapCityDto
    {
        #region Public Properties

        /// <summary>
        /// ID thành phố
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// Tên thành phố
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// Bang (?)
        /// </summary>
        public string? state { get; set; }
        /// <summary>
        /// Đất nước
        /// </summary>
        public string? country { get; set; }
        /// <summary>
        /// Kinh độ
        /// </summary>
        public decimal? lon { get; set; }
        /// <summary>
        /// Vĩ độ
        /// </summary>
        public decimal? lat { get; set; }

        #endregion Public Properties
    }
}