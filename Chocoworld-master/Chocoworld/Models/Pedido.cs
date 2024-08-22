using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Pedido
{
    public int PedidoId { get; set; }

    public int? ClienteId { get; set; }

    public int? EmpleadoId { get; set; }

    public DateTime? FechaPedido { get; set; }

    public string? EstadoPedido { get; set; }

    public decimal? Total { get; set; }

    public string? TipoPago { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Detallespedido> Detallespedidos { get; set; } = new List<Detallespedido>();

    public virtual Empleado? Empleado { get; set; }

    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
}
