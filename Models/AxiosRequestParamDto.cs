namespace WebAppManager.Models
{
    public class AxiosRequestParamDto
    {
    }

    /// <summary>
    /// DTO cập nhật thông tin thành phố OpenWeatherMap
    /// </summary>
    public class OpenWeatherMapCityDto
    {
        #region Public Properties

        /// <summary>
        /// Đất nước
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// ID thành phố
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// Vĩ độ
        /// </summary>
        public decimal? lat { get; set; }

        /// <summary>
        /// Kinh độ
        /// </summary>
        public decimal? lon { get; set; }

        /// <summary>
        /// Tên thành phố
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// Bang (?)
        /// </summary>
        public string? state { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// DTO chứa thông tin tài khoản đăng nhập
    /// </summary>
    public class ThongTinTaiKhoanDto
    {
        #region Public Properties

        /// <summary>
        /// Lấy đường dẫn trước khi trỏ về trang đăng nhập
        /// </summary>
        public string? LastUrl { get; set; } = "/";

        /// <summary>
        /// Tên tài khoản
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Mật khẩu đăng nhập
        /// </summary>
        public string UserPass { get; set; } = string.Empty;

        #endregion Public Properties
    }
}
