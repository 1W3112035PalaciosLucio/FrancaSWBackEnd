﻿using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class Catalogo
{
    public int IdCatalogo { get; set; }

    public int IdProducto { get; set; }

    public string Descripcion { get; set; } = null!;

    public byte[] Imagen { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
