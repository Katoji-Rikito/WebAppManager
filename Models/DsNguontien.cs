using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DsNguontien
{
    public sbyte MaNguonTien { get; set; }

    public string NhomNguonTien { get; set; } = null!;

    public string TenNguonTien { get; set; } = null!;

    public long DonGia { get; set; }

    public long SoLuong { get; set; }

    public DateTime UpdateAt { get; set; }
}
