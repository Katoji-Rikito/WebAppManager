using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class Taikhoandangnhap
{
    public sbyte MaNguoiDung { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string HashSalt { get; set; } = null!;
}
