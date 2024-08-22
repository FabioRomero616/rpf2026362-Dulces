using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Chocoworld.Data;

namespace Chocoworld.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ChocomundoContext _context;

        public DashboardController(ChocomundoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int totalProductos = ObtenerTotalProductos();
            int totalClientes = ObtenerTotalClientes();
            int totalCategorias = ObtenerTotalCategorias();
            int totalEmpleados = ObtenerTotalEmpleados();


            ViewBag.TotalProductos = totalProductos;
            ViewBag.TotalClientes = totalClientes;
            ViewBag.TotalCategorias = totalCategorias;
            ViewBag.TotalEmpleados = totalEmpleados;


            return View();
        }

        private int ObtenerTotalProductos()
        {
            return _context.Productos.Count();
        }

        private int ObtenerTotalClientes()
        {
            return _context.Clientes.Count();
        }
        private int ObtenerTotalCategorias()
        {
            return _context.Categorias.Count();
        }
        private int ObtenerTotalEmpleados()
        {
            return _context.Empleados.Count();
        }


    }
}

