namespace WebAppManager.Models.DTOs
{
    public class ThongTinTaiKhoanDto
    {
        public string UserName { get; set; } = string.Empty;
        public string UserPass { get; set; } = string.Empty;
        /// <summary>
        /// Lấy đường dẫn trước khi trỏ về trang đăng nhập
        /// </summary>
        public string? LastUrl { get; set; } = "/";
    }
}
