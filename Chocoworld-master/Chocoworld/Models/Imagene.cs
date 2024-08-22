using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Imagene
{
    public int ImagenId { get; set; }

    public string? Nombre { get; set; }

    public string? Ruta { get; set; }

    public string? Extension { get; set; }

    public int? ProductoId { get; set; }

    public virtual Producto? Producto { get; set; }
}
