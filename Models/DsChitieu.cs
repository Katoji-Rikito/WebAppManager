using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DsChitieu
{
    public int Stt { get; set; }

    public DateTime NgayThang { get; set; }

    public int MaKhoanChi { get; set; }

    public long DonGia { get; set; }

    public sbyte SoLuong { get; set; }

    public bool LaKhoanNhan { get; set; }

    public string? GhiChu { get; set; }

    public DateTime UpdateAt { get; set; }

    public virtual DmKhoanchi MaKhoanChiNavigation { get; set; } = null!;
}
