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
    public class ComentariosproductoesController : Controller
    {
        private readonly ChocomundoContext _context;

        public ComentariosproductoesController(ChocomundoContext context)
        {
            _context = context;
        }

        // GET: Comentariosproductoes
        public async Task<IActionResult> Index()
        {
            var chocomundoContext = _context.Comentariosproductos.Include(c => c.Cliente).Include(c => c.Producto);
            return View(await chocomundoContext.ToListAsync());
        }

        // GET: Comentariosproductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comentariosproductos == null)
            {
                return NotFound();
            }

            var comentariosproducto = await _context.Comentariosproductos
                .Include(c => c.Cliente)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.ComentarioId == id);
            if (comentariosproducto == null)
            {
                return NotFound();
            }

            return View(comentariosproducto);
        }

        // GET: Comentariosproductoes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId");
            return View();
        }

        // POST: Comentariosproductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComentarioId,Comentario,Valoracion,FechaComentario,ClienteId,ProductoId")] Comentariosproducto comentariosproducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentariosproducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", comentariosproducto.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", comentariosproducto.ProductoId);
            return View(comentariosproducto);
        }

        // GET: Comentariosproductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comentariosproductos == null)
            {
                return NotFound();
            }

            var comentariosproducto = await _context.Comentariosproductos.FindAsync(id);
            if (comentariosproducto == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", comentariosproducto.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", comentariosproducto.ProductoId);
            return View(comentariosproducto);
        }

        // POST: Comentariosproductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComentarioId,Comentario,Valoracion,FechaComentario,ClienteId,ProductoId")] Comentariosproducto comentariosproducto)
        {
            if (id != comentariosproducto.ComentarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentariosproducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentariosproductoExists(comentariosproducto.ComentarioId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", comentariosproducto.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", comentariosproducto.ProductoId);
            return View(comentariosproducto);
        }

        // GET: Comentariosproductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comentariosproductos == null)
            {
                return NotFound();
            }

            var comentariosproducto = await _context.Comentariosproductos
                .Include(c => c.Cliente)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.ComentarioId == id);
            if (comentariosproducto == null)
            {
                return NotFound();
            }

            return View(comentariosproducto);
        }

        // POST: Comentariosproductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comentariosproductos == null)
            {
                return Problem("Entity set 'ChocomundoContext.Comentariosproductos'  is null.");
            }
            var comentariosproducto = await _context.Comentariosproductos.FindAsync(id);
            if (comentariosproducto != null)
            {
                _context.Comentariosproductos.Remove(comentariosproducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentariosproductoExists(int id)
        {
          return (_context.Comentariosproductos?.Any(e => e.ComentarioId == id)).GetValueOrDefault();
        }
    }
}
