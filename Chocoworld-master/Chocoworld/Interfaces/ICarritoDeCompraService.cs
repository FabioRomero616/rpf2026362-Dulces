using Chocoworld.Data;
using Chocoworld.Models;
using Microsoft.EntityFrameworkCore;

namespace Chocoworld.Interfaces
{
    public interface ICarritoDeCompraService
    {
        List<Carritodecompra> ObtenerCarrito(int clienteId);
        void AgregarAlCarrito(int clienteId, int productoId, int cantidad);
        void EliminarDelCarrito(int clienteId, int productoId);
        void VaciarCarrito(int clienteId);
    }

    public class CarritoDeCompraService : ICarritoDeCompraService
    {
        private readonly ChocomundoContext _context;

        public CarritoDeCompraService(ChocomundoContext context)
        {
            _context = context;
        }

        public List<Carritodecompra> ObtenerCarrito(int clienteId)
        {
            return _context.Carritodecompras
                .Where(c => c.ClienteId == clienteId)
                .Include(c => c.Producto)
                .ToList();
        }

        public void AgregarAlCarrito(int clienteId, int productoId, int cantidad)
        {
            // Verificar si el producto ya está en el carrito del cliente
            var carritoExistente = _context.Carritodecompras
                .FirstOrDefault(c => c.ClienteId == clienteId && c.ProductoId == productoId);

            if (carritoExistente != null)
            {
                // Actualizar la cantidad si el producto ya está en el carrito
                carritoExistente.Cantidad += cantidad;
            }
            else
            {
                // Agregar nuevo elemento al carrito si no existe
                var nuevoCarritoItem = new Carritodecompra
                {
                    ClienteId = clienteId,
                    ProductoId = productoId,
                    Cantidad = cantidad
                };
                _context.Carritodecompras.Add(nuevoCarritoItem);
            }

            _context.SaveChanges();
        }

        public void EliminarDelCarrito(int clienteId, int productoId)
        {
            var carritoItem = _context.Carritodecompras
                .FirstOrDefault(c => c.ClienteId == clienteId && c.ProductoId == productoId);

            if (carritoItem != null)
            {
                _context.Carritodecompras.Remove(carritoItem);
                _context.SaveChanges();
            }
        }

        public void VaciarCarrito(int clienteId)
        {
            var carritoItems = _context.Carritodecompras
                .Where(c => c.ClienteId == clienteId)
                .ToList();

            _context.Carritodecompras.RemoveRange(carritoItems);
            _context.SaveChanges();
        }
    }

}
