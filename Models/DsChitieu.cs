using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DsChitieu : BaseEntities
{
    #region Public Properties

    public long DonGia { get; set; }

    public string? GhiChu { get; set; }

    /// <summary>
    /// GUID
    /// </summary>
    public string IdKhoanChi { get; set; } = null!;

    public virtual DmKhoanchi IdKhoanChiNavigation { get; set; } = null!;
    public bool LaKhoanNhan { get; set; }
    public DateTime NgayThang { get; set; }
    public sbyte SoLuong { get; set; }

    #endregion Public Properties
}
