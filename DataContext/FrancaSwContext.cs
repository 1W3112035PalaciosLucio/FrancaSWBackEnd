using System;
using System.Collections.Generic;
using FrancaSW.Models;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.DataContext;

public partial class FrancaSwContext : DbContext
{
    public FrancaSwContext()
    {
    }

    public FrancaSwContext(DbContextOptions<FrancaSwContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Catalogo> Catalogos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ColoresProducto> ColoresProductos { get; set; }

    public virtual DbSet<DetallesOrdenesProduccione> DetallesOrdenesProducciones { get; set; }

    public virtual DbSet<DiseniosProducto> DiseniosProductos { get; set; }

    public virtual DbSet<EstadosOrdenesProduccione> EstadosOrdenesProducciones { get; set; }

    public virtual DbSet<Formula> Formulas { get; set; }

    public virtual DbSet<Localidade> Localidades { get; set; }

    public virtual DbSet<MateriasPrima> MateriasPrimas { get; set; }

    public virtual DbSet<MedidasProducto> MedidasProductos { get; set; }

    public virtual DbSet<MovimientosProducto> MovimientosProductos { get; set; }

    public virtual DbSet<OrdenesProduccione> OrdenesProducciones { get; set; }

    public virtual DbSet<PreciosBocha> PreciosBochas { get; set; }

    public virtual DbSet<PreciosMateriasPrimasProveedore> PreciosMateriasPrimasProveedores { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesUsuario> RolesUsuarios { get; set; }

    public virtual DbSet<StockMateriasPrima> StockMateriasPrimas { get; set; }

    public virtual DbSet<StockProducto> StockProductos { get; set; }

    public virtual DbSet<TiposMovimiento> TiposMovimientos { get; set; }

    public virtual DbSet<TiposProducto> TiposProductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-HFJMQO3\\SQLEXPRESS;Initial Catalog=FrancaSW; Trusted_Connection=true; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catalogo>(entity =>
        {
            entity.HasKey(e => e.IdCatalogo);

            entity.Property(e => e.IdCatalogo).HasColumnName("Id_catalogo");
            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.IdProducto).HasColumnName("Id_producto");
            entity.Property(e => e.Imagen).HasColumnType("image");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Catalogos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Catalogos_Productos");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.Property(e => e.IdCliente).HasColumnName("Id_cliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdLocalidad).HasColumnName("Id_localidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdLocalidadNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdLocalidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Localidades");
        });

