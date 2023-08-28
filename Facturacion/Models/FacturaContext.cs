using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Models;

public partial class FacturaContext : DbContext
{
    public FacturaContext()
    {
    }

    public FacturaContext(DbContextOptions<FacturaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<FacturaCabecera> FacturaCabeceras { get; set; }

    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//    => optionsBuilder.UseSqlServer("Server=(local);Database=factura;User Id=sa;Password=Alejorb2001; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__E005FBFF1E37F55A");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente)
                .ValueGeneratedNever()
                .HasColumnName("ID_Cliente");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FacturaCabecera>(entity =>
        {
            entity.HasKey(e => e.IdFacturaCabecera).HasName("PK__FacturaC__E6D7B549E19F7D02");

            entity.ToTable("FacturaCabecera");

            entity.Property(e => e.IdFacturaCabecera)
                .ValueGeneratedNever()
                .HasColumnName("ID_FacturaCabecera");
            entity.Property(e => e.FechaFactura)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.FacturaCabeceras)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__FacturaCa__ID_Cl__4E88ABD4");
        });

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdFacturaDetalle).HasName("PK__FacturaD__0208DCFAB670E025");

            entity.ToTable("FacturaDetalle");

            entity.Property(e => e.IdFacturaDetalle)
                .ValueGeneratedNever()
                .HasColumnName("ID_FacturaDetalle");
            entity.Property(e => e.IdFacturaCabecera).HasColumnName("ID_FacturaCabecera");
            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdFacturaCabeceraNavigation).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.IdFacturaCabecera)
                .HasConstraintName("FK__FacturaDe__ID_Fa__5165187F");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__FacturaDe__ID_Pr__52593CB8");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodoPa__E2BFEC3E7521A50D");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.IdMetodoPago)
                .ValueGeneratedNever()
                .HasColumnName("ID_MetodoPago");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__9B4120E2714FAC9F");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto)
                .ValueGeneratedNever()
                .HasColumnName("ID_Producto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
