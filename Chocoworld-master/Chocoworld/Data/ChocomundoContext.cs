using System;
using System.Collections.Generic;
using Chocoworld.Models;
using Microsoft.EntityFrameworkCore;

namespace Chocoworld.Data;

public partial class ChocomundoContext : DbContext
{
    public ChocomundoContext()
    {
    }

    public ChocomundoContext(DbContextOptions<ChocomundoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carritodecompra> Carritodecompras { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Comentariosproducto> Comentariosproductos { get; set; }

    public virtual DbSet<Detallespedido> Detallespedidos { get; set; }

    public virtual DbSet<Direccione> Direcciones { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Envio> Envios { get; set; }

    public virtual DbSet<Imagene> Imagenes { get; set; }

    public virtual DbSet<Lote> Lotes { get; set; }

    public virtual DbSet<Metodospago> Metodospagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Promocione> Promociones { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=chocomundo;user=root", ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Carritodecompra>(entity =>
        {
            entity.HasKey(e => e.CarritoId).HasName("PRIMARY");

            entity.ToTable("carritodecompras");

            entity.HasIndex(e => e.ClienteId, "ClienteID");

            entity.HasIndex(e => e.ProductoId, "ProductoID");

            entity.Property(e => e.CarritoId)
                .HasColumnType("int(11)")
                .HasColumnName("CarritoID");
            entity.Property(e => e.Cantidad).HasColumnType("int(11)");
            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("ClienteID");
            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("ProductoID");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Carritodecompras)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("carritodecompras_ibfk_1");

            entity.HasOne(d => d.Producto).WithMany(p => p.Carritodecompras)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("carritodecompras_ibfk_2");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.Property(e => e.CategoriaId)
                .HasColumnType("int(11)")
                .HasColumnName("CategoriaID");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.DireccionId, "DireccionID");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("ClienteID");
            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Contrasena).HasMaxLength(50);
            entity.Property(e => e.DireccionId)
                .HasColumnType("int(11)")
                .HasColumnName("DireccionID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.Direccion).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.DireccionId)
                .HasConstraintName("clientes_ibfk_1");
        });

        modelBuilder.Entity<Comentariosproducto>(entity =>
        {
            entity.HasKey(e => e.ComentarioId).HasName("PRIMARY");

            entity.ToTable("comentariosproductos");

            entity.HasIndex(e => e.ClienteId, "ClienteID");

            entity.HasIndex(e => e.ProductoId, "ProductoID");

            entity.Property(e => e.ComentarioId)
                .HasColumnType("int(11)")
                .HasColumnName("ComentarioID");
            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("ClienteID");
            entity.Property(e => e.Comentario).HasColumnType("text");
            entity.Property(e => e.FechaComentario).HasColumnType("datetime");
            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("ProductoID");
            entity.Property(e => e.Valoracion).HasColumnType("int(11)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Comentariosproductos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("comentariosproductos_ibfk_2");

            entity.HasOne(d => d.Producto).WithMany(p => p.Comentariosproductos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("comentariosproductos_ibfk_1");
        });

        modelBuilder.Entity<Detallespedido>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PRIMARY");

            entity.ToTable("detallespedido");

            entity.HasIndex(e => e.PedidoId, "PedidoID");

            entity.HasIndex(e => e.ProductoId, "ProductoID");

            entity.HasIndex(e => e.PromocionId, "PromocionID");

            entity.Property(e => e.DetalleId)
                .HasColumnType("int(11)")
                .HasColumnName("DetalleID");
            entity.Property(e => e.Cantidad).HasColumnType("int(11)");
            entity.Property(e => e.PedidoId)
                .HasColumnType("int(11)")
                .HasColumnName("PedidoID");
            entity.Property(e => e.PrecioUnitario).HasPrecision(10, 2);
            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("ProductoID");
            entity.Property(e => e.PromocionId)
                .HasColumnType("int(11)")
                .HasColumnName("PromocionID");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Detallespedidos)
                .HasForeignKey(d => d.PedidoId)
                .HasConstraintName("detallespedido_ibfk_1");

            entity.HasOne(d => d.Producto).WithMany(p => p.Detallespedidos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("detallespedido_ibfk_3");

            entity.HasOne(d => d.Promocion).WithMany(p => p.Detallespedidos)
                .HasForeignKey(d => d.PromocionId)
                .HasConstraintName("detallespedido_ibfk_2");
        });

        modelBuilder.Entity<Direccione>(entity =>
        {
            entity.HasKey(e => e.DireccionId).HasName("PRIMARY");

            entity.ToTable("direcciones");

            entity.Property(e => e.DireccionId)
                .HasColumnType("int(11)")
                .HasColumnName("DireccionID");
            entity.Property(e => e.Calle).HasMaxLength(255);
            entity.Property(e => e.Ciudad).HasMaxLength(50);
            entity.Property(e => e.Zona).HasMaxLength(50);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PRIMARY");

            entity.ToTable("empleados");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.EmpleadoId)
                .HasColumnType("int(11)")
                .HasColumnName("EmpleadoID");
            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Contrasena).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Rol).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.HasKey(e => e.EnvioId).HasName("PRIMARY");

