using System;
using System.Collections.Generic;

namespace CrudICBF.Models;

public partial class Madrecom
{
    public int Cedula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public virtual ICollection<Ninio> Ninios { get; set; } = new List<Ninio>();
}
