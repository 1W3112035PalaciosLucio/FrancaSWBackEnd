using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class HistorialStockProducto
{
    public int IdHistorialProd { get; set; }

    public int Cantidad { get; set; }

    public DateTime FechaUltimaActualizacion { get; set; }

    public int IdProducto { get; set; }

    public string? TipoMovimiento { get; set; }

    public virtual ICollection<HistorialXStockP> HistorialXStockPs { get; } = new List<HistorialXStockP>();
}
