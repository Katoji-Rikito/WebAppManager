using Microsoft.EntityFrameworkCore;

namespace WebAppManager.Models;

public partial class WebappmanagerContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder
            .UseCollation("utf32_vietnamese_ci")
            .HasCharSet("utf32");

        _ = modelBuilder.Entity<DmTagnhom>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("PRIMARY");

            _ = entity.ToTable("dm_tagnhom", tb => tb.HasComment("Danh mục các tag, nhóm chung cho nhiều bảng"));

            _ = entity.HasIndex(e => e.TenTagNhom, "TenTagNhom").IsUnique();

            _ = entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("ID");
            _ = entity.Property(e => e.GhiChu).HasColumnType("text");
            _ = entity.Property(e => e.IsAble)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasComment("Hết hiệu lực: 0, có hiệu lực: 1");
            _ = entity.Property(e => e.TenTagNhom).HasColumnType("text");
            _ = entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("Thời gian cập nhật dữ liệu")
                .HasColumnType("datetime");
        });

        _ = modelBuilder.Entity<DsLichsuthaydoi>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("PRIMARY");

            _ = entity.ToTable("ds_lichsuthaydoi", tb => tb.HasComment("Danh sách lịch sử thay đổi cơ sở dữ liệu"));

            _ = entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("ID");
            _ = entity.Property(e => e.GiaTriCu).HasColumnType("text");
            _ = entity.Property(e => e.GiaTriMoi).HasColumnType("text");
            _ = entity.Property(e => e.IdDong).HasColumnType("bigint(20) unsigned");
            _ = entity.Property(e => e.IsAble)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasComment("Hết hiệu lực: 0, có hiệu lực: 1");
            _ = entity.Property(e => e.LoaiThayDoi).HasColumnType("tinytext");
            _ = entity.Property(e => e.TenBang).HasColumnType("text");
            _ = entity.Property(e => e.TenCot).HasColumnType("text");
            _ = entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("Thời gian cập nhật dữ liệu")
                .HasColumnType("datetime");
        });

        _ = modelBuilder.Entity<DsNguontien>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("PRIMARY");

            _ = entity.ToTable("ds_nguontien", tb => tb.HasComment("Danh sách nguồn tiền"));

            _ = entity.HasIndex(e => e.IdNhomTien, "FK_DsNguonTien_DmTagNhom");

            _ = entity.HasIndex(e => e.TenNguonTien, "TenNguonTien").IsUnique();

            _ = entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("ID");
            _ = entity.Property(e => e.DonGia).HasColumnType("bigint(20) unsigned");
            _ = entity.Property(e => e.IdNhomTien).HasColumnType("bigint(20) unsigned");
            _ = entity.Property(e => e.IsAble)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasComment("Hết hiệu lực: 0, có hiệu lực: 1");
            _ = entity.Property(e => e.SoLuong)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20) unsigned");
            _ = entity.Property(e => e.TenNguonTien).HasColumnType("tinytext");
            _ = entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("Thời gian cập nhật dữ liệu")
                .HasColumnType("datetime");

            _ = entity.HasOne(d => d.IdNhomTienNavigation).WithMany(p => p.DsNguontiens)
                .HasForeignKey(d => d.IdNhomTien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DsNguonTien_DmTagNhom");
        });

        _ = modelBuilder.Entity<DsTaikhoan>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("PRIMARY");

            _ = entity.ToTable("ds_taikhoan", tb => tb.HasComment("Danh sách tài khoản dùng để đăng nhập ứng dụng"));

            _ = entity.HasIndex(e => e.TenDangNhap, "TenDangNhap").IsUnique();

            _ = entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("ID");
            _ = entity.Property(e => e.HashSalt).HasColumnType("tinytext");
            _ = entity.Property(e => e.IsAble)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasComment("Hết hiệu lực: 0, có hiệu lực: 1");
            _ = entity.Property(e => e.MatKhau).HasColumnType("text");
            _ = entity.Property(e => e.TenDangNhap).HasColumnType("tinytext");
            _ = entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("Thời gian cập nhật dữ liệu")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public WebappmanagerContext()
    {
    }

    public WebappmanagerContext(DbContextOptions<WebappmanagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DmTagnhom> DmTagnhoms { get; set; }

    public virtual DbSet<DsLichsuthaydoi> DsLichsuthaydois { get; set; }

    public virtual DbSet<DsNguontien> DsNguontiens { get; set; }

    public virtual DbSet<DsTaikhoan> DsTaikhoans { get; set; }
}