using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercDevs_ej2.Models;

namespace MercDevs_ej2.Controllers
{
    public class DiagnosticosolucionsController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public DiagnosticosolucionsController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Diagnosticosolucions
        public async Task<IActionResult> Index()
        {
            var mercyDeveloperContext = _context.Diagnosticosolucions.Include(d => d.DatosFichaTecnica);
            return View(await mercyDeveloperContext.ToListAsync());
        }

        // GET: Diagnosticosolucions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosticosolucion = await _context.Diagnosticosolucions
                .Include(d => d.DatosFichaTecnica)
                .FirstOrDefaultAsync(m => m.IdDiagnosticoSolucion == id);
            if (diagnosticosolucion == null)
            {
                return NotFound();
            }

            return View(diagnosticosolucion);
        }

        // GET: Diagnosticosolucions/Create
        public IActionResult Create()
        {
            ViewData["DatosFichaTecnicaId"] = new SelectList(_context.Datosfichatecnicas, "IdDatosFichaTecnica", "IdDatosFichaTecnica");
            return View();
        }

        // POST: Diagnosticosolucions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDiagnosticoSolucion,DescripcionDiagnostico,DescripcionSolucion,DatosFichaTecnicaId")] Diagnosticosolucion diagnosticosolucion)
        {
            if (diagnosticosolucion.DescripcionSolucion != null)
            {
                _context.Add(diagnosticosolucion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DatosFichaTecnicaId"] = new SelectList(_context.Datosfichatecnicas, "IdDatosFichaTecnica", "IdDatosFichaTecnica", diagnosticosolucion.DatosFichaTecnicaId);
            return View(diagnosticosolucion);
        }

        // GET: Diagnosticosolucions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosticosolucion = await _context.Diagnosticosolucions.FindAsync(id);
            if (diagnosticosolucion == null)
            {
                return NotFound();
            }
            ViewData["DatosFichaTecnicaId"] = new SelectList(_context.Datosfichatecnicas, "IdDatosFichaTecnica", "IdDatosFichaTecnica", diagnosticosolucion.DatosFichaTecnicaId);
            return View(diagnosticosolucion);
        }

        // POST: Diagnosticosolucions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDiagnosticoSolucion,DescripcionDiagnostico,DescripcionSolucion,DatosFichaTecnicaId")] Diagnosticosolucion diagnosticosolucion)
        {
            if (id != diagnosticosolucion.IdDiagnosticoSolucion)
            {
                return NotFound();
            }

            if (diagnosticosolucion.DescripcionSolucion != null)
            {
                try
                {
                    _context.Update(diagnosticosolucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticosolucionExists(diagnosticosolucion.IdDiagnosticoSolucion))
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
            ViewData["DatosFichaTecnicaId"] = new SelectList(_context.Datosfichatecnicas, "IdDatosFichaTecnica", "IdDatosFichaTecnica", diagnosticosolucion.DatosFichaTecnicaId);
            return View(diagnosticosolucion);
        }

        // GET: Diagnosticosolucions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosticosolucion = await _context.Diagnosticosolucions
                .Include(d => d.DatosFichaTecnica)
                .FirstOrDefaultAsync(m => m.IdDiagnosticoSolucion == id);
            if (diagnosticosolucion == null)
            {
                return NotFound();
            }

            return View(diagnosticosolucion);
        }

        // POST: Diagnosticosolucions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnosticosolucion = await _context.Diagnosticosolucions.FindAsync(id);
            if (diagnosticosolucion != null)
            {
                _context.Diagnosticosolucions.Remove(diagnosticosolucion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticosolucionExists(int id)
        {
            return _context.Diagnosticosolucions.Any(e => e.IdDiagnosticoSolucion == id);
        }
    }
}
