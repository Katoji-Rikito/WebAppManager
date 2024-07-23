namespace WebAppManager.Models;

/// <summary>
/// Danh sách lịch sử thay đổi cơ sở dữ liệu
/// </summary>
public partial class DsLichsuthaydoi
{
    #region Public Properties

    public string? GiaTriCu { get; set; }

    public string? GiaTriMoi { get; set; }

    public ulong Id { get; set; }

    public ulong? IdDong { get; set; }

    /// <summary>
    /// Hết hiệu lực: 0, có hiệu lực: 1
    /// </summary>
    public bool? IsAble { get; set; }

    public string LoaiThayDoi { get; set; } = null!;

    public string TenBang { get; set; } = null!;

    public string TenCot { get; set; } = null!;

    /// <summary>
    /// Thời gian cập nhật dữ liệu
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    #endregion Public Properties
}
