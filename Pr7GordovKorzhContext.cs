using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ISIP523_Korzh;

public partial class Pr7GordovKorzhContext : DbContext
{
    public Pr7GordovKorzhContext()
    {
    }

    public Pr7GordovKorzhContext(DbContextOptions<Pr7GordovKorzhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Spare> Spares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KORZH;Initial Catalog=Pr7_Gordov_Korzh;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.BrokenPartId).HasColumnName("BrokenPartID");
            entity.Property(e => e.CarModel).HasMaxLength(50);
            entity.Property(e => e.FinalProfit).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RepairCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ServiceId)
                .HasDefaultValue(1)
                .HasColumnName("ServiceID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UsedPartId).HasColumnName("UsedPartID");

            entity.HasOne(d => d.Service).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Service");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.ServiceId)
                .HasDefaultValue(1)
                .HasColumnName("ServiceID");
            entity.Property(e => e.Balance)
                .HasDefaultValueSql("((1000.00))")
                .HasColumnType("decimal(15, 2)");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Spare>(entity =>
        {
            entity.HasIndex(e => e.SpareName, "UQ__Spares__972E1B9630A5F4D8").IsUnique();

            entity.Property(e => e.SpareId).HasColumnName("SpareID");
            entity.Property(e => e.MinimumStockLevel).HasDefaultValue(1);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RepairMarkup)
                .HasDefaultValueSql("((1.5))")
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ServiceId)
                .HasDefaultValue(1)
                .HasColumnName("ServiceID");
            entity.Property(e => e.SpareName).HasMaxLength(50);

            entity.HasOne(d => d.Service).WithMany(p => p.Spares)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Spares_Service");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
