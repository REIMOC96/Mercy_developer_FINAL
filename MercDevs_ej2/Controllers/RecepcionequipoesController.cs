using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercDevs_ej2.Models;
using Microsoft.CodeAnalysis;
using System;
using Microsoft.AspNetCore.Authorization;

namespace MercDevs_ej2.Controllers
{
    [Authorize]

    public class RecepcionequipoesController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public RecepcionequipoesController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Recepcionequipoes
        public async Task<IActionResult> Index()
        {
            var mercydevsEjercicio2Context = _context.Recepcionequipos
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdServicioNavigation);

            return View(await mercydevsEjercicio2Context.ToListAsync());
        }

        public async Task<IActionResult> IndexId(int id)
        {
            var mercydevsEjercicio2Context = _context.Recepcionequipos
                .Where(r => r.IdCliente == id)
                .Include(r => r.IdServicioNavigation)
                .Include(r => r.IdClienteNavigation);

            return View (await mercydevsEjercicio2Context.ToListAsync());
        }

        // GET: Recepcionequipoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcionequipo = await _context.Recepcionequipos
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recepcionequipo == null)
            {
                return NotFound();
            }

            return View(recepcionequipo);
        }

        public async Task<IActionResult> DetailsId(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcionequipo = await _context.Recepcionequipos
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recepcionequipo == null)
            {
                return NotFound();
            }

            return View(recepcionequipo);
        }

        // GET: Recepcionequipoes/Create
        public IActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(_context.Clientes, "IdCliente", "Nombre");
            ViewBag.IdServicio = new SelectList(_context.Servicios, "IdServicio", "Nombre");

            return View();
        }


        // POST: Recepcionequipoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCliente,IdServicio,Fecha,TipoPc,CPU,Accesorio,MarcaPc,ModeloPc,Nserie,CapacidadRam,TipoAlmacenamiento,CapacidadAlmacenamiento,TipoGpu,Grafico,Estado")] Recepcionequipo recepcionequipo)
        {
            // Asignar la fecha actual si no se ha proporcionado una
            if (recepcionequipo.Fecha == default(DateTime))
            {
                recepcionequipo.Fecha = DateTime.Now;
            }

            // Diccionario con los datos de la tabla
            var tablaDato = new Dictionary<string, object?>
    {
        { nameof(recepcionequipo.MarcaPc), recepcionequipo.MarcaPc  },
        { nameof(recepcionequipo.Fecha), recepcionequipo.Fecha },
        { nameof(recepcionequipo.TipoPc), recepcionequipo.TipoPc },
        { nameof(recepcionequipo.Accesorio), recepcionequipo.Accesorio },
        { nameof(recepcionequipo.ModeloPc), recepcionequipo.ModeloPc },
        { nameof(recepcionequipo.Nserie), recepcionequipo.Nserie },
        { nameof(recepcionequipo.CapacidadRam), recepcionequipo.CapacidadRam },
        { nameof(recepcionequipo.TipoAlmacenamiento), recepcionequipo.TipoAlmacenamiento },
        { nameof(recepcionequipo.CapacidadAlmacenamiento), recepcionequipo.CapacidadAlmacenamiento },
        { nameof(recepcionequipo.TipoGpu), recepcionequipo.TipoGpu },
        { nameof(recepcionequipo.Grafico), recepcionequipo.Grafico }
    };

            // Validación de cada dato
            foreach (var dato in tablaDato)
            {
                if (dato.Value == null || (dato.Value is int intValue && intValue == 0))
                {
                    // Si un valor es nulo o cero, devolver la vista con los select lists nuevamente
                    ViewData["IdCliente"] = new SelectList(await _context.Clientes.ToListAsync(), "IdCliente", "Nombre", recepcionequipo.IdCliente);
                    ViewData["IdServicio"] = new SelectList(await _context.Servicios.ToListAsync(), "IdServicio", "Nombre", recepcionequipo.IdServicio);
                    return View(recepcionequipo);
                }

                // Validar si la fecha es anterior a hoy
                if (dato.Key == nameof(recepcionequipo.Fecha) && recepcionequipo.Fecha < DateTime.Today)
                {
                    ModelState.AddModelError(nameof(recepcionequipo.Fecha), "La fecha no puede ser anterior a la fecha actual.");
                    ViewData["IdCliente"] = new SelectList(await _context.Clientes.ToListAsync(), "IdCliente", "Nombre", recepcionequipo.IdCliente);
                    ViewData["IdServicio"] = new SelectList(await _context.Servicios.ToListAsync(), "IdServicio", "Nombre", recepcionequipo.IdServicio);
                    return View(recepcionequipo);
                }
            }

            // Si todas las validaciones son correctas, añadir el equipo y guardar
            _context.Add(recepcionequipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }






        // GET: Recepcionequipoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcionequipo = await _context.Recepcionequipos
                .Include(r => r.IdClienteNavigation) // Carga los datos del cliente
                .Include(r => r.IdServicioNavigation) // Carga los datos del servicio
                .FirstOrDefaultAsync(r => r.Id == id); // Busca por el ID

            if (recepcionequipo == null)
            {
                return NotFound();
            }

            // Crea las listas para los dropdowns
            ViewData["IdCliente"] = new SelectList(
                _context.Clientes, "IdCliente", "Nombre", recepcionequipo.IdCliente);

            ViewData["IdServicio"] = new SelectList(
                _context.Servicios, "IdServicio", "Nombre", recepcionequipo.IdServicio);

            return View(recepcionequipo);
        }

        // POST: Recepcionequipoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCliente,IdServicio,Fecha,TipoPc,CPU,Accesorio,MarcaPc,ModeloPc," +
