namespace WebAppManager.Models;

/// <summary>
/// Danh mục các tag, nhóm chung cho nhiều bảng
/// </summary>
public partial class DmTagnhom : BaseEntities
{
    #region Public Properties

    public virtual ICollection<DsNguontien> DsNguontiens { get; set; } = [];

    public string? GhiChu { get; set; }

    public string TenTagNhom { get; set; } = null!;

    #endregion Public Properties
}