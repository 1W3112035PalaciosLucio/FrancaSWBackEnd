using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class TiposProducto
{
    public int IdTipoProducto { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