"Nserie,CapacidadRam,TipoAlmacenamiento,CapacidadAlmacenamiento,TipoGpu,Grafico,Estado")] Recepcionequipo recepcionequipo)
        {
            if (id != recepcionequipo.Id)
            {
                return NotFound();
            }

            if (recepcionequipo.IdCliente != 0) // Verificar si el modelo es válido
            {
                try
                {
                    _context.Update(recepcionequipo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecepcionequipoExists(recepcionequipo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "No se pudieron guardar los cambios. El registro fue actualizado o eliminado por otro usuario.");
                    }
                }
                catch (Exception ex)
                {
                    // Imprimir el mensaje de error en la consola
                    Console.WriteLine($"Error al guardar en la base de datos: {ex.Message}");

                    // También puedes imprimir detalles adicionales si los necesitas
                    Console.WriteLine($"Detalles adicionales: {ex.InnerException}");

                    ModelState.AddModelError(string.Empty, $"Ocurrió un error: {ex.Message}");
                }
            }

            // Si el modelo no es válido o si ocurre un error, vuelve a cargar los datos necesarios para mostrar la vista de edición
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", recepcionequipo.IdCliente);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", recepcionequipo.IdServicio);
            return View(recepcionequipo);
        }


        private bool RecepcionequipoExists(int id)
        {
            return _context.Recepcionequipos.Any(e => e.Id == id);
        }






        // GET: Recepcionequipoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcionequipo = await _context.Recepcionequipos
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recepcionequipo == null)
            {
                return NotFound();
            }

            return View(recepcionequipo);
        }




        // POST: Recepcionequipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Buscar el registro en Recepcionequipos por su ID
            var recepcionequipo = await _context.Recepcionequipos.FindAsync(id);

            if (recepcionequipo == null)
            {
                return NotFound();
            }

            // Verificar si el registro tiene relaciones en Datosfichatecnicas
            bool tieneForanea = await _context.Datosfichatecnicas.AnyAsync(d => d.RecepcionEquipoId == id);

            if (tieneForanea)
            {
                TempData["ErrorMessage"] = "No se puede eliminar este registro porque está relacionado con otros datos tecnicos.";
                return RedirectToAction(nameof(Delete), new { id });
            }


            // Si no hay registros relacionados, eliminar el registro
            _context.Recepcionequipos.Remove(recepcionequipo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        // POST: Recepcionequipoes/Finalizar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Finalizar(int id)
        {
            var recepcionEquipo = await _context.Recepcionequipos.FindAsync(id);
            if (recepcionEquipo == null)
            {
                return NotFound();
            }

            recepcionEquipo.Estado = 0;

            try
            {
                _context.Update(recepcionEquipo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecepcionequipoExists(recepcionEquipo.Id))
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

    }
}
