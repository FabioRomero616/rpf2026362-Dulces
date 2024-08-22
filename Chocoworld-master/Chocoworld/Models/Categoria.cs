using System;
using System.Collections.Generic;

namespace Chocoworld.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    internal static object Where(Func<object, bool> value)
    {
        throw new NotImplementedException();
    }
}
