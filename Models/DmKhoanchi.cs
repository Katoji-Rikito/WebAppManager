namespace WebAppManager.Models;

public partial class DmKhoanchi : BaseEntities
{
    #region Public Properties

    public string? DiaChi { get; set; }
    public string DonViTinh { get; set; } = null!;
    public virtual ICollection<DsChitieu> DsChitieus { get; set; } = new List<DsChitieu>();
    public string TenKhoanChi { get; set; } = null!;

    #endregion Public Properties
}
