using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Thitrachnghiem.Models.Model
{
    public partial class thitracnghiemContext : DbContext
    {
        public thitracnghiemContext()
        {
        }

        public thitracnghiemContext(DbContextOptions<thitracnghiemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authority> Authorities { get; set; }
        public virtual DbSet<Cauhoi> Cauhois { get; set; }
        public virtual DbSet<Cautraloi> Cautralois { get; set; }
        public virtual DbSet<Chitietdethi> Chitietdethis { get; set; }
        public virtual DbSet<Chuyennganh> Chuyennganhs { get; set; }
        public virtual DbSet<Dethi> Dethis { get; set; }
        public virtual DbSet<Donvi> Donvis { get; set; }
        public virtual DbSet<Kithi> Kithis { get; set; }
        public virtual DbSet<Matrandethi> Matrandethis { get; set; }
        public virtual DbSet<Phienthi> Phienthis { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Thisinh> Thisinhs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Userrole> Userroles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=;Database=thitracnghiem;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authority>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("authority");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Mota)
                    .HasMaxLength(50)
                    .HasColumnName("mota");

                entity.Property(e => e.Tenhienthi)
                    .HasMaxLength(50)
                    .HasColumnName("tenhienthi");
            });

            modelBuilder.Entity<Cauhoi>(entity =>
            {
                entity.ToTable("cauhoi");

                entity.HasIndex(e => e.Uuid, "UQ__cauhoi__7F4279318C7E04A3")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bac).HasColumnName("bac");

                entity.Property(e => e.Idchuyennganh).HasColumnName("idchuyennganh");

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Trinhdodaotao)
                    .HasMaxLength(10)
                    .HasColumnName("trinhdodaotao")
                    .IsFixedLength(true);

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Cautraloi>(entity =>
            {
                entity.ToTable("cautraloi");

                entity.HasIndex(e => e.Uuid, "UQ__cautralo__7F427931D1374144")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cauhoiid).HasColumnName("cauhoiid");

                entity.Property(e => e.Neo).HasColumnName("neo");

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Chitietdethi>(entity =>
            {
                entity.ToTable("chitietdethi");

                entity.HasIndex(e => e.Uuid, "UQ__chitietd__7F427931EAF5234D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cauhoiid).HasColumnName("cauhoiid");

                entity.Property(e => e.Dethiid).HasColumnName("dethiid");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Chuyennganh>(entity =>
            {
                entity.ToTable("chuyennganh");

                entity.HasIndex(e => e.Uuid, "UQ__chuyenng__7F4279316B7DA73E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Sobac).HasColumnName("sobac");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Ten)
                    .IsUnicode(false)
                    .HasColumnName("ten");

                entity.Property(e => e.Trinhdodaotao)
                    .HasMaxLength(10)
                    .HasColumnName("trinhdodaotao")
                    .IsFixedLength(true);

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Dethi>(entity =>
            {
                entity.ToTable("dethi");

                entity.HasIndex(e => e.Uuid, "UQ__dethi__7F427931521316F9")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Kithiid).HasColumnName("kithiid");

                entity.Property(e => e.Madethi)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("madethi");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Donvi>(entity =>
            {
                entity.ToTable("donvi");

                entity.HasIndex(e => e.Uuid, "UQ__donvi__7F42793183BF7C20")
                    .IsUnique();

                entity.HasIndex(e => e.Uuid, "UQ__donvi__7F427931D682575C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ma)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Macha).HasColumnName("macha");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Ten).HasColumnName("ten");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Kithi>(entity =>
            {
                entity.ToTable("kithi");

                entity.HasIndex(e => e.Uuid, "UQ__kithi_ng__7F427931D9965B06")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bac).HasColumnName("bac");

                entity.Property(e => e.Chuyennganhid).HasColumnName("chuyennganhid");

                entity.Property(e => e.Dangthi).HasColumnName("dangthi");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Thoigianbatdau).HasColumnName("thoigianbatdau");

                entity.Property(e => e.Thoigianketthuc).HasColumnName("thoigianketthuc");

                entity.Property(e => e.Trinhdodaotao)
                    .HasMaxLength(10)
                    .HasColumnName("trinhdodaotao")
                    .IsFixedLength(true);

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Matrandethi>(entity =>
            {
                entity.ToTable("matrandethi");

                entity.HasIndex(e => e.Uuid, "UQ__matrande__7F42793144EC7DE3")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bac).HasColumnName("bac");

                entity.Property(e => e.Chuyennganhid).HasColumnName("chuyennganhid");

                entity.Property(e => e.Kithiid).HasColumnName("kithiid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tile).HasColumnName("tile");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Phienthi>(entity =>
            {
                entity.ToTable("phienthi");

                entity.HasIndex(e => e.Uuid, "UQ__phienthi__7F427931F3CA65CE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Kithiid).HasColumnName("kithiid");

                entity.Property(e => e.Thoigianbatdau)
                    .HasMaxLength(100)
                    .HasColumnName("thoigianbatdau");

                entity.Property(e => e.Thoigianketthuc)
                    .HasMaxLength(100)
                    .HasColumnName("thoigianketthuc");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.HasIndex(e => e.Uuid, "UQ__role__7F4279313AA4EC5D")
                    .IsUnique();

                entity.HasIndex(e => e.Uuid, "UQ__role__7F4279316A08C9DA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dsquyen)
                    .IsUnicode(false)
                    .HasColumnName("dsquyen");

                entity.Property(e => e.Mota).HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Thisinh>(entity =>
            {
                entity.ToTable("thisinh");

                entity.HasIndex(e => e.Uuid, "UQ__thisinh__7F4279314CA60FFF")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bacdanggiu).HasColumnName("bacdanggiu");

                entity.Property(e => e.Bacluong)
                    .HasMaxLength(50)
                    .HasColumnName("bacluong");

                entity.Property(e => e.Bacthi).HasColumnName("bacthi");

                entity.Property(e => e.Capbac)
                    .HasMaxLength(50)
                    .HasColumnName("capbac");

                entity.Property(e => e.Chucvu)
                    .HasMaxLength(50)
                    .HasColumnName("chucvu");

                entity.Property(e => e.Chuyennganhhoc).HasColumnName("chuyennganhhoc");

                entity.Property(e => e.Chuyennganhthiid).HasColumnName("chuyennganhthiid");

                entity.Property(e => e.Donvi).HasColumnName("donvi");

                entity.Property(e => e.Email)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Kithiid).HasColumnName("kithiid");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Namsinh)
                    .HasMaxLength(50)
                    .HasColumnName("namsinh");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Trinhdo)
                    .HasMaxLength(50)
                    .HasColumnName("trinhdo");

                entity.Property(e => e.Trinhdodaotao)
                    .HasMaxLength(10)
                    .HasColumnName("trinhdodaotao")
                    .IsFixedLength(true);

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Uuid, "UQ__users__7F4279317BBAF47A")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Chucvu).HasColumnName("chucvu");

                entity.Property(e => e.Madonvi).HasColumnName("madonvi");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username")
                    .IsFixedLength(true);

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.ToTable("userrole");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Roleid).HasColumnName("roleid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("FK_userrole_role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_userrole_users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
