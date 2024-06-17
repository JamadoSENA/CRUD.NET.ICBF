using System;
using System.Collections.Generic;

namespace CrudICBF.Models;

public partial class Ninio
{
    public int RegistroNiup { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string TipoSangre { get; set; } = null!;

    public string CiudadNacimiento { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Eps { get; set; } = null!;

    public int IdentificacionAcudiente { get; set; }

    public int IdentificacionMadreCom { get; set; }

    public int IdentificadorJardin { get; set; }

    public virtual Acudiente IdentificacionAcudienteNavigation { get; set; } = null!;

    public virtual Madrecom IdentificacionMadreComNavigation { get; set; } = null!;

    public virtual Jardin IdentificadorJardinNavigation { get; set; } = null!;

    public virtual ICollection<Registroasistencium> Registroasistencia { get; set; } = new List<Registroasistencium>();

    public virtual ICollection<Registroavanceacademico> Registroavanceacademicos { get; set; } = new List<Registroavanceacademico>();
}
