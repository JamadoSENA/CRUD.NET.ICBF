using System;
using System.Collections.Generic;

namespace CrudICBF.Models;

public partial class Registroavanceacademico
{
    public int RegistroAvance { get; set; }

    public int AnioEscolar { get; set; }

    public string Nivel { get; set; } = null!;

    public string Notas { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateOnly FechaEntregaNota { get; set; }

    public int IdentificacionNino { get; set; }

    public virtual Ninio IdentificacionNinoNavigation { get; set; } = null!;
}
