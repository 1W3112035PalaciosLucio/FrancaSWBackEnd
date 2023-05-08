using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class StockProducto
{
    public int IdStockProducto { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public DateTime FechaUltimaActualizacion { get; set; }

    public virtual ICollection<HistorialXStockP> HistorialXStockPs { get; } = new List<HistorialXStockP>();

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
