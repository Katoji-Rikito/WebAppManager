using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DsDiachi : BaseEntities
{
    #region Public Properties

    public string DiaChiChiTiet { get; set; } = null!;

    public string? GhiChu { get; set; }

    /// <summary>
    /// GUID
    /// </summary>
    public string IdDiaGioi { get; set; } = null!;

    public virtual DmDiagioihanhchinh IdDiaGioiNavigation { get; set; } = null!;

    #endregion Public Properties
}
