using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Detallespedido
{
    public int DetalleId { get; set; }

    public int? PedidoId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public int? PromocionId { get; set; }

    public virtual Pedido? Pedido { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Promocione? Promocion { get; set; }
}
