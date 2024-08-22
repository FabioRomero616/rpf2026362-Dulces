using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? CategoriaId { get; set; }

    public virtual ICollection<Carritodecompra> Carritodecompras { get; set; } = new List<Carritodecompra>();

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<Comentariosproducto> Comentariosproductos { get; set; } = new List<Comentariosproducto>();

    public virtual ICollection<Detallespedido> Detallespedidos { get; set; } = new List<Detallespedido>();

    public virtual ICollection<Imagene> Imagenes { get; set; } = new List<Imagene>();

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();
}
