using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class StockProducto
{
    public int IdStockProducto { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
