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
            return View(await _context.Usuarios.ToListAsync());
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
            if (usuario.Nombre != null && usuario.Apellido !=null && usuario.Correo !=null)
            {
                // Verifica que las contraseñas coincidan
                if (usuario.Password != usuario.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Las contraseñas no coinciden.");
                    return View(usuario);
                }

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
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,Apellido,Correo,Password,CurrentPassword")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (usuario.Nombre !=null && usuario.Apellido !=null && usuario.Correo != null)
            {
                var existingUser = await _context.Usuarios.FindAsync(id);

                // Verificar la contraseña actual
                if (!BCrypt.Net.BCrypt.Verify(usuario.CurrentPassword, existingUser.Password))
                {
                    ModelState.AddModelError("CurrentPassword", "Contraseña incorrecta.");
                    return View(usuario);
                }

                // No se cambia la contraseña en este formulario, solo los otros campos
                // se plantea la idea de ina vista aparte para modificar la contraseña del usuario requerira la contraseña de mercy Root
                existingUser.Nombre = usuario.Nombre;
                existingUser.Apellido = usuario.Apellido;
                existingUser.Correo = usuario.Correo;

                try
                {
                    _context.Update(existingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        return NotFound(); // console log aqui
                    }
                    else
                    {
                        throw;
    // hacer console log aqui 
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

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string CurrentPassword)
        {
            // Buscar el usuario en la base de datos
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                // El usuario no existe
                return NotFound();
            }

            // Comparar la contraseña ingresada con la almacenada
            if (BCrypt.Net.BCrypt.Verify(CurrentPassword, usuario.Password))
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // La contraseña no coincide, se devuelve un mensaje de error
                ModelState.AddModelError(string.Empty, "Contraseña incorrecta.");
                // Opcional: Redirigir a la vista de confirmación con el usuario
                return RedirectToAction("ConfirmDelete", new { id });
            }
        }

        // Método adicional para confirmar la eliminación
        public IActionResult ConfirmDelete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario); // Deberías tener una vista de confirmación de eliminación
        }


        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
