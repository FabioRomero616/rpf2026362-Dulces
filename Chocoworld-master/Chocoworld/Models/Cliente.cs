using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Chocoworld.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public string? Contrasena { get; set; }

    public string? Telefono { get; set; }

    public int? DireccionId { get; set; }

    public virtual ICollection<Carritodecompra> Carritodecompras { get; set; } = new List<Carritodecompra>();

    public virtual ICollection<Comentariosproducto> Comentariosproductos { get; set; } = new List<Comentariosproducto>();

    public virtual Direccione? Direccion { get; set; }

    public virtual ICollection<Metodospago> Metodospagos { get; set; } = new List<Metodospago>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    [NotMapped] // This attribute is used to indicate that ConfirmarContrasena is not mapped to a database column
    [Display(Name = "Confirmar Contraseña")]
    [Compare("Contrasena", ErrorMessage = "La confirmación de la contraseña no coincide")]
    public string ConfirmarContrasena { get; set; }
}
