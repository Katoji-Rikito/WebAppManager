namespace WebAppManager.Models.DTOs
{
    public class ThongTinTaiKhoanDto
    {
        #region Public Properties

        /// <summary>
        /// Lấy đường dẫn trước khi trỏ về trang đăng nhập
        /// </summary>
        public string? LastUrl { get; set; } = "/";

        public string UserName { get; set; } = string.Empty;

        public string UserPass { get; set; } = string.Empty;

        #endregion Public Properties
    }
}