            entity.ToTable("envios");

            entity.HasIndex(e => e.DireccionId, "DireccionID");

            entity.HasIndex(e => e.PedidoId, "PedidoID");

            entity.Property(e => e.EnvioId)
                .HasColumnType("int(11)")
                .HasColumnName("EnvioID");
            entity.Property(e => e.DireccionId)
                .HasColumnType("int(11)")
                .HasColumnName("DireccionID");
            entity.Property(e => e.EstadoEnvio).HasMaxLength(20);
            entity.Property(e => e.FechaEnvio).HasColumnType("datetime");
            entity.Property(e => e.MetodoEnvio).HasMaxLength(50);
            entity.Property(e => e.PedidoId)
                .HasColumnType("int(11)")
                .HasColumnName("PedidoID");

            entity.HasOne(d => d.Direccion).WithMany(p => p.Envios)
                .HasForeignKey(d => d.DireccionId)
                .HasConstraintName("envios_ibfk_2");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Envios)
                .HasForeignKey(d => d.PedidoId)
                .HasConstraintName("envios_ibfk_1");
        });

        modelBuilder.Entity<Imagene>(entity =>
        {
            entity.HasKey(e => e.ImagenId).HasName("PRIMARY");

            entity.ToTable("imagenes");

            entity.HasIndex(e => e.ProductoId, "ProductoID");

            entity.Property(e => e.ImagenId)
                .HasColumnType("int(11)")
                .HasColumnName("ImagenID");
            entity.Property(e => e.Extension).HasMaxLength(5);
            entity.Property(e => e.Nombre).HasMaxLength(10);
            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("ProductoID");
            entity.Property(e => e.Ruta).HasMaxLength(255);

            entity.HasOne(d => d.Producto).WithMany(p => p.Imagenes)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("imagenes_ibfk_1");
        });

        modelBuilder.Entity<Lote>(entity =>
        {
            entity.HasKey(e => e.LoteId).HasName("PRIMARY");

            entity.ToTable("lotes");

            entity.HasIndex(e => e.ProductoId, "ProductoID");

            entity.Property(e => e.LoteId)
                .HasColumnType("int(11)")
                .HasColumnName("LoteID");
            entity.Property(e => e.Cantidad).HasColumnType("int(11)");
            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("ProductoID");
            entity.Property(e => e.ProveedorId)
                .HasColumnType("int(11)")
                .HasColumnName("ProveedorID");

            entity.HasOne(d => d.Producto).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("lotes_ibfk_1");
        });

        modelBuilder.Entity<Metodospago>(entity =>
        {
            entity.HasKey(e => e.MetodoPagoId).HasName("PRIMARY");

            entity.ToTable("metodospago");

            entity.HasIndex(e => e.ClienteId, "ClienteID");

            entity.Property(e => e.MetodoPagoId)
                .HasColumnType("int(11)")
                .HasColumnName("MetodoPagoID");
            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("ClienteID");
            entity.Property(e => e.FechaExpiracion).HasMaxLength(10);
            entity.Property(e => e.NombreTitular).HasMaxLength(100);
            entity.Property(e => e.NumeroTarjeta).HasMaxLength(16);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Metodospagos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("metodospago_ibfk_1");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedidoId).HasName("PRIMARY");

            entity.ToTable("pedidos");

            entity.HasIndex(e => e.ClienteId, "ClienteID");

            entity.HasIndex(e => e.EmpleadoId, "EmpleadoID");

            entity.Property(e => e.PedidoId)
                .HasColumnType("int(11)")
                .HasColumnName("PedidoID");
            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("ClienteID");
            entity.Property(e => e.EmpleadoId)
                .HasColumnType("int(11)")
                .HasColumnName("EmpleadoID");
            entity.Property(e => e.EstadoPedido).HasMaxLength(20);
            entity.Property(e => e.FechaPedido).HasColumnType("datetime");
            entity.Property(e => e.TipoPago).HasMaxLength(10);
            entity.Property(e => e.Total).HasPrecision(10, 2);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("pedidos_ibfk_1");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.EmpleadoId)
                .HasConstraintName("pedidos_ibfk_2");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.HasIndex(e => e.CategoriaId, "CategoriaID");

            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("ProductoID");
            entity.Property(e => e.CategoriaId)
                .HasColumnType("int(11)")
                .HasColumnName("CategoriaID");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasPrecision(10, 2);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("productos_ibfk_1");
        });

        modelBuilder.Entity<Promocione>(entity =>
        {
            entity.HasKey(e => e.PromocionId).HasName("PRIMARY");

            entity.ToTable("promociones");

            entity.Property(e => e.PromocionId)
                .HasColumnType("int(11)")
                .HasColumnName("PromocionID");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Descuento).HasPrecision(5, 2);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.ProveedorId).HasName("PRIMARY");

            entity.ToTable("proveedores");

            entity.Property(e => e.ProveedorId)
                .HasColumnType("int(11)")
                .HasColumnName("ProveedorID");
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.NombreContacto).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
