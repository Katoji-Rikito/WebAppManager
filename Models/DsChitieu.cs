using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DsChitieu
{
    public ulong Id { get; set; }

    public ulong IdKhoanChi { get; set; }

    public DateOnly NgayThang { get; set; }

    public ulong DonGia { get; set; }

    public byte SoLuong { get; set; }

    public bool LaKhoanNhan { get; set; }

    public string? GhiChu { get; set; }

    public bool? IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual DmKhoanchi IdKhoanChiNavigation { get; set; } = null!;
}
