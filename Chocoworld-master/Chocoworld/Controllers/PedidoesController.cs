using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chocoworld.Data;
using Chocoworld.Models;

namespace Chocoworld.Controllers
{
    public class PedidoesController : Controller
    {
        private readonly ChocomundoContext _context;

        public PedidoesController(ChocomundoContext context)
        {
            _context = context;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var chocomundoContext = _context.Pedidos.Include(p => p.Cliente).Include(p => p.Empleado);
            return View(await chocomundoContext.ToListAsync());
        }

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Empleado)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidoes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre");
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre");
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoId,ClienteId,EmpleadoId,FechaPedido,EstadoPedido,Total,TipoPago")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                // Agregar la fecha y hora actual al comentario
                pedido.FechaPedido = DateTime.Now;
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", pedido.ClienteId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre", pedido.EmpleadoId);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", pedido.ClienteId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre", pedido.EmpleadoId);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoId,ClienteId,EmpleadoId,FechaPedido,EstadoPedido,Total,TipoPago")] Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.PedidoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", pedido.ClienteId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre", pedido.EmpleadoId);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Empleado)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'ChocomundoContext.Pedidos'  is null.");
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedidos?.Any(e => e.PedidoId == id)).GetValueOrDefault();
        }


      


        public decimal GetTotalVentasDelDia()
        {
            var hoy = DateTime.Today;
            var totalVentasDelDia = _context.Pedidos
                .Where(p => p.FechaPedido.HasValue && p.FechaPedido.Value.Date == hoy)
                .Sum(p => p.Total ?? 0);

            return totalVentasDelDia;
        }

        public decimal GetTotalVentasDelDiaPorTipoPago(string tipoPago)
        {
            var hoy = DateTime.Today;
            var totalVentasDelDia = _context.Pedidos
                .Where(p => p.FechaPedido.HasValue && p.FechaPedido.Value.Date == hoy && p.TipoPago == tipoPago)
                .Sum(p => p.Total ?? 0);

            return totalVentasDelDia;
        }

        public IActionResult Ventas()
        {
            var totalVentasDelDiaQR = GetTotalVentasDelDiaPorTipoPago("QR");
            var totalVentasDelDiaEfectivo = GetTotalVentasDelDiaPorTipoPago("Efectivo");
            var totalVentasDelDiaTarjeta = GetTotalVentasDelDiaPorTipoPago("Tarjeta");

            ViewBag.TotalVentasDelDiaQR = totalVentasDelDiaQR;
            ViewBag.TotalVentasDelDiaEfectivo = totalVentasDelDiaEfectivo;
            ViewBag.TotalVentasDelDiaTarjeta = totalVentasDelDiaTarjeta;
            ViewBag.TotalVentasDelDia = totalVentasDelDiaQR + totalVentasDelDiaEfectivo + totalVentasDelDiaTarjeta;

            var chocomundoContext = _context.Pedidos.Include(p => p.Cliente).Include(p => p.Empleado);
            return View(chocomundoContext.ToList());
        }

    }
}
