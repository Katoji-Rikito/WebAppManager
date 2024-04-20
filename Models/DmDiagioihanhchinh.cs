using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DmDiagioihanhchinh : BaseEntities
{
    #region Public Properties

    public virtual ICollection<DsDiachi> DsDiachis { get; set; } = new List<DsDiachi>();

    /// <summary>
    /// GUID
    /// </summary>
    public string? IdCapTren { get; set; }

    public virtual DmDiagioihanhchinh? IdCapTrenNavigation { get; set; }

    /// <summary>
    /// GUID
    /// </summary>
    public string IdNhomCap { get; set; } = null!;

    public virtual DmTagnhom IdNhomCapNavigation { get; set; } = null!;
    public virtual ICollection<DmDiagioihanhchinh> InverseIdCapTrenNavigation { get; set; } = new List<DmDiagioihanhchinh>();
    public string? TenCap { get; set; }

    public string TenDiaGioi { get; set; } = null!;

    #endregion Public Properties
}
