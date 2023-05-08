using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class HistorialStockMateriaPrima
{
    public int IdHistorial { get; set; }

    public decimal Cantidad { get; set; }

    public decimal Precio { get; set; }

    public DateTime FechaUltimaActualizacion { get; set; }

    public int? IdMateriaPrima { get; set; }

    public string? TipoMovimiento { get; set; }

    public virtual ICollection<HistorialXStockMp> HistorialXStockMps { get; } = new List<HistorialXStockMp>();
}
