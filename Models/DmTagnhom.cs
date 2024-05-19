namespace WebAppManager.Models;

/// <summary>
/// Danh mục các tag, nhóm chung cho nhiều bảng
/// </summary>
public partial class DmTagnhom : BaseEntities
{

    public string TenTagNhom { get; set; } = null!;

    public string? GhiChu { get; set; }

    public virtual ICollection<DsNguontien> DsNguontiens { get; set; } = [];
}
