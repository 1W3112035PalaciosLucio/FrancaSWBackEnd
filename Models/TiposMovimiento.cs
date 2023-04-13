using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class TiposMovimiento
{
    public int IdTipoMovimiento { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<MovimientosProducto> MovimientosProductos { get; } = new List<MovimientosProducto>();
}
