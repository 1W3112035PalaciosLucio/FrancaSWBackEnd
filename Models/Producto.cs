using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int Codigo { get; set; }

    public int IdTipoProducto { get; set; }

    public int IdColorProducto { get; set; }

    public int IdMedidaProducto { get; set; }

    public int IdPrecioBocha { get; set; }

    public int IdDisenioProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Catalogo> Catalogos { get; } = new List<Catalogo>();

    public virtual ICollection<Formula> Formulas { get; } = new List<Formula>();

    public virtual ColoresProducto IdColorProductoNavigation { get; set; } = null!;

    public virtual DiseniosProducto IdDisenioProductoNavigation { get; set; } = null!;

    public virtual MedidasProducto IdMedidaProductoNavigation { get; set; } = null!;

    public virtual PreciosBocha IdPrecioBochaNavigation { get; set; } = null!;

    public virtual TiposProducto IdTipoProductoNavigation { get; set; } = null!;

    public virtual ICollection<MovimientosProducto> MovimientosProductos { get; } = new List<MovimientosProducto>();

    public virtual ICollection<OrdenesProduccione> OrdenesProducciones { get; } = new List<OrdenesProduccione>();

    public virtual ICollection<StockProducto> StockProductos { get; } = new List<StockProducto>();
}
