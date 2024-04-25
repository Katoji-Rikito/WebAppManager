using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DmDiagioihanhchinh
{
    public ulong Id { get; set; }

    public ulong? IdCapTren { get; set; }

    public ulong IdNhomCap { get; set; }

    public string? TenCap { get; set; }

    public string TenDiaGioi { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<DsDiachi> DsDiachis { get; set; } = new List<DsDiachi>();

    public virtual DmDiagioihanhchinh? IdCapTrenNavigation { get; set; }

    public virtual DmTagnhom IdNhomCapNavigation { get; set; } = null!;

    public virtual ICollection<DmDiagioihanhchinh> InverseIdCapTrenNavigation { get; set; } = new List<DmDiagioihanhchinh>();
}
