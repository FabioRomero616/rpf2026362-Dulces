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
    public class CarritodecomprasController : Controller
    {
        private readonly ChocomundoContext _context;

        public CarritodecomprasController(ChocomundoContext context)
        {
            _context = context;
        }

        // GET: Carritodecompras
        public async Task<IActionResult> Index()
        {
            var chocomundoContext = _context.Carritodecompras.Include(c => c.Cliente).Include(c => c.Producto);
            return View(await chocomundoContext.ToListAsync());
        }

        // GET: Carritodecompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carritodecompras == null)
            {
                return NotFound();
            }

            var carritodecompra = await _context.Carritodecompras
                .Include(c => c.Cliente)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.CarritoId == id);
            if (carritodecompra == null)
            {
                return NotFound();
            }

            return View(carritodecompra);
        }

        // GET: Carritodecompras/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId");
            return View();
        }

        // POST: Carritodecompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarritoId,ClienteId,ProductoId,Cantidad")] Carritodecompra carritodecompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carritodecompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", carritodecompra.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", carritodecompra.ProductoId);
            return View(carritodecompra);
        }

        // GET: Carritodecompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carritodecompras == null)
            {
                return NotFound();
            }

            var carritodecompra = await _context.Carritodecompras.FindAsync(id);
            if (carritodecompra == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", carritodecompra.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", carritodecompra.ProductoId);
            return View(carritodecompra);
        }

        // POST: Carritodecompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarritoId,ClienteId,ProductoId,Cantidad")] Carritodecompra carritodecompra)
        {
            if (id != carritodecompra.CarritoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritodecompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritodecompraExists(carritodecompra.CarritoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", carritodecompra.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", carritodecompra.ProductoId);
            return View(carritodecompra);
        }

        // GET: Carritodecompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carritodecompras == null)
            {
                return NotFound();
            }

            var carritodecompra = await _context.Carritodecompras
                .Include(c => c.Cliente)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.CarritoId == id);
            if (carritodecompra == null)
            {
                return NotFound();
            }

            return View(carritodecompra);
        }

        // POST: Carritodecompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carritodecompras == null)
            {
                return Problem("Entity set 'ChocomundoContext.Carritodecompras'  is null.");
            }
            var carritodecompra = await _context.Carritodecompras.FindAsync(id);
            if (carritodecompra != null)
            {
                _context.Carritodecompras.Remove(carritodecompra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritodecompraExists(int id)
        {
          return (_context.Carritodecompras?.Any(e => e.CarritoId == id)).GetValueOrDefault();
        }
    }
}
