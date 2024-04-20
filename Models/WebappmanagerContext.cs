﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppManager.Models;

public partial class WebappmanagerContext : DbContext
{
    public WebappmanagerContext() { }
    public WebappmanagerContext(DbContextOptions<WebappmanagerContext> options) : base(options) { }

    #region DbSets
    public virtual DbSet<DmDiagioihanhchinh> DmDiagioihanhchinhs { get; set; }
    public virtual DbSet<DmGiaphong> DmGiaphongs { get; set; }
    public virtual DbSet<DmKhoanchi> DmKhoanchis { get; set; }
    public virtual DbSet<DmPass> DmPasses { get; set; }
    public virtual DbSet<DmTagnhom> DmTagnhoms { get; set; }
    public virtual DbSet<DsChitieu> DsChitieus { get; set; }
    public virtual DbSet<DsDiachi> DsDiachis { get; set; }
    public virtual DbSet<DsNguontien> DsNguontiens { get; set; }
    public virtual DbSet<DsTaikhoan> DsTaikhoans { get; set; }
    #endregion DbSets

    #region Configuration
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DmDiagioihanhchinh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dm_diagioihanhchinh");

            entity.HasIndex(e => e.IdCapTren, "FK_DMDiaGioiHanhChinh_DMDiaGioiHanhChinh");

            entity.HasIndex(e => e.IdNhomCap, "FK_DMDiaGioiHanhChinh_DMTagNhom");

            entity.HasIndex(e => e.Id, "ID").IsUnique();

            entity.HasIndex(e => e.TenDiaGioi, "TenDiaGioi").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("GUID")
                .HasColumnName("ID");
            entity.Property(e => e.IdCapTren)
                .HasMaxLength(32)
                .HasDefaultValueSql("'NULL'")
                .HasComment("GUID");
            entity.Property(e => e.IdNhomCap)
                .HasMaxLength(32)
                .HasComment("GUID");
            entity.Property(e => e.TenCap)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinytext");
            entity.Property(e => e.TenDiaGioi).HasColumnType("tinytext");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdCapTrenNavigation).WithMany(p => p.InverseIdCapTrenNavigation)
                .HasForeignKey(d => d.IdCapTren)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DMDiaGioiHanhChinh_DMDiaGioiHanhChinh");

            entity.HasOne(d => d.IdNhomCapNavigation).WithMany(p => p.DmDiagioihanhchinhs)
                .HasForeignKey(d => d.IdNhomCap)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DMDiaGioiHanhChinh_DMTagNhom");
        });

        modelBuilder.Entity<DmGiaphong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dm_giaphong");

            entity.HasIndex(e => e.Id, "ID").IsUnique();

            entity.HasIndex(e => e.TenGiaPhong, "TenGiaPhong").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("GUID")
                .HasColumnName("ID");
            entity.Property(e => e.TenGiaPhong).HasColumnType("tinytext");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DmKhoanchi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dm_khoanchi");

            entity.HasIndex(e => e.Id, "ID").IsUnique();

            entity.HasIndex(e => e.TenKhoanChi, "TenKhoanChi").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("GUID")
                .HasColumnName("ID");
            entity.Property(e => e.DiaChi)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.DonViTinh).HasColumnType("tinytext");
            entity.Property(e => e.TenKhoanChi).HasColumnType("tinytext");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DmPass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dm_pass");

            entity.HasIndex(e => e.Id, "ID").IsUnique();

            entity.HasIndex(e => e.TenPass, "TenPass").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("GUID")
                .HasColumnName("ID");
            entity.Property(e => e.TenPass).HasColumnType("tinytext");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DmTagnhom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dm_tagnhom");

            entity.HasIndex(e => e.Id, "ID").IsUnique();

            entity.HasIndex(e => e.TenTag, "TenTag").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("GUID")
                .HasColumnName("ID");
            entity.Property(e => e.GhiChu)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.TenTag).HasColumnType("text");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DsChitieu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ds_chitieu");

            entity.HasIndex(e => e.IdKhoanChi, "FK_DSChiTieu_DMKhoanChi");

            entity.HasIndex(e => e.Id, "ID").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("GUID")
                .HasColumnName("ID");
            entity.Property(e => e.DonGia).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.GhiChu)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.IdKhoanChi)
                .HasMaxLength(32)
                .HasComment("GUID");
            entity.Property(e => e.NgayThang).HasColumnType("date");
            entity.Property(e => e.SoLuong).HasColumnType("tinyint(3) unsigned");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdKhoanChiNavigation).WithMany(p => p.DsChitieus)
                .HasForeignKey(d => d.IdKhoanChi)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DSChiTieu_DMKhoanChi");
        });

        modelBuilder.Entity<DsDiachi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ds_diachi");

            entity.HasIndex(e => e.IdDiaGioi, "FK_DSDiaChi_DMDiaGioiHanhChinh");

            entity.HasIndex(e => e.Id, "ID").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("GUID")
                .HasColumnName("ID");
            entity.Property(e => e.DiaChiChiTiet).HasColumnType("text");
            entity.Property(e => e.GhiChu)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.IdDiaGioi)
                .HasMaxLength(32)
                .HasComment("GUID");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdDiaGioiNavigation).WithMany(p => p.DsDiachis)
                .HasForeignKey(d => d.IdDiaGioi)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DSDiaChi_DMDiaGioiHanhChinh");
        });

        modelBuilder.Entity<DsNguontien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ds_nguontien");

            entity.HasIndex(e => e.IdNhomTien, "FK_DSNguonTien_DMTagNhom");

            entity.HasIndex(e => e.Id, "ID").IsUnique();

            entity.HasIndex(e => e.TenNguonTien, "TenNguonTien").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("GUID")
                .HasColumnName("ID");
            entity.Property(e => e.DonGia).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.IdNhomTien)
                .HasMaxLength(32)
                .HasComment("GUID");
            entity.Property(e => e.SoLuong).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.TenNguonTien).HasColumnType("tinytext");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdNhomTienNavigation).WithMany(p => p.DsNguontiens)
                .HasForeignKey(d => d.IdNhomTien)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DSNguonTien_DMTagNhom");
        });

        modelBuilder.Entity<DsTaikhoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ds_taikhoan");

            entity.HasIndex(e => e.Id, "ID").IsUnique();

            entity.HasIndex(e => e.TenDangNhap, "TenDangNhap").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("GUID")
                .HasColumnName("ID");
            entity.Property(e => e.HashSalt).HasColumnType("tinytext");
            entity.Property(e => e.MatKhau).HasColumnType("text");
            entity.Property(e => e.TenDangNhap).HasColumnType("tinytext");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    #endregion Configuration
}
