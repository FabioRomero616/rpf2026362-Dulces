using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Envio
{
    public int EnvioId { get; set; }

    public int? PedidoId { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public int? DireccionId { get; set; }

    public string? MetodoEnvio { get; set; }

    public string? EstadoEnvio { get; set; }

    public virtual Direccione? Direccion { get; set; }

    public virtual Pedido? Pedido { get; set; }
}
