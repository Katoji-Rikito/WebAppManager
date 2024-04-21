namespace WebAppManager.Models;

public partial class DsChitieu : BaseEntities
{
    #region Public Properties

    public long DonGia { get; set; }
    public string? GhiChu { get; set; }
    public long IdKhoanChi { get; set; }

    public virtual DmKhoanchi IdKhoanChiNavigation { get; set; } = null!;
    public bool LaKhoanNhan { get; set; }
    public DateTime NgayThang { get; set; }
    public sbyte SoLuong { get; set; }

    #endregion Public Properties
}
