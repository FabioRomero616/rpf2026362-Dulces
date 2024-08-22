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
    public class ImagenesController : Controller
    {
        private readonly ChocomundoContext _context;

        public ImagenesController(ChocomundoContext context)
        {
            _context = context;
        }

        // GET: Imagenes
        public async Task<IActionResult> Index(string buscar)
        {
            var imagenes = from imagen in _context.Imagenes select imagen;
            if (!String.IsNullOrEmpty(buscar))
            {
                imagenes = imagenes.Where(imagen => imagen.Nombre.Contains(buscar));
            }   
            return View(await imagenes.ToListAsync());
        }

        // GET: Imagenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Imagenes == null)
            {
                return NotFound();
            }

            var imagene = await _context.Imagenes
                .Include(i => i.Producto)
                .FirstOrDefaultAsync(m => m.ImagenId == id);
            if (imagene == null)
            {
                return NotFound();
            }

            return View(imagene);
        }

        // GET: Imagenes/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            return View();
        }

        // POST: Imagenes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImagenId,Nombre,Ruta,Extension,ProductoId")] Imagene imagene)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imagene);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", imagene.ProductoId);
            return View(imagene);
        }

        // GET: Imagenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Imagenes == null)
            {
                return NotFound();
            }

            var imagene = await _context.Imagenes.FindAsync(id);
            if (imagene == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", imagene.ProductoId);
            return View(imagene);
        }

        // POST: Imagenes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImagenId,Nombre,Ruta,Extension,ProductoId")] Imagene imagene)
        {
            if (id != imagene.ImagenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagene);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageneExists(imagene.ImagenId))
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
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", imagene.ProductoId);
            return View(imagene);
        }

        // GET: Imagenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Imagenes == null)
            {
                return NotFound();
            }

            var imagene = await _context.Imagenes
                .Include(i => i.Producto)
                .FirstOrDefaultAsync(m => m.ImagenId == id);
            if (imagene == null)
            {
                return NotFound();
            }

            return View(imagene);
        }

        // POST: Imagenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Imagenes == null)
            {
                return Problem("Entity set 'ChocomundoContext.Imagenes'  is null.");
            }
            var imagene = await _context.Imagenes.FindAsync(id);
            if (imagene != null)
            {
                _context.Imagenes.Remove(imagene);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageneExists(int id)
        {
          return (_context.Imagenes?.Any(e => e.ImagenId == id)).GetValueOrDefault();
        }
    }
}
