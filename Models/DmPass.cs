using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DmPass
{
    public ulong Id { get; set; }

    public string TenPass { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }
}
