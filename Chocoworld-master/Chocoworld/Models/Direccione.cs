using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Direccione
{
    public int DireccionId { get; set; }

    public string? Calle { get; set; }

    public string? Zona { get; set; }

    public string? Ciudad { get; set; }

    public int? ClienteId { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
}
