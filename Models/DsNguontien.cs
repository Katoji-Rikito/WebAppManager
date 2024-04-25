using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DsNguontien
{
    public ulong Id { get; set; }

    public string TenNguonTien { get; set; } = null!;

    public ulong IdNhomTien { get; set; }

    public ulong DonGia { get; set; }

    public ulong SoLuong { get; set; }

    public bool? IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual DmTagnhom IdNhomTienNavigation { get; set; } = null!;
}
