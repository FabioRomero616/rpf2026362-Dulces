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
    public class DetallespedidoesController : Controller
    {
        private readonly ChocomundoContext _context;

        public DetallespedidoesController(ChocomundoContext context)
        {
            _context = context;
        }

        // GET: Detallespedidoes
        public async Task<IActionResult> Index()
        {
            var chocomundoContext = _context.Detallespedidos.Include(d => d.Pedido).Include(d => d.Producto).Include(d => d.Promocion);
            return View(await chocomundoContext.ToListAsync());
        }

        // GET: Detallespedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Detallespedidos == null)
            {
                return NotFound();
            }

            var detallespedido = await _context.Detallespedidos
                .Include(d => d.Pedido)
                .Include(d => d.Producto)
                .Include(d => d.Promocion)
                .FirstOrDefaultAsync(m => m.DetalleId == id);
            if (detallespedido == null)
            {
                return NotFound();
            }

            return View(detallespedido);
        }

        // GET: Detallespedidoes/Create
        public IActionResult Create()
        {
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "PedidoId");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId");
            ViewData["PromocionId"] = new SelectList(_context.Promociones, "PromocionId", "PromocionId");
            return View();
        }

        // POST: Detallespedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleId,PedidoId,ProductoId,Cantidad,PrecioUnitario,PromocionId")] Detallespedido detallespedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallespedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "PedidoId", detallespedido.PedidoId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", detallespedido.ProductoId);
            ViewData["PromocionId"] = new SelectList(_context.Promociones, "PromocionId", "PromocionId", detallespedido.PromocionId);
            return View(detallespedido);
        }

        // GET: Detallespedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Detallespedidos == null)
            {
                return NotFound();
            }

            var detallespedido = await _context.Detallespedidos.FindAsync(id);
            if (detallespedido == null)
            {
                return NotFound();
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "PedidoId", detallespedido.PedidoId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", detallespedido.ProductoId);
            ViewData["PromocionId"] = new SelectList(_context.Promociones, "PromocionId", "PromocionId", detallespedido.PromocionId);
            return View(detallespedido);
        }

        // POST: Detallespedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleId,PedidoId,ProductoId,Cantidad,PrecioUnitario,PromocionId")] Detallespedido detallespedido)
        {
            if (id != detallespedido.DetalleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallespedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallespedidoExists(detallespedido.DetalleId))
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
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "PedidoId", detallespedido.PedidoId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", detallespedido.ProductoId);
            ViewData["PromocionId"] = new SelectList(_context.Promociones, "PromocionId", "PromocionId", detallespedido.PromocionId);
            return View(detallespedido);
        }

        // GET: Detallespedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Detallespedidos == null)
            {
                return NotFound();
            }

            var detallespedido = await _context.Detallespedidos
                .Include(d => d.Pedido)
                .Include(d => d.Producto)
                .Include(d => d.Promocion)
                .FirstOrDefaultAsync(m => m.DetalleId == id);
            if (detallespedido == null)
            {
                return NotFound();
            }

            return View(detallespedido);
        }

        // POST: Detallespedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Detallespedidos == null)
            {
                return Problem("Entity set 'ChocomundoContext.Detallespedidos'  is null.");
            }
            var detallespedido = await _context.Detallespedidos.FindAsync(id);
            if (detallespedido != null)
            {
                _context.Detallespedidos.Remove(detallespedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallespedidoExists(int id)
        {
          return (_context.Detallespedidos?.Any(e => e.DetalleId == id)).GetValueOrDefault();
        }
    }
}