        modelBuilder.Entity<ColoresProducto>(entity =>
        {
            entity.HasKey(e => e.IdColorProducto);

            entity.ToTable("Colores_Productos");

            entity.Property(e => e.IdColorProducto).HasColumnName("Id_color_producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetallesOrdenesProduccione>(entity =>
        {
            entity.HasKey(e => e.IdDetalleOrdenProduccion);

            entity.ToTable("Detalles_Ordenes_Producciones");

            entity.Property(e => e.IdDetalleOrdenProduccion).HasColumnName("Id_detalle_orden_produccion");
            entity.Property(e => e.IdOrdenProduccion).HasColumnName("Id_orden_produccion");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdOrdenProduccionNavigation).WithMany(p => p.DetallesOrdenesProducciones)
                .HasForeignKey(d => d.IdOrdenProduccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalles_Ordenes_Producciones_Ordenes_Producciones");
        });

        modelBuilder.Entity<DiseniosProducto>(entity =>
        {
            entity.HasKey(e => e.IdDisenio).HasName("PK_Diseños_Productos");

            entity.ToTable("Disenios_Productos");

            entity.Property(e => e.IdDisenio).HasColumnName("Id_disenio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadosOrdenesProduccione>(entity =>
        {
            entity.HasKey(e => e.IdEstadoOrdenProduccion);

            entity.ToTable("Estados_Ordenes_Producciones");

            entity.Property(e => e.IdEstadoOrdenProduccion).HasColumnName("Id_estado_orden_produccion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Formula>(entity =>
        {
            entity.HasKey(e => e.IdFormula);

            entity.Property(e => e.IdFormula).HasColumnName("Id_formula");
            entity.Property(e => e.CantidadMateriaPrima)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Cantidad_materia_prima");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("Id_materia_prima");
            entity.Property(e => e.IdProducto).HasColumnName("Id_producto");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.Formulas)
                .HasForeignKey(d => d.IdMateriaPrima)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Formulas_Materias_Primas");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Formulas)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Formulas_Productos");
        });

        modelBuilder.Entity<Localidade>(entity =>
        {
            entity.HasKey(e => e.IdLocalidad);

            entity.Property(e => e.IdLocalidad)
                .ValueGeneratedNever()
                .HasColumnName("Id_localidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdProvincia).HasColumnName("Id_provincia");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Localidades)
                .HasForeignKey(d => d.IdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Localidades_Provincias");
        });

        modelBuilder.Entity<MateriasPrima>(entity =>
        {
            entity.HasKey(e => e.IdMateriaPrima);

            entity.ToTable("Materias_Primas");

            entity.Property(e => e.IdMateriaPrima).HasColumnName("Id_materia_prima");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MedidasProducto>(entity =>
        {
            entity.HasKey(e => e.IdMedidaProducto);

            entity.ToTable("Medidas_Productos");

            entity.Property(e => e.IdMedidaProducto).HasColumnName("Id_medida_producto");
        });

        modelBuilder.Entity<MovimientosProducto>(entity =>
        {
            entity.HasKey(e => e.IdMovimientoProducto);

            entity.ToTable("Movimientos_Productos");

            entity.Property(e => e.IdMovimientoProducto).HasColumnName("Id_movimiento_producto");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FechaEntrada)
                .HasColumnType("date")
                .HasColumnName("Fecha_entrada");
            entity.Property(e => e.IdProducto).HasColumnName("Id_producto");
            entity.Property(e => e.IdTipoMovimiento).HasColumnName("Id_tipo_movimiento");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.MovimientosProductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Productos_Productos");

            entity.HasOne(d => d.IdTipoMovimientoNavigation).WithMany(p => p.MovimientosProductos)
                .HasForeignKey(d => d.IdTipoMovimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Productos_Tipos_Movimientos");
        });

        modelBuilder.Entity<OrdenesProduccione>(entity =>
        {
            entity.HasKey(e => e.IdOrdenProduccion);

            entity.ToTable("Ordenes_Producciones");

            entity.Property(e => e.IdOrdenProduccion).HasColumnName("Id_orden_produccion");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("date")
                .HasColumnName("Fecha_entrega");
            entity.Property(e => e.FechaPedido)
                .HasColumnType("date")
                .HasColumnName("Fecha_pedido");
            entity.Property(e => e.IdCliente).HasColumnName("Id_cliente");
            entity.Property(e => e.IdEstadoOrdenProduccion).HasColumnName("Id_estado_orden_produccion");
            entity.Property(e => e.IdProducto).HasColumnName("Id_producto");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.NumeroOrden).HasColumnName("Numero_orden");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.OrdenesProducciones)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Producciones_Clientes");

            entity.HasOne(d => d.IdEstadoOrdenProduccionNavigation).WithMany(p => p.OrdenesProducciones)
                .HasForeignKey(d => d.IdEstadoOrdenProduccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Producciones_Estados_Ordenes_Producciones");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.OrdenesProducciones)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Producciones_Productos");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.OrdenesProducciones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Producciones_Usuarios");
        });

        modelBuilder.Entity<PreciosBocha>(entity =>
        {
            entity.HasKey(e => e.IdPreciosBocha);

            entity.ToTable("Precios_Bochas");

            entity.Property(e => e.IdPreciosBocha).HasColumnName("Id_precios_bocha");
            entity.Property(e => e.FechaVigenciaDesde)
                .HasColumnType("date")
                .HasColumnName("Fecha_vigencia_desde");
            entity.Property(e => e.FehcaVigenciaHasta)
                .HasColumnType("date")
                .HasColumnName("Fehca_vigencia_hasta");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<PreciosMateriasPrimasProveedore>(entity =>
        {
            entity.HasKey(e => e.IdPreciosMateriaPrimaProveedor);

            entity.ToTable("Precios_MateriasPrimas_Proveedores");

            entity.Property(e => e.IdPreciosMateriaPrimaProveedor).HasColumnName("Id_precios_materia_prima_proveedor");
            entity.Property(e => e.FechaVigenciaDesde)
                .HasColumnType("date")
                .HasColumnName("Fecha_vigencia_desde");
            entity.Property(e => e.FechaVigenciaHasta)
                .HasColumnType("date")
                .HasColumnName("Fecha_vigencia_hasta");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("Id_materia_prima");
            entity.Property(e => e.IdProveedor).HasColumnName("Id_proveedor");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.PreciosMateriasPrimasProveedores)
                .HasForeignKey(d => d.IdMateriaPrima)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Precios_MateriasPrimas_Proveedores_Materias_Primas");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.PreciosMateriasPrimasProveedores)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Precios_MateriasPrimas_Proveedores_Proveedores");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.Property(e => e.IdProducto).HasColumnName("Id_producto");
            entity.Property(e => e.IdColorProducto).HasColumnName("Id_color_producto");
            entity.Property(e => e.IdDisenioProducto).HasColumnName("Id_disenio_producto");
            entity.Property(e => e.IdMedidaProducto).HasColumnName("Id_medida_producto");
            entity.Property(e => e.IdPrecioBocha).HasColumnName("Id_precio_bocha");
            entity.Property(e => e.IdTipoProducto).HasColumnName("Id_tipo_producto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdColorProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdColorProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Colores_Productos");

            entity.HasOne(d => d.IdDisenioProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdDisenioProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Diseños_Productos");

            entity.HasOne(d => d.IdMedidaProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMedidaProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Medidas_Productos");

            entity.HasOne(d => d.IdPrecioBochaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdPrecioBocha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Precios_Bochas");

            entity.HasOne(d => d.IdTipoProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTipoProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Tipos_Productos");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor);

            entity.Property(e => e.IdProveedor).HasColumnName("Id_proveedor");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdLocalidad).HasColumnName("Id_localidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdLocalidadNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdLocalidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedores_Localidades");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia);

            entity.Property(e => e.IdProvincia)
                .ValueGeneratedNever()
                .HasColumnName("Id_provincia");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).IsClustered(false);

            entity.Property(e => e.IdRol).HasColumnName("Id_rol");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolesUsuario>(entity =>
        {
            entity.HasKey(e => e.IdRolUsuario);

            entity.ToTable("Roles_Usuarios");

            entity.Property(e => e.IdRolUsuario).HasColumnName("Id_rol_usuario");
            entity.Property(e => e.IdRol).HasColumnName("Id_rol");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolesUsuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Usuarios_Roles");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.RolesUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Usuarios_Usuarios");
        });

        modelBuilder.Entity<StockMateriasPrima>(entity =>
        {
            entity.HasKey(e => e.IdStockMateriaPrima);

            entity.ToTable("Stock_Materias_Primas");

            entity.Property(e => e.IdStockMateriaPrima).HasColumnName("Id_stock_materia_prima");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FechaUltimaActualizacion)
                .HasColumnType("date")
                .HasColumnName("Fecha_ultima_actualizacion");
            entity.Property(e => e.FechaUltimoPrecio)
                .HasColumnType("date")
                .HasColumnName("Fecha_ultimo_precio");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("Id_materia_prima");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockInicial)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Stock_inicial");
            entity.Property(e => e.StockMinimo)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Stock_minimo");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.StockMateriasPrimas)
                .HasForeignKey(d => d.IdMateriaPrima)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Materias_Primas_Materias_Primas");
        });

        modelBuilder.Entity<StockProducto>(entity =>
        {
            entity.HasKey(e => e.IdStockProducto);

            entity.ToTable("Stock_Productos");

            entity.Property(e => e.IdStockProducto).HasColumnName("Id_stock_producto");
            entity.Property(e => e.IdProducto).HasColumnName("Id_producto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.StockProductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Productos_Productos");
        });

        modelBuilder.Entity<TiposMovimiento>(entity =>
        {
            entity.HasKey(e => e.IdTipoMovimiento);

            entity.ToTable("Tipos_Movimientos");

            entity.Property(e => e.IdTipoMovimiento).HasColumnName("Id_tipo_movimiento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TiposProducto>(entity =>
        {
            entity.HasKey(e => e.IdTipoProducto);

            entity.ToTable("Tipos_Productos");

            entity.Property(e => e.IdTipoProducto).HasColumnName("Id_tipo_producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HashPassword).HasColumnName("Hash_password");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
