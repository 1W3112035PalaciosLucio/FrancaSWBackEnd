using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class EstadosOrdenesProduccione
{
    public int IdEstadoOrdenProduccion { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<OrdenesProduccione> OrdenesProducciones { get; } = new List<OrdenesProduccione>();
}
