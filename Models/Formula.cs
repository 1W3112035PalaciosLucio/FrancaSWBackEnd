using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class Formula
{
    public int IdFormula { get; set; }

    public int IdProducto { get; set; }

    public int IdMateriaPrima { get; set; }

    public decimal CantidadMateriaPrima { get; set; }

    public virtual MateriasPrima IdMateriaPrimaNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
