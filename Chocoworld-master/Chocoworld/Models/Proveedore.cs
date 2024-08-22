using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Proveedore
{
    public int ProveedorId { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? NombreContacto { get; set; }

    public string? Telefono { get; set; }
}
