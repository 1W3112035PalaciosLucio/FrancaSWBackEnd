using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public bool Activo { get; set; }

    public string Email { get; set; } = null!;

    public decimal Telefono { get; set; }

    public byte[] HashPassword { get; set; } = null!;

    public virtual ICollection<OrdenesProduccione> OrdenesProducciones { get; } = new List<OrdenesProduccione>();

    public virtual ICollection<RolesUsuario> RolesUsuarios { get; } = new List<RolesUsuario>();
}
