using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class HistorialXStockMp
{
    public int IdHistorialMpStockMp { get; set; }

    public int IdHistorial { get; set; }

    public int IdStockMateriaPrima { get; set; }

    public virtual HistorialStockMateriaPrima IdHistorialNavigation { get; set; } = null!;

    public virtual StockMateriasPrima IdStockMateriaPrimaNavigation { get; set; } = null!;
}
