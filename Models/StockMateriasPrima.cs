using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class StockMateriasPrima
{
    public int IdStockMateriaPrima { get; set; }

    public int IdMateriaPrima { get; set; }

    public decimal Cantidad { get; set; }

    public decimal Precio { get; set; }

    public decimal StockMinimo { get; set; }

    public DateTime FechaUltimoPrecio { get; set; }

    public DateTime FechaUltimaActualizacion { get; set; }

    public decimal? StockInicial { get; set; }

    public virtual MateriasPrima IdMateriaPrimaNavigation { get; set; } = null!;
}
