using Microsoft.AspNetCore.Mvc;
using Chocoworld.Models;
using Chocoworld.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Chocoworld.Controllers
{
    // Constructor que recibe una instancia del contexto de la base de datos

    public class AccesoController : Controller
    {
        private readonly ChocomundoContext _context;
        public AccesoController(ChocomundoContext context)
        {
            _context = context;
        }
        // Método que devuelve la vista de inicio de sesión

        public IActionResult Login()
        {
            return View();
        }
        // Método para validar el usuario en la base de datos


        public Tuple<Cliente, Empleado> ValidarUsuario(string correo, string pass)
        {
            var cliente = _context.Clientes.FirstOrDefault(item => item.Email == correo && item.Contrasena == pass);
            var empleado = _context.Empleados.FirstOrDefault(item => item.Email == correo && item.Contrasena == pass);

            return new Tuple<Cliente, Empleado>(cliente, empleado);
        }
        // Método que maneja la autenticación cuando se envía el formulario de inicio de sesión

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel modelo)
        {
            string correo = modelo.CorreoElectronico;
            string pass = modelo.Contraseña;

            var validacion = ValidarUsuario(correo, pass);
            // Si el usuario es un cliente
            if (validacion.Item1 != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, validacion.Item1.Nombre + " " + validacion.Item1.Apellido),

                    new Claim("Correo", validacion.Item1.Email)
                };
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                return RedirectToAction("Index", "Home");
            }
            // Si el usuario es un empleado
            else if (validacion.Item2 != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, validacion.Item2.Nombre + " " + validacion.Item2.Apellido),
                    new Claim("Correo", validacion.Item2.Email),
                    new Claim(ClaimTypes.Role, validacion.Item2.Rol)
                };
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

                return RedirectToAction("Index", "Dashboard");
                // Si la autenticación falla, regresa a la vista de inicio de sesión
            }
            else
            {
                return View();
            }
        }

        // Método para cerrar sesión
        public async Task<IActionResult> Cerrar()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Nombre,Apellido,Email,Contrasena,ConfirmarContrasena,Telefono,DireccionId")] Cliente cliente)
        {
            bool redirectToHome = true; // Bandera para indicar si se debe redirigir a la página de inicio

            if (ModelState.IsValid)
            {
                if (cliente.Contrasena != cliente.ConfirmarContrasena)
                {
                    ModelState.AddModelError("ConfirmarContrasena", "La confirmación de la contraseña no coincide");
                    ViewData["DireccionId"] = new SelectList(_context.Direcciones, "DireccionId", "DireccionId", cliente.DireccionId);
                    redirectToHome = false; // No se redirige a la página de inicio si las contraseñas no coinciden
                }
                else
                {
                    // Hash the password before saving it to the database (ensure you have proper password hashing implementation)
                    // For example, you can use ASP.NET Core Identity UserManager to handle password hashing.
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();

                    // Iniciar sesión automáticamente después del registro
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, cliente.Nombre + " " + cliente.Apellido),
                new Claim("Correo", cliente.Email)
            };
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

                    // La redirección se realizará después de un registro exitoso
                    return RedirectToAction("Index", "Home");
                }
            }

            // La redirección se realiza solo si redirectToHome es true
            if (redirectToHome)
            {
                ViewData["DireccionId"] = new SelectList(_context.Direcciones, "DireccionId", "DireccionId", cliente.DireccionId);
                return View(cliente);
            }

            return View();
        }

        

    }
}
