using System.ComponentModel.DataAnnotations;

namespace WebAppManager.Models
{
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
        /// <summary>
        /// Lưu trữ tạm tên đăng nhập
        /// </summary>
        private string? _userName;
        /// <summary>
        /// Lưu trữ tạm mật khẩu đăng nhập
        /// </summary>
        private string? _userPass;

        #region Public Properties

        /// <summary>
        /// Lấy đường dẫn trước khi trỏ về trang đăng nhập
        /// </summary>
        public string? LastUrl { get; set; } = "/";

        /// <summary>
        /// Tên tài khoản
        /// </summary>
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string UserName
        {
            get => _userName ?? null!;
            set => _userName = string.IsNullOrEmpty(value?.Trim()) ? null : value?.Trim();
        }

        /// <summary>
        /// Mật khẩu đăng nhập
        /// </summary>
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string UserPass
        {
            get => _userPass ?? null!;
            set => _userPass = string.IsNullOrEmpty(value?.Trim()) ? null : value?.Trim();
        }

        #endregion Public Properties
    }
}