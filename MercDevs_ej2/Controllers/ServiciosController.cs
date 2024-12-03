using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercDevs_ej2.Models;
using Microsoft.AspNetCore.Authorization;

namespace MercDevs_ej2.Controllers
{
    [Authorize]

    public class ServiciosController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public ServiciosController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Servicios
        public async Task<IActionResult> Index()
        {
            var mercydevsEjercicio2Context = _context.Servicios.Include(s => s.UsuarioIdUsuarioNavigation);
            return View(await mercydevsEjercicio2Context.ToListAsync());
        }

        // GET: Servicios/Verdescripcion/5
        public async Task<IActionResult> Verdescripcion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .FirstOrDefaultAsync(m => m.IdServicio == id)
                ;

            var verdescripcion = _context.Descripcionservicios.Where(m => m.ServicioIdServicio == id)
                .Include(m => m.ServicioIdServicioNavigation);
            if (verdescripcion == null)
            {
                return NotFound();
            }

            ViewBag.servicio = servicio;
            return View(await verdescripcion.ToListAsync());
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .Include(s => s.UsuarioIdUsuarioNavigation)
                .Include(d => d.Descripcionservicios)
                .FirstOrDefaultAsync(m => m.IdServicio == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {
            ViewData["UsuarioIdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            ViewData["Descripcionservicio"] = new SelectList(_context.Descripcionservicios, "IdDescServ", "IdDescServ");
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServicio,Nombre,Precio,Sku,Descripcionservicios")] Servicio servicio)
        {
            // Obtén el ID del usuario autenticado desde las claims
            var userId = User.FindFirst("Id")?.Value;

            // Verifica que el ID no sea nulo
            if (!string.IsNullOrEmpty(userId))
            {
                // Asigna el ID del usuario autenticado al modelo
                servicio.UsuarioIdUsuario = int.Parse(userId);
            }

            if (servicio.Nombre != null && servicio.Precio != 0)
            {
                // Guarda el servicio primero para que tenga un ID generado
                _context.Servicios.Add(servicio);
                await _context.SaveChangesAsync();

                // Guarda las descripciones asociadas al servicio
                foreach (var descripcion in servicio.Descripcionservicios)
                {
                    descripcion.ServicioIdServicio = servicio.IdServicio; // Asociar la descripción con el servicio
                    _context.Descripcionservicios.Add(descripcion);
                }

                // Guarda las descripciones
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Si hay algún error, se vuelve a cargar la vista con los datos actuales
            ViewData["UsuarioIdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", servicio.UsuarioIdUsuario);
            return View(servicio);
        }

        //Agregar metodo Get: de editar
        // GET: Servicios
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Obtener el servicio con sus descripciones relacionadas
            var servicio = await _context.Servicios
                .Include(s => s.Descripcionservicios)
                .FirstOrDefaultAsync(s => s.IdServicio == id); // Asegúrate de obtener un solo objeto

            if (servicio == null)
            {
                return NotFound();
            }

            // Opcional: Si necesitas enviar datos adicionales, como un ViewBag, aquí lo harías
            ViewBag.UsuarioIdUsuario = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");

            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServicio,Nombre,Precio,Sku,UsuarioIdUsuario")] Servicio servicio)
        {
            if (id != servicio.IdServicio)
            {
                return NotFound();
            }

            if (servicio.Nombre != null)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.IdServicio))
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
            ViewData["UsuarioIdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", servicio.UsuarioIdUsuario);
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .Include(s => s.UsuarioIdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdServicio == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.IdServicio == id);
        }

    }
}
