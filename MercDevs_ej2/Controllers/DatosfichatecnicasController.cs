using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercDevs_ej2.Models;
using Rotativa.AspNetCore;
using MercDevs_ej2.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;



namespace MercDevs_ej2.Controllers
{
    [Authorize]

    public class DatosfichatecnicasController : Controller
    {
        private readonly MercyDeveloperContext _context;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        public DatosfichatecnicasController(MercyDeveloperContext context, IConfiguration configuration, EmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<IActionResult> FichaTecnica(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var DatosFicha = await _context.Datosfichatecnicas
                .Include(d => d.RecepcionEquipo)
                    .ThenInclude(r => r.IdClienteNavigation)
                .Include(d => d.RecepcionEquipo)
                    .ThenInclude(r => r.IdServicioNavigation)
                .Include(d => d.Diagnosticosolucions)
                .FirstOrDefaultAsync(d => d.IdDatosFichaTecnica == id);

            if (DatosFicha == null)
            {
                return NotFound();
            }

            return View(DatosFicha);
        }

        [HttpGet]
       


        public async Task<IActionResult> Inicio()
        {
            var mercydevsEjercicio2Context = _context.Datosfichatecnicas.Include(d => d.RecepcionEquipo);
            return View(await mercydevsEjercicio2Context.ToListAsync());
        }

        // GET: Datosfichatecnicas
        public async Task<IActionResult> Index(int id)
        {
            var fichaTecnica = await _context.Datosfichatecnicas
                .Include(d => d.RecepcionEquipo)
                .Include(d => d.Diagnosticosolucions)
                .Include(d => d.RecepcionEquipo.IdClienteNavigation)
                .Include(d=> d.RecepcionEquipo.IdServicioNavigation)
                .FirstOrDefaultAsync(d => d.RecepcionEquipoId == id);

            if (fichaTecnica == null)
            {
                return RedirectToAction("Create", new { id });
            }

            return View(fichaTecnica);
        }

        //Listar Datos ficha Tecnica por Recepción de Equipos de Cliente: VerDatosFichaTecnicaPorRecepcion

        public async Task<IActionResult> VerDatosFichaTecnicaPorRecepcion(int id)
        {
            var mercydevsEjercicio2Context = _context.Datosfichatecnicas
                .Where(d => d.RecepcionEquipoId == id)
                .Include(d => d.RecepcionEquipo);
            ViewData["IdRecepcionEquipo"] = id;
            return View(await mercydevsEjercicio2Context.ToListAsync());
        }


        // GET: Datosfichatecnicas/Diagnosticosolucionpordatosficha/5
        public async Task<IActionResult> Diagnosticosolucionpordatosficha(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verdiagnostico = await _context.Datosfichatecnicas
                .Include(r => r.Diagnosticosolucions)
                .Include(r => r.RecepcionEquipo)
                .Include(d => d.RecepcionEquipo.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdDatosFichaTecnica == id);
            if (verdiagnostico == null)
            {
                return NotFound();
            }

            return View(verdiagnostico);
        }

        // GET: Datosfichatecnicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datosfichatecnica = await _context.Datosfichatecnicas
                .Include(d => d.RecepcionEquipo)
                .Include(c => c.RecepcionEquipo.IdClienteNavigation)
                .Include(c => c.RecepcionEquipo.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdDatosFichaTecnica == id);
            if (datosfichatecnica == null)
            {
                return NotFound();
            }

            return View(datosfichatecnica);
        }

        // GET: Datosfichatecnicas/Create
        public IActionResult Create(int? id)
        {
            ViewData["RecepcionEquipoId"] = new SelectList(_context.Recepcionequipos, "Id", "Id");
            ViewData["IdRecepcionEquipo"] = id;
            return View();
        }

        // POST: Datosfichatecnicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Datosfichatecnicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("IdDatosFichaTecnica,FechaInicio,FechaFinalizacion,PobservacionesRecomendaciones,Soinstalado,SuiteOfficeInstalada,LectorPdfinstalado,NavegadorWebInstalado,AntivirusInstalado,RecepcionEquipoId,Estado")] Datosfichatecnica datosfichatecnica)
        {
            // Verificamos que el id recibido no sea nulo
            if (id == null)
            {
                return NotFound();
            }

            // Asignamos el RecepcionEquipoId al nuevo registro
            datosfichatecnica.RecepcionEquipoId = Convert.ToInt32(id);

            // Verificamos si el RecepcionEquipoId es válido
            if (!_context.Recepcionequipos.Any(r => r.Id == datosfichatecnica.RecepcionEquipoId))
            {
                ModelState.AddModelError("RecepcionEquipoId", "El id del equipo no es válido.");
                ViewData["RecepcionEquipoId"] = new SelectList(_context.Recepcionequipos, "Id", "Id", datosfichatecnica.RecepcionEquipoId);
                return View(datosfichatecnica);
            }

            if (ModelState.IsValid)
            {
                // Guardamos los cambios
                _context.Add(datosfichatecnica);
                await _context.SaveChangesAsync();
                return RedirectToAction("Inicio", "Datosfichatecnicas");
            }

            // Si hay errores, recargamos el SelectList
            ViewData["RecepcionEquipoId"] = new SelectList(_context.Recepcionequipos, "Id", "Id", datosfichatecnica.RecepcionEquipoId);
            return View(datosfichatecnica);
        }

        // GET: Datosfichatecnicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datosfichatecnica = await _context.Datosfichatecnicas.FindAsync(id);
            if (datosfichatecnica == null)
            {
                return NotFound();
            }
            ViewData["RecepcionEquipoId"] = new SelectList(_context.Recepcionequipos, "Id", "Id", datosfichatecnica.RecepcionEquipoId);
            return View(datosfichatecnica);
        }

