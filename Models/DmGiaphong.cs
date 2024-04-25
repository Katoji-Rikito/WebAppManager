using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DmGiaphong
{
    public ulong Id { get; set; }

    public string TenGiaPhong { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }
}
