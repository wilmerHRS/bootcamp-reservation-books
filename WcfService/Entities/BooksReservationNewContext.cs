using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WcfService.Entities
{
    public partial class BooksReservationNewContext : DbContext
    {
        public BooksReservationNewContext()
        {
        }

        public BooksReservationNewContext(DbContextOptions<BooksReservationNewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tbooks> Tbooks { get; set; }
        public virtual DbSet<Treservations> Treservations { get; set; }
        public virtual DbSet<Tusers> Tusers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbooks>(entity =>
            {
                entity.HasKey(e => e.IdBook)
                    .HasName("PK__TBooks__D80547A1BC1E9587");

                entity.ToTable("TBooks");

                entity.Property(e => e.IdBook).HasColumnName("idBook");

                entity.Property(e => e.BitIsAvailable)
                    .HasColumnName("bitIsAvailable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.BitIsDeleted)
                    .HasColumnName("bitIsDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DtimeCreatedAt)
                    .HasColumnName("dtimeCreatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DtimeUpdatedAt)
                    .HasColumnName("dtimeUpdatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IntStatus)
                    .HasColumnName("intStatus")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.VarCode)
                    .IsRequired()
                    .HasColumnName("varCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VarTitle)
                    .IsRequired()
                    .HasColumnName("varTitle")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Treservations>(entity =>
            {
                entity.HasKey(e => e.IdResevation)
                    .HasName("PK__TReserva__9B182A360B01171E");

                entity.ToTable("TReservations");

                entity.Property(e => e.IdResevation).HasColumnName("idResevation");

                entity.Property(e => e.BitIsDeleted)
                    .HasColumnName("bitIsDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DtimeCreatedAt)
                    .HasColumnName("dtimeCreatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DtimeDateReservation)
                    .HasColumnName("dtimeDateReservation")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DtimeUpdatedAt)
                    .HasColumnName("dtimeUpdatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IdBook).HasColumnName("idBook");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.IntStatus)
                    .HasColumnName("intStatus")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.VarBookName)
                    .IsRequired()
                    .HasColumnName("varBookName")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VarUserName)
                    .IsRequired()
                    .HasColumnName("varUserName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.Treservations)
                    .HasForeignKey(d => d.IdBook)
                    .HasConstraintName("FK_TReservations_TBooks");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Treservations)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_TReservations_TUsers");
            });

            modelBuilder.Entity<Tusers>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__TUsers__3717C98237962AD3");

                entity.ToTable("TUsers");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.BitIsDeleted)
                    .HasColumnName("bitIsDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DtimeCreatedAt)
                    .HasColumnName("dtimeCreatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DtimeUpdatedAt)
                    .HasColumnName("dtimeUpdatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IntStatus)
                    .HasColumnName("intStatus")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.VarEmail)
                    .IsRequired()
                    .HasColumnName("varEmail")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VarFirstName)
                    .IsRequired()
                    .HasColumnName("varFirstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VarLastName)
                    .IsRequired()
                    .HasColumnName("varLastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VarPassword)
                    .IsRequired()
                    .HasColumnName("varPassword")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
