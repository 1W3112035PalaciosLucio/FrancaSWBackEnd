using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class Role
{
    public int IdRol { get; set; }

    public string? Codigo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<RolesUsuario> RolesUsuarios { get; } = new List<RolesUsuario>();
}
