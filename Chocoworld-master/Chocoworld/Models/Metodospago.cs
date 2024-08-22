using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Metodospago
{
    public int MetodoPagoId { get; set; }

    public int? ClienteId { get; set; }

    public string? NumeroTarjeta { get; set; }

    public string? FechaExpiracion { get; set; }

    public string? NombreTitular { get; set; }

    public virtual Cliente? Cliente { get; set; }
}
