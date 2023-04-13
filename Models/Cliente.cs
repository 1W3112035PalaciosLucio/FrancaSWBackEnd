using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Telefono { get; set; }

    public string Direccion { get; set; } = null!;

    public int IdLocalidad { get; set; }

    public virtual Localidade IdLocalidadNavigation { get; set; } = null!;

    public virtual ICollection<OrdenesProduccione> OrdenesProducciones { get; } = new List<OrdenesProduccione>();
}
