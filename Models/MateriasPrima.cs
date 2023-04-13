using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class MateriasPrima
{
    public int IdMateriaPrima { get; set; }

    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Formula> Formulas { get; } = new List<Formula>();

    public virtual ICollection<PreciosMateriasPrimasProveedore> PreciosMateriasPrimasProveedores { get; } = new List<PreciosMateriasPrimasProveedore>();

    public virtual ICollection<StockMateriasPrima> StockMateriasPrimas { get; } = new List<StockMateriasPrima>();
}
