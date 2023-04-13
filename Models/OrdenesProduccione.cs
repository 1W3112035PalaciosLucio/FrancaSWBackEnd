using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class OrdenesProduccione
{
    public int IdOrdenProduccion { get; set; }

    public int IdCliente { get; set; }

    public int IdProducto { get; set; }

    public int IdUsuario { get; set; }

    public int IdEstadoOrdenProduccion { get; set; }

    public DateTime FechaPedido { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public int NumeroOrden { get; set; }

    public int? Cantidad { get; set; }

    public virtual ICollection<DetallesOrdenesProduccione> DetallesOrdenesProducciones { get; } = new List<DetallesOrdenesProduccione>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual EstadosOrdenesProduccione IdEstadoOrdenProduccionNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
