using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class PreciosBocha
{
    public int IdPreciosBocha { get; set; }

    public DateTime FechaVigenciaDesde { get; set; }

    public DateTime FehcaVigenciaHasta { get; set; }

    public decimal Precio { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
