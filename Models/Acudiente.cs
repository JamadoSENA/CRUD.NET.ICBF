using System;
using System.Collections.Generic;

namespace CrudICBF.Models;

public partial class Acudiente
{
    public int Cedula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public virtual ICollection<Ninio> Ninios { get; set; } = new List<Ninio>();
}
