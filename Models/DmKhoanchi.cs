using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DmKhoanchi
{
    public ulong Id { get; set; }

    public string TenKhoanChi { get; set; } = null!;

    public string DonViTinh { get; set; } = null!;

    public string? DiaChi { get; set; }

    public bool? IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<DsChitieu> DsChitieus { get; set; } = new List<DsChitieu>();
}
