namespace WebAppManager.Models;

public partial class DsDiachi : BaseEntities
{
    #region Public Properties

    public string DiaChiChiTiet { get; set; } = null!;
    public string? GhiChu { get; set; }
    public long IdDiaGioi { get; set; }
    public virtual DmDiagioihanhchinh IdDiaGioiNavigation { get; set; } = null!;

    #endregion Public Properties
}
