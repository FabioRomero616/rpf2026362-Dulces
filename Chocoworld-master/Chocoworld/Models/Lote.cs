using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Lote
{
    public int LoteId { get; set; }

    public int? ProductoId { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public int? Cantidad { get; set; }

    public int? ProveedorId { get; set; }

    public virtual Producto? Producto { get; set; }
}
