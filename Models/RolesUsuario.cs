using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class RolesUsuario
{
    public int IdRolUsuario { get; set; }

    public int IdRol { get; set; }

    public int IdUsuario { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
