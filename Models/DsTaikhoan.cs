using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DsTaikhoan
{
    public ulong Id { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string HashSalt { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }
}
