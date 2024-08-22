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
    public class MetodospagoesController : Controller
    {
        private readonly ChocomundoContext _context;

        public MetodospagoesController(ChocomundoContext context)
        {
            _context = context;
        }

        // GET: Metodospagoes
        public async Task<IActionResult> Index()
        {
            var chocomundoContext = _context.Metodospagos.Include(m => m.Cliente);
            return View(await chocomundoContext.ToListAsync());
        }

        // GET: Metodospagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Metodospagos == null)
            {
                return NotFound();
            }

            var metodospago = await _context.Metodospagos
                .Include(m => m.Cliente)
                .FirstOrDefaultAsync(m => m.MetodoPagoId == id);
            if (metodospago == null)
            {
                return NotFound();
            }

            return View(metodospago);
        }

        // GET: Metodospagoes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId");
            return View();
        }

        // POST: Metodospagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetodoPagoId,ClienteId,NumeroTarjeta,FechaExpiracion,NombreTitular")] Metodospago metodospago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodospago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", metodospago.ClienteId);
            return View(metodospago);
        }

        // GET: Metodospagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Metodospagos == null)
            {
                return NotFound();
            }

            var metodospago = await _context.Metodospagos.FindAsync(id);
            if (metodospago == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", metodospago.ClienteId);
            return View(metodospago);
        }

        // POST: Metodospagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetodoPagoId,ClienteId,NumeroTarjeta,FechaExpiracion,NombreTitular")] Metodospago metodospago)
        {
            if (id != metodospago.MetodoPagoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metodospago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodospagoExists(metodospago.MetodoPagoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", metodospago.ClienteId);
            return View(metodospago);
        }

        // GET: Metodospagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Metodospagos == null)
            {
                return NotFound();
            }

            var metodospago = await _context.Metodospagos
                .Include(m => m.Cliente)
                .FirstOrDefaultAsync(m => m.MetodoPagoId == id);
            if (metodospago == null)
            {
                return NotFound();
            }

            return View(metodospago);
        }

        // POST: Metodospagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Metodospagos == null)
            {
                return Problem("Entity set 'ChocomundoContext.Metodospagos'  is null.");
            }
            var metodospago = await _context.Metodospagos.FindAsync(id);
            if (metodospago != null)
            {
                _context.Metodospagos.Remove(metodospago);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetodospagoExists(int id)
        {
          return (_context.Metodospagos?.Any(e => e.MetodoPagoId == id)).GetValueOrDefault();
        }
    }
}
