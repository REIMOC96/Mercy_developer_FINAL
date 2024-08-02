using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MercDevs_ej2.Models;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using BCrypt.Net;

namespace MercDevs_ej2.Controllers
{
    public class LoginController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public LoginController(MercyDeveloperContext context)
        {
            _context = context;
        }

        //GET USUARIO
        [HttpGet]
        public IActionResult Ingresar()
        {
            //Para validar la autenticación en caso que la sesión siga activa y no muestre el login
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            //Fin validar autenticación
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(UsuarioLogin usuarioLogin)
        {
            // Busca un solo usuario que coincida el correo ingresado por el login
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == usuarioLogin.Correo);

            // Verifica si el usuario existe y si la contraseña es correcta
            if (usuario != null && BCrypt.Net.BCrypt.Verify(usuarioLogin.Password, usuario.Password))
            {
                // Para datos de usuario login
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.Email, usuario.Correo),
                    new Claim("Id", usuario.IdUsuario.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true
                };

                await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        properties
                    );
                // Fin Usuario Login
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Usuario no encontrado o contraseña incorrecta
                ViewData["Mensaje"] = "Nombre de usuario o contraseña no coinciden";
                return View();
            }
        }
    }
}