        // POST: Datosfichatecnicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDatosFichaTecnica,FechaInicio,FechaFinalizacion,PobservacionesRecomendaciones,Soinstalado,SuiteOfficeInstalada,LectorPdfinstalado,NavegadorWebInstalado,AntivirusInstalado,RecepcionEquipoId,Estado")] Datosfichatecnica datosfichatecnica)
        {
            if (id != datosfichatecnica.IdDatosFichaTecnica)
            {
                return NotFound();
            }

            if (datosfichatecnica.FechaInicio != null)
            {
                try
                {
                    _context.Update(datosfichatecnica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatosfichatecnicaExists(datosfichatecnica.IdDatosFichaTecnica))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Inicio));
            }
            ViewData["RecepcionEquipoId"] = new SelectList(_context.Recepcionequipos, "Id", "Id", datosfichatecnica.RecepcionEquipoId);
            return View(datosfichatecnica);
        }

        // GET: Datosfichatecnicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datosfichatecnica = await _context.Datosfichatecnicas
                .Include(d => d.RecepcionEquipo)
                .Include(s => s.RecepcionEquipo.IdServicioNavigation)
                .Include(s => s.RecepcionEquipo.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdDatosFichaTecnica == id);
            if (datosfichatecnica == null)
            {
                return NotFound();
            }

            return View(datosfichatecnica);
        }

        // POST: Datosfichatecnicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datosfichatecnica = await _context.Datosfichatecnicas.FindAsync(id);

            if (datosfichatecnica != null)
            {
                // Verificar si el registro está siendo referenciado en la tabla DiagnosticoSolucion
                bool isReferencedInDiagnosticoSolucion = await _context.Diagnosticosolucions
                    .AnyAsync(d => d.DatosFichaTecnicaId == id); // Verifica si la ficha técnica está en DiagnosticoSolucion

                if (isReferencedInDiagnosticoSolucion)
                {
                    // Si está referenciado, mostrar un mensaje de error y evitar la eliminación
                    TempData["ErrorMessage"] = "No se puede eliminar el registro porque está relacionado con un diagnóstico y solución.";

                    // Redirigir al detalle de la ficha técnica para que el usuario vea la alerta
                    return RedirectToAction("Details", new { id = id });
                }

                // Si no está referenciado, proceder con la eliminación
                _context.Datosfichatecnicas.Remove(datosfichatecnica);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index)); // Redirige al listado de las fichas técnicas
        }



        public IActionResult FichaToPdf(int id)
        {
            var datosFicha = _context.Datosfichatecnicas
                .Include(d => d.RecepcionEquipo)
                .ThenInclude(r => r.IdClienteNavigation)
                .Include(d => d.Diagnosticosolucions)
                .FirstOrDefault(d => d.IdDatosFichaTecnica == id);

            if (datosFicha == null)
            {
                return NotFound();
            }

            var pdfResult = new ViewAsPdf("FichaTopdf", datosFicha)
            {
                FileName = $"Fichatecnica_{id}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(10, 10, 10, 10)
            };

            return pdfResult;
        }
        [HttpGet]
        public async Task<IActionResult> DatosPorRecep(int id)
        {
            var fichaTecnica = await _context.Datosfichatecnicas
                .Where(d => d.RecepcionEquipoId == id)
                .Include(d => d.RecepcionEquipo) // Incluir la entidad de navegación
                .ToListAsync();

            if (fichaTecnica == null || !fichaTecnica.Any())
            {
                return RedirectToAction("Create", new { id });
            }

            return View(fichaTecnica);
        }

        [HttpPost]

        //tuve que refactorizar esta funcion para evitar ciertos detalles ademas de eliminar el apartado de 
        //ingresar el email del usuario, ahora lo envia directamente.
        public async Task<JsonResult> EnviarPdf(int id)
        {
            var fichaTecnica = await _context.Datosfichatecnicas
                .Include(f => f.RecepcionEquipo)
                    .ThenInclude(r => r.IdClienteNavigation)
                .Include(f => f.Diagnosticosolucions)
                .FirstOrDefaultAsync(f => f.IdDatosFichaTecnica == id);

            if (fichaTecnica == null)
            {
                return Json(new { success = false, message = "Ficha técnica no encontrada." });
            }

            var pdf = new ViewAsPdf("FichaToPdf", fichaTecnica)
            {
                FileName = "FichaTecnica.pdf"
            };

            var pdfBytes = await pdf.BuildFile(ControllerContext);

            try
            {
                // Esta funcion debe devolver una respuesta tipo bool - ajax trabaja junto a un script en la vista de la ficha
                // Para entender bien como se define el correo electronico, ir a EmailService.cs funcion de SendEmailWithAttatchmentAsync 
                //require 5 parametros respectivamente
                bool emailResult = await _emailService.SendEmailWithAttachmentAsync(
                    fichaTecnica.RecepcionEquipo.IdClienteNavigation.Correo,
                    "Ficha Técnica Mercy Developer",
                    "Hola junto con saludarle, Adjunto la ficha técnica solicitada por el servicio. Gracias por preferir Mercy Developers.",
                    pdfBytes,
                    "FichaTecnicaMercyDeveloper.pdf");

                return Json(new { success = emailResult });
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return Json(new { success = false, message = "Error al enviar el correo: " + ex.Message });
            }
        }





        private bool DatosfichatecnicaExists(int id)
        {
            return _context.Datosfichatecnicas.Any(e => e.IdDatosFichaTecnica == id);
        }
    }
}
