using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppManager.Models;

public partial class WebappmanagerContext : DbContext
{
    #region Public Constructors

    public WebappmanagerContext()
    { }

    public WebappmanagerContext(DbContextOptions<WebappmanagerContext> options) : base(options)
    {
    }

    #endregion Public Constructors



    #region Public Properties

    public virtual DbSet<DmGiaphong> DmGiaphongs { get; set; }
    public virtual DbSet<DmKhoanchi> DmKhoanchis { get; set; }
    public virtual DbSet<DmPass> DmPasses { get; set; }
    public virtual DbSet<DmTagnhom> DmTagnhoms { get; set; }
    public virtual DbSet<DsChitieu> DsChitieus { get; set; }
    public virtual DbSet<DsNguontien> DsNguontiens { get; set; }
    public virtual DbSet<Taikhoandangnhap> Taikhoandangnhaps { get; set; }

    #endregion Public Properties



    #region Protected Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DmGiaphong>(entity =>
        {
            entity.HasKey(e => e.MaGiaPhong).HasName("PRIMARY");
            entity.ToTable("dm_giaphong");
            entity.HasIndex(e => e.TenGiaPhong, "TenGiaPhong").IsUnique();
            entity.Property(e => e.MaGiaPhong).HasColumnType("tinyint(3) unsigned zerofill");
            entity.Property(e => e.TenGiaPhong).HasColumnType("tinytext");
        });

        modelBuilder.Entity<DmKhoanchi>(entity =>
        {
            entity.HasKey(e => e.MaKhoanChi).HasName("PRIMARY");
            entity.ToTable("dm_khoanchi");
            entity.HasIndex(e => e.TenKhoanChi, "TenKhoanChi").IsUnique();
            entity.Property(e => e.MaKhoanChi).HasColumnType("int(10) unsigned zerofill");
            entity.Property(e => e.DiaChi)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.DonViTinh).HasColumnType("tinytext");
            entity.Property(e => e.TenKhoanChi).HasColumnType("tinytext");
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DmPass>(entity =>
        {
            entity.HasKey(e => e.MaPass).HasName("PRIMARY");
            entity.ToTable("dm_pass");
            entity.HasIndex(e => e.TenPass, "TenPass").IsUnique();
            entity.Property(e => e.MaPass).HasColumnType("tinyint(3) unsigned zerofill");
            entity.Property(e => e.TenPass).HasColumnType("tinytext");
        });

        modelBuilder.Entity<DmTagnhom>(entity =>
        {
            entity.HasKey(e => e.MaTag).HasName("PRIMARY");
            entity.ToTable("dm_tagnhom");
            entity.HasIndex(e => e.TenTag, "TenTag").IsUnique();
            entity.Property(e => e.MaTag).HasColumnType("int(10) unsigned zerofill");
            entity.Property(e => e.GhiChu)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.TenTag).HasColumnType("text");
        });

        modelBuilder.Entity<DsChitieu>(entity =>
        {
            entity.HasKey(e => e.Stt).HasName("PRIMARY");
            entity.ToTable("ds_chitieu");
            entity.HasIndex(e => e.MaKhoanChi, "FK-DanhSach_ChiTieu-DanhMuc_KhoanChi");
            entity.Property(e => e.Stt)
                .HasColumnType("int(10) unsigned zerofill")
                .HasColumnName("STT");
            entity.Property(e => e.DonGia).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.GhiChu)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.MaKhoanChi).HasColumnType("int(10) unsigned zerofill");
            entity.Property(e => e.NgayThang).HasColumnType("date");
            entity.Property(e => e.SoLuong).HasColumnType("tinyint(3) unsigned");
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            entity.HasOne(d => d.MaKhoanChiNavigation).WithMany(p => p.DsChitieus)
                .HasForeignKey(d => d.MaKhoanChi)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK-DanhSach_ChiTieu-DanhMuc_KhoanChi");
        });

        modelBuilder.Entity<DsNguontien>(entity =>
        {
            entity.HasKey(e => e.MaNguonTien).HasName("PRIMARY");
            entity.ToTable("ds_nguontien");
            entity.HasIndex(e => e.TenNguonTien, "TenNguonTien").IsUnique();
            entity.Property(e => e.MaNguonTien).HasColumnType("tinyint(3) unsigned zerofill");
            entity.Property(e => e.DonGia).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.NhomNguonTien).HasColumnType("tinytext");
            entity.Property(e => e.SoLuong).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.TenNguonTien).HasColumnType("tinytext");
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Taikhoandangnhap>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PRIMARY");
            entity.ToTable("taikhoandangnhap");
            entity.HasIndex(e => e.TenDangNhap, "TenDangNhap").IsUnique();
            entity.Property(e => e.MaNguoiDung).HasColumnType("tinyint(3) unsigned zerofill");
            entity.Property(e => e.HashSalt).HasColumnType("tinytext");
            entity.Property(e => e.MatKhau).HasColumnType("text");
            entity.Property(e => e.TenDangNhap).HasColumnType("tinytext");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    #endregion Protected Methods

    #region Private Methods

    private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    #endregion Private Methods
}
