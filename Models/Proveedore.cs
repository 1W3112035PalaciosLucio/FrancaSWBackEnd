using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public decimal Telefono { get; set; }

    public int IdLocalidad { get; set; }

    public bool? Activo { get; set; }

    public virtual Localidade IdLocalidadNavigation { get; set; } = null!;

    public virtual ICollection<PreciosMateriasPrimasProveedore> PreciosMateriasPrimasProveedores { get; } = new List<PreciosMateriasPrimasProveedore>();
}
