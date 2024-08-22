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
    public class EmpleadoesController : Controller
    {
        private readonly ChocomundoContext _context;

        public EmpleadoesController(ChocomundoContext context)
        {
            _context = context;
        }

        // GET: Empleadoes
        public async Task<IActionResult> Index(string buscar)
        {
            var empleados = from empleado in _context.Empleados select empleado;

            if (!String.IsNullOrEmpty(buscar))
            {
                empleados = empleados.Where(empleado => empleado.Nombre.Contains(buscar));
            }
            return View(await empleados.ToListAsync());
        }

        // GET: Empleadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleadoes/Create
        // POST: Empleadoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpleadoId,Nombre,Apellido,Email,Rol,Contrasena,Telefono,Direccion")] Empleado empleado)
        {
            // Validar el formato del correo electrónico
            if (!EsCorreoValido(empleado.Email))
            {
                ModelState.AddModelError("Email", "El correo electrónico debe tener el formato usuario@chocomundo.com");
            }

            // Validar que el correo electrónico no esté duplicado
            if (_context.Empleados.Any(e => e.Email == empleado.Email))
            {
                ModelState.AddModelError("Email", "Ya existe un empleado con este correo electrónico.");
            }

            // Validar que el número de teléfono no esté duplicado
            if (_context.Empleados.Any(e => e.Telefono == empleado.Telefono))
            {
                ModelState.AddModelError("Telefono", "Ya existe un empleado con este número de teléfono.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(empleado);
        }


        // Función para validar el formato del correo electrónico
        private bool EsCorreoValido(string correo)
        {
            // Puedes personalizar esta lógica según tus necesidades
            return correo != null && correo.EndsWith("@chocomundo.com");
        }


        // GET: Empleadoes/Edit/5
        // GET: Empleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpleadoId,Nombre,Apellido,Email,Rol,Contrasena,Telefono,Direccion")] Empleado empleado)
        {
            if (id != empleado.EmpleadoId)
            {
                return NotFound();
            }

            // Validar el formato del correo electrónico
            if (!EsCorreoValido(empleado.Email))
            {
                ModelState.AddModelError("Email", "El correo electrónico debe tener el formato usuario@chocomundo.com");
            }

            // Validar que el correo electrónico no esté duplicado (excepto para el empleado actual)
            if (_context.Empleados.Any(e => e.Email == empleado.Email && e.EmpleadoId != empleado.EmpleadoId))
            {
                ModelState.AddModelError("Email", "Ya existe un empleado con este correo electrónico.");
            }

            // Validar que el número de teléfono no esté duplicado (excepto para el empleado actual)
            if (_context.Empleados.Any(e => e.Telefono == empleado.Telefono && e.EmpleadoId != empleado.EmpleadoId))
            {
                ModelState.AddModelError("Telefono", "Ya existe un empleado con este número de teléfono.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.EmpleadoId))
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
            return View(empleado);
        }


        // GET: Empleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'ChocomundoContext.Empleados'  is null.");
            }
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
          return (_context.Empleados?.Any(e => e.EmpleadoId == id)).GetValueOrDefault();
        }
    }
}
