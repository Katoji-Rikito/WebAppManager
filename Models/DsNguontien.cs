namespace WebAppManager.Models;

/// <summary>
/// Danh sách nguồn tiền
/// </summary>
public partial class DsNguontien : BaseEntities
{
    public ulong IdNhomTien { get; set; }

    public string TenNguonTien { get; set; } = null!;

    public ulong DonGia { get; set; }

    public ulong SoLuong { get; set; }

    public virtual DmTagnhom IdNhomTienNavigation { get; set; } = null!;
}
