using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DsDiachi
{
    public ulong Id { get; set; }

    public ulong IdDiaGioi { get; set; }

    public string? DiaChiChiTiet { get; set; }

    public string? GhiChu { get; set; }

    public bool? IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual DmDiagioihanhchinh IdDiaGioiNavigation { get; set; } = null!;
}
