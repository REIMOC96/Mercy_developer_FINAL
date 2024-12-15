using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercDevs_ej2.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Text;

namespace MercDevs_ej2.Controllers
{
    [Authorize]

    public class UsuariosController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public UsuariosController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            // Imprimir el conteo de usuarios en la consola para verificar
            Console.WriteLine($"Número de usuarios recuperados: {usuarios.Count}");

            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,Apellido,Correo,Password,ConfirmPassword")] Usuario usuario)
        {
            // Verificar que el correo electronico no existe
            bool emailExists = await _context.Usuarios.AnyAsync(u => u.Correo == usuario.Correo);

            if (emailExists && usuario.Nombre != null && usuario.Apellido != null)
            {
                ModelState.AddModelError("Correo", "El correo electronico ya  existe");
            }

            else if (usuario.Password != usuario.ConfirmPassword)
            {
                // Verifica que las contraseñas coincidan
                ModelState.AddModelError("ConfirmPassword", "Las contraseñas no coinciden.");
                return View(usuario);



            }
            else {
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password); // Encripta la contraseña
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }


        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string CurrentPassword, [Bind("IdUsuario,Nombre,Apellido,Correo,Password,CurrentPassword")] Usuario usuario)

        {

            if (usuario == null && id != usuario.IdUsuario )
            {
                return NotFound();
            }

            if (usuario.Nombre != null && usuario.Apellido != null && usuario.Correo != null)
            {
                var userExists = await _context.Usuarios.FindAsync(id);

                if (userExists != null)
                {
                    // Verificar la contraseña actual
                    if (!BCrypt.Net.BCrypt.Verify(CurrentPassword, userExists.Password))

                    {
                        ModelState.AddModelError("CurrentPassword", "Contraseña incorrecta.");
                        return View(usuario);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontró el registro en la base de datos.");
                    return NotFound();
                }

                // Actualizar solo las propiedades específicas sin cambiar el objeto "userExists" en el contexto
                userExists.Nombre = usuario.Nombre;
                userExists.Apellido = usuario.Apellido;
                userExists.Correo = usuario.Correo;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        Console.WriteLine("No se encontró el usuario durante la concurrencia.");
                        return NotFound();
                    }
                    else
                    {
                        Console.WriteLine("Error de concurrencia durante la actualización.");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }



        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, [Bind("CurrentPassword")] Usuario usuario)
        {
            // Buscar el usuario en la base de datos
            var ExistUser = await _context.Usuarios.FindAsync(id);

            //verificar que no tenga una llave foranea registrada pa q no explote
            bool tieneForanea = await _context.Servicios.AnyAsync(d => d.UsuarioIdUsuario == id);

            if (tieneForanea)
            {
                TempData["ErrorMessage"] = "No se puede eliminar este registro porque está relacionado con otros datos tecnicos.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            if (ExistUser != null && BCrypt.Net.BCrypt.Verify(usuario.CurrentPassword, ExistUser.Password))
            {
                // Eliminar el usuario si la contraseña coincide
                _context.Usuarios.Remove(ExistUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // La contraseña no coincide o el usuario no existe
                ModelState.AddModelError(string.Empty, "Contraseña incorrecta.");
                Console.WriteLine("NO FUNCA");
                // Volver a la vista Delete sin redireccionar, mostrando el mensaje de error
                return View();
            }
        }



        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
