using System;
using System.Collections.Generic;

namespace CrudICBF.Models;

public partial class Registroasistencium
{
    public int RegistroAsistencia { get; set; }

    public DateOnly Fecha { get; set; }

    public string Estado { get; set; } = null!;

    public int IdentificacionNino { get; set; }

    public virtual Ninio IdentificacionNinoNavigation { get; set; } = null!;
}
