namespace WebAppManager.Models;

/// <summary>
/// Danh sách nguồn tiền
/// </summary>
public partial class DsNguontien : BaseEntities
{
    #region Public Properties

    public ulong DonGia { get; set; }

    public ulong IdNhomTien { get; set; }

    public virtual DmTagnhom IdNhomTienNavigation { get; set; } = null!;

    public ulong SoLuong { get; set; }

    public string TenNguonTien { get; set; } = null!;

    #endregion Public Properties
}
