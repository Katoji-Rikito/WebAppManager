using System;
using System.Collections.Generic;

namespace WebAppManager.Models;

public partial class DsNguontien : BaseEntities
{
    #region Public Properties

    public long DonGia { get; set; }

    /// <summary>
    /// GUID
    /// </summary>
    public string IdNhomTien { get; set; } = null!;

    public virtual DmTagnhom IdNhomTienNavigation { get; set; } = null!;
    public long SoLuong { get; set; }
    public string TenNguonTien { get; set; } = null!;

    #endregion Public Properties
}
