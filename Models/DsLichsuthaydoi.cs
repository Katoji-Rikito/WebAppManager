using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

/// <summary>
/// Danh sách lịch sử thay đổi cơ sở dữ liệu
/// </summary>
public partial class DsLichsuthaydoi
{
    public ulong Id { get; set; }

    public string LoaiThayDoi { get; set; } = null!;

    public string TenBang { get; set; } = null!;

    public ulong? IdDong { get; set; }

    public string TenCot { get; set; } = null!;

    public string? GiaTriCu { get; set; }

    public string? GiaTriMoi { get; set; }

    /// <summary>
    /// Hết hiệu lực: 0, có hiệu lực: 1
    /// </summary>
    public bool? IsAble { get; set; }

    /// <summary>
    /// Thời gian cập nhật dữ liệu
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
