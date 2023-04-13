using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class MovimientosProducto
{
    public int IdMovimientoProducto { get; set; }

    public int IdProducto { get; set; }

    public int IdTipoMovimiento { get; set; }

    public DateTime FechaEntrada { get; set; }

    public decimal Cantidad { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual TiposMovimiento IdTipoMovimientoNavigation { get; set; } = null!;
}
