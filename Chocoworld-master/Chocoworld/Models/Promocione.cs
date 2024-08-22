using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Promocione
{
    public int PromocionId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public decimal? Descuento { get; set; }

    public virtual ICollection<Detallespedido> Detallespedidos { get; set; } = new List<Detallespedido>();
}
