using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class MedidasProducto
{
    public int IdMedidaProducto { get; set; }

    public int Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
