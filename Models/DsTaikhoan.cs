namespace WebAppManager.Models;

public partial class DsTaikhoan : BaseEntities
{
    #region Public Properties

    public string HashSalt { get; set; } = null!;
    public string MatKhau { get; set; } = null!;
    public string TenDangNhap { get; set; } = null!;

    #endregion Public Properties
}
