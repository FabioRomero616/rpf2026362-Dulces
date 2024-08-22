using Chocoworld.Data;
using Chocoworld.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chocoworld.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly ICarritoDeCompraService _carritoService;
        private readonly ChocomundoContext _context;

        public CarritoController(ICarritoDeCompraService carritoService)
        {
            _carritoService = carritoService;
        }
        
        public IActionResult VerCarrito(int clienteId)
        {
            var carrito = _carritoService.ObtenerCarrito(clienteId);
            return View(carrito);
        }

        public IActionResult AgregarAlCarrito(int clienteId, int productoId, int cantidad)
        {
            _carritoService.AgregarAlCarrito(clienteId, productoId, cantidad);
            return RedirectToAction("VerCarrito", new { clienteId });
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult EliminarDelCarrito(int clienteId, int productoId)
        {
            _carritoService.EliminarDelCarrito(clienteId, productoId);
            return RedirectToAction("VerCarrito", new { clienteId });
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult VaciarCarrito(int clienteId)
        {
            _carritoService.VaciarCarrito(clienteId);
            return RedirectToAction("VerCarrito", new { clienteId });
        }
    }

}
