using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class DetallesOrdenesProduccione
{
    public int IdDetalleOrdenProduccion { get; set; }

    public int IdOrdenProduccion { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public virtual OrdenesProduccione IdOrdenProduccionNavigation { get; set; } = null!;
}
