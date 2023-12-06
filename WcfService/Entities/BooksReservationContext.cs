using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WcfService.Entities
{
    public partial class BooksReservationContext : DbContext
    {
        public BooksReservationContext()
        {
        }

        public BooksReservationContext(DbContextOptions<BooksReservationContext> options)
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
                    .HasName("PK__TBooks__2756CBDB26A3259B");

                entity.ToTable("TBooks");

                entity.Property(e => e.BitIsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DtimeCreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DtimeUpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IntStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.VarCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VarTitle)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Treservations>(entity =>
            {
                entity.HasKey(e => e.IdResevation)
                    .HasName("PK__TReserva__F88D1AE966B8851C");

                entity.ToTable("TReservations");

                entity.Property(e => e.BitIsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DtimeCreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DtimeDateReservation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DtimeUpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IntStatus).HasDefaultValueSql("((1))");

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
                    .HasName("PK__TUsers__B7C926383BB0E3D1");

                entity.ToTable("TUsers");

                entity.Property(e => e.BitIsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DtimeCreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DtimeUpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IntStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.VarEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VarFirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VarLastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VarPassword)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
