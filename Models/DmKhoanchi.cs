using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DmKhoanchi
{
    public int MaKhoanChi { get; set; }

    public string TenKhoanChi { get; set; } = null!;

    public string DonViTinh { get; set; } = null!;

    public string? DiaChi { get; set; }

    public DateTime UpdateAt { get; set; }

    public virtual ICollection<DsChitieu> DsChitieus { get; set; } = new List<DsChitieu>();
}
