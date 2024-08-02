using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercDevs_ej2.Models;
using Rotativa.AspNetCore;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace MercDevs_ej2.Controllers
{
    public class DatosfichatecnicasController : Controller
    {
        private readonly MercyDeveloperContext _context;
        private readonly IConfiguration _configuration;

        public DatosfichatecnicasController(MercyDeveloperContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id,[Bind("IdDatosFichaTecnica,FechaInicio,FechaFinalizacion,PobservacionesRecomendaciones,Soinstalado,SuiteOfficeInstalada,LectorPdfinstalado,NavegadorWebInstalado,AntivirusInstalado,RecepcionEquipoId,Estado")] Datosfichatecnica datosfichatecnica)
        {
            if (datosfichatecnica.FechaInicio != null)
            {
                datosfichatecnica.RecepcionEquipoId = Convert.ToInt32(id);
                _context.Add(datosfichatecnica);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Recepcionequipoes");
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("IdDatosFichaTecnica,FechaInicio,FechaFinalizacion,PobservacionesRecomendaciones,Soinstalado,SuiteOfficeInstalada,LectorPdfinstalado,NavegadorWebInstalado,AntivirusInstalado,RecepcionEquipoId")] Datosfichatecnica datosfichatecnica)
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
                _context.Datosfichatecnicas.Remove(datosfichatecnica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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


        //esto costo pero se pudo requirio varias cosas que desconocia u.u
public async Task<IActionResult> EnviarPdf(int id, string email)
{
    var fichaTecnica = await _context.Datosfichatecnicas
        .Include(f => f.RecepcionEquipo)
        .ThenInclude(r => r.IdClienteNavigation)
        .Include(f => f.Diagnosticosolucions)
        .FirstOrDefaultAsync(f => f.IdDatosFichaTecnica == id);

    if (fichaTecnica == null)
    {
        return NotFound();
    }

    var pdf = new ViewAsPdf("FichaToPdf", fichaTecnica)
    {
        FileName = "FichaTecnica.pdf"
    };

    var pdfBytes = await pdf.BuildFile(ControllerContext);

    var smtpSettings = _configuration.GetSection("Smtp").Get<SmtpSettings>();

    // smtp tuviste feo
    if (smtpSettings == null ||
        string.IsNullOrEmpty(smtpSettings.Username) ||
        string.IsNullOrEmpty(smtpSettings.Host) ||
        smtpSettings.Port <= 0 ||
        string.IsNullOrEmpty(smtpSettings.Password))
    {
        return StatusCode(StatusCodes.Status500InternalServerError, "Configuración SMTP incompleta.");
    }

    var smtpClient = new SmtpClient(smtpSettings.Host)
    {
        Port = smtpSettings.Port,
        Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
        EnableSsl = true,
    };

    var mailMessage = new MailMessage
    {
        From = new MailAddress(smtpSettings.Username), // Esto debería estar bien ahora
        Subject = "Ficha Técnica Mercy Developer",
        Body = "Hola buenos dias, Adjunto la Ficha tecnica solicitada por el servicio, Gracias por preferir Mercy Developers",
        IsBodyHtml = true,
    };

    mailMessage.To.Add(email);
    mailMessage.Attachments.Add(new Attachment(new MemoryStream(pdfBytes), "FichaTecnica.pdf"));

    try
    {
        await smtpClient.SendMailAsync(mailMessage);
        TempData["Success"] = "El correo ha sido enviado exitosamente.";
    }
    catch (Exception ex)
    {
        TempData["Error"] = "Hubo un error al enviar el correo: " + ex.Message;
    }

    return RedirectToAction(nameof(Index), new { id});
}









        private bool DatosfichatecnicaExists(int id)
        {
            return _context.Datosfichatecnicas.Any(e => e.IdDatosFichaTecnica == id);
        }
    }
}
