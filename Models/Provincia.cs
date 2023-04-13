using System;
using System.Collections.Generic;

namespace FrancaSW.Models;

public partial class Provincia
{
    public int IdProvincia { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Localidade> Localidades { get; } = new List<Localidade>();
}
