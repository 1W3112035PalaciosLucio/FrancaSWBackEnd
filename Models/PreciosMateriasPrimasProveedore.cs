using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class PreciosMateriasPrimasProveedore
{
    public int IdPreciosMateriaPrimaProveedor { get; set; }

    public int IdProveedor { get; set; }

    public int IdMateriaPrima { get; set; }

    public DateTime FechaVigenciaDesde { get; set; }

    public DateTime FechaVigenciaHasta { get; set; }

    public decimal Precio { get; set; }

    public virtual MateriasPrima IdMateriaPrimaNavigation { get; set; } = null!;

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;
}
