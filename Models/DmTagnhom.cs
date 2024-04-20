using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DmTagnhom
{
    public int MaTag { get; set; }

    public string TenTag { get; set; } = null!;

    public string? GhiChu { get; set; }
}
