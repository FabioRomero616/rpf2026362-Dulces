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
    public class DireccionesController : Controller
    {
        private readonly ChocomundoContext _context;

        public DireccionesController(ChocomundoContext context)
        {
            _context = context;
        }

        // GET: Direcciones
        public async Task<IActionResult> Index(string buscar)
        {
            var direcciones = from direccion in _context.Direcciones select direccion;

            if (!String.IsNullOrEmpty(buscar))
            {
                direcciones = direcciones.Where(direccion => direccion.Calle.Contains(buscar));
            }
              return View(await direcciones.ToListAsync());
        }

        // GET: Direcciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Direcciones == null)
            {
                return NotFound();
            }

            var direccione = await _context.Direcciones
                .FirstOrDefaultAsync(m => m.DireccionId == id);
            if (direccione == null)
            {
                return NotFound();
            }

            return View(direccione);
        }

        // GET: Direcciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Direcciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DireccionId,Calle,Zona,Ciudad")] Direccione direccione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(direccione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(direccione);
        }

        // GET: Direcciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Direcciones == null)
            {
                return NotFound();
            }

            var direccione = await _context.Direcciones.FindAsync(id);
            if (direccione == null)
            {
                return NotFound();
            }
            return View(direccione);
        }

        // POST: Direcciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DireccionId,Calle,Zona,Ciudad")] Direccione direccione)
        {
            if (id != direccione.DireccionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccioneExists(direccione.DireccionId))
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
            return View(direccione);
        }

        // GET: Direcciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Direcciones == null)
            {
                return NotFound();
            }

            var direccione = await _context.Direcciones
                .FirstOrDefaultAsync(m => m.DireccionId == id);
            if (direccione == null)
            {
                return NotFound();
            }

            return View(direccione);
        }

        // POST: Direcciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Direcciones == null)
            {
                return Problem("Entity set 'ChocomundoContext.Direcciones'  is null.");
            }
            var direccione = await _context.Direcciones.FindAsync(id);
            if (direccione != null)
            {
                _context.Direcciones.Remove(direccione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccioneExists(int id)
        {
          return (_context.Direcciones?.Any(e => e.DireccionId == id)).GetValueOrDefault();
        }
    }
}
