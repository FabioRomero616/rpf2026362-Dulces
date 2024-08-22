using Chocoworld.Data;
using Chocoworld.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Chocoworld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ChocomundoContext _context;
        private Cliente Session;

        public HomeController(ILogger<HomeController> logger, ChocomundoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ubicacion()
        {
            return View();
        }
        public async Task<IActionResult> Catalogo()
        {
            var chocomundoContext = _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Imagenes);

            return View(await chocomundoContext.ToListAsync());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                    return RedirectToAction("catalogo", "Home");
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


        public async Task<IActionResult> Perfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                var correo = User.FindFirst("Correo").Value;
                var cliente = await _context.Clientes
                    .Include(c => c.Direccion)
                    .FirstOrDefaultAsync(c => c.Email == correo);

                // Pasar el cliente completo a la vista
                return View(cliente);
            }

            return View();
        }

        public IActionResult Rdireccion()
        {
            return View();
        }
        public IActionResult CambiarContrasena()
        {
            return View();
        }


        //Registro de direcciones

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDireccion([Bind("Calle,Zona,Ciudad")] Direccione direccione)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var correo = User.FindFirst("Correo").Value;
                    var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == correo);

                    _context.Add(direccione);
                    await _context.SaveChangesAsync();

                    // Actualizar el Cliente con el DireccionId de la dirección recién creada
                    cliente.DireccionId = direccione.DireccionId;
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();

                    TempData["mensaje"] = "Direccion agregada correctamente";

                    return RedirectToAction(nameof(Rdireccion));
                }
            }

            return View(direccione);
        }

        // Cambio de contraseña


        [HttpPost]
        public ActionResult CambiarContrasena(CambiarContrasenaViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.ContrasenaActual == model.NuevaContrasena)
                {
                    ModelState.AddModelError("NuevaContrasena", "La nueva contraseña no puede ser igual a la contraseña actual");
                    return View(model);
                }
                else
                {

                    if (User.Identity.IsAuthenticated)
                    {
                        var correo = User.FindFirst("Correo").Value;
                        var Session = _context.Clientes.FirstOrDefault(c => c.Email == correo);

                        Session.Contrasena = model.NuevaContrasena;
                        _context.Update(Session);
                        _context.SaveChanges();

                        TempData["mensaje"] = "Contraseña cambiada correctamente";

                        return RedirectToAction("CambiarContrasena"); // Redirige a la página de perfil o donde desees
                    }
                }
            }
            return View(model);
        }

        // comentar producto

        public IActionResult ComentarProducto()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComentarProducto([Bind("Comentario,ProductoId")] Comentariosproducto comentariosproducto)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var correo = User.FindFirst("Correo").Value;
                    var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == correo);



                    comentariosproducto.ClienteId = cliente.ClienteId;

                    // Agregar la fecha y hora actual al comentario
                    comentariosproducto.FechaComentario = DateTime.Now;
                    _context.Add(comentariosproducto);
                    await _context.SaveChangesAsync();

                    TempData["mensaje"] = "Comentario agregado correctamente";

                    return RedirectToAction("ComentarProducto");
                }
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", comentariosproducto.ProductoId);

            return View(comentariosproducto);
        }




    }






    }






