﻿using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

/// <summary>
/// Danh sách tài khoản dùng để đăng nhập ứng dụng
/// </summary>
public partial class DsTaikhoan
{
    public ulong Id { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string HashSalt { get; set; } = null!;

    /// <summary>
    /// Hết hiệu lực: 0, có hiệu lực: 1
    /// </summary>
    public bool? IsAble { get; set; }

    /// <summary>
    /// Thời gian cập nhật dữ liệu
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
