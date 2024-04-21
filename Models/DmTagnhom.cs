namespace WebAppManager.Models;

public partial class DmTagnhom : BaseEntities
{
    #region Public Properties

    public virtual ICollection<DmDiagioihanhchinh> DmDiagioihanhchinhs { get; set; } = new List<DmDiagioihanhchinh>();
    public virtual ICollection<DsNguontien> DsNguontiens { get; set; } = new List<DsNguontien>();
    public string? GhiChu { get; set; }
    public string TenTag { get; set; } = null!;

    #endregion Public Properties
}
