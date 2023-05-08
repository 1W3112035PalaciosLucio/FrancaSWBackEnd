using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class HistorialXStockP
{
    public int IdHistorialPStockP { get; set; }

    public int IdHistorialProd { get; set; }

    public int IdStockProducto { get; set; }

    public virtual HistorialStockProducto IdHistorialProdNavigation { get; set; } = null!;

    public virtual StockProducto IdStockProductoNavigation { get; set; } = null!;
}
