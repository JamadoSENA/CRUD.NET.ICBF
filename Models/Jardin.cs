using System;
using System.Collections.Generic;

namespace CrudICBF.Models;

public partial class Jardin
{
    public int IdentificadorJardin { get; set; }

    public string NombreJardin { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Ninio> Ninios { get; set; } = new List<Ninio>();
}
