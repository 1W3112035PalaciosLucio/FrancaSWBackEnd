using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class Localidade
{
    public int IdLocalidad { get; set; }

    public int IdProvincia { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; } = new List<Cliente>();

    public virtual Provincia IdProvinciaNavigation { get; set; } = null!;

    public virtual ICollection<Proveedore> Proveedores { get; } = new List<Proveedore>();
}
