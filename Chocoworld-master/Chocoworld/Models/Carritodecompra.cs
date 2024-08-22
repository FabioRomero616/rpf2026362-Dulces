using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Carritodecompra
{
    public int CarritoId { get; set; }

    public int? ClienteId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Producto? Producto { get; set; }
}
