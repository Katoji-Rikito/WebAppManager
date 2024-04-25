using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DmTagnhom
{
    public ulong Id { get; set; }

    public string TenTag { get; set; } = null!;

    public string? GhiChu { get; set; }

    public bool? IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<DmDiagioihanhchinh> DmDiagioihanhchinhs { get; set; } = new List<DmDiagioihanhchinh>();

    public virtual ICollection<DsNguontien> DsNguontiens { get; set; } = new List<DsNguontien>();
}
