using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Comentariosproducto
{
    public int ComentarioId { get; set; }

    public string? Comentario { get; set; }

    public int? Valoracion { get; set; }

    public DateTime? FechaComentario { get; set; }

    public int? ClienteId { get; set; }

    public int? ProductoId { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Producto? Producto { get; set; }
}
