﻿using System;
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

    public class DescripcionserviciosController : Controller
    {

        private readonly MercyDeveloperContext _context;

        public DescripcionserviciosController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Descripcionservicios
        public async Task<IActionResult> Index()
        {
            var mercydevsEjercicio2Context = _context.Descripcionservicios.Include(d => d.ServicioIdServicioNavigation);
            return View(await mercydevsEjercicio2Context.ToListAsync());
        }

        // GET: Descripcionservicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionservicio = await _context.Descripcionservicios
                .Include(d => d.ServicioIdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdDescServ == id);
            if (descripcionservicio == null)
            {
                return NotFound();
            }

            return View(descripcionservicio);
        }

        // GET: Descripcionservicios/Create
        public IActionResult Create(int? id)
        {
            ViewData["ServicioIdServicio"] = new SelectList(_context.Servicios, "IdServicio", "Nombre");
            ViewData["IdServicio"] = id;
            return View();
        }

        // POST: Descripcionservicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id,[Bind("IdDescServ,Nombre,ServicioIdServicio")] Descripcionservicio descripcionservicio)
        {
            if (descripcionservicio.Nombre != null)
            {
                descripcionservicio.ServicioIdServicio = Convert.ToInt32(id);
                _context.Add(descripcionservicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServicioIdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", descripcionservicio.ServicioIdServicio);
            return View(descripcionservicio);
        }

        // GET: Descripcionservicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionservicio = await _context.Descripcionservicios.FindAsync(id);
            if (descripcionservicio == null)
            {
                return NotFound();
            }
            ViewData["ServicioIdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", descripcionservicio.ServicioIdServicio);
            return View(descripcionservicio);
        }

        // POST: Descripcionservicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDescServ,Nombre,ServicioIdServicio")] Descripcionservicio descripcionservicio)
        {
            if (id != descripcionservicio.IdDescServ)
            {
                return NotFound();
            }

            if (descripcionservicio.Nombre != null)
            {
                try
                {
                    _context.Update(descripcionservicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescripcionservicioExists(descripcionservicio.IdDescServ))
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
            ViewData["ServicioIdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", descripcionservicio.ServicioIdServicio);
            return View(descripcionservicio);
        }

        // GET: Descripcionservicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionservicio = await _context.Descripcionservicios
                .Include(d => d.ServicioIdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdDescServ == id);
            if (descripcionservicio == null)
            {
                return NotFound();
            }

            return View(descripcionservicio);
        }

        // POST: Descripcionservicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var descripcionservicio = await _context.Descripcionservicios.FindAsync(id);
            if (descripcionservicio != null)
            {
                _context.Descripcionservicios.Remove(descripcionservicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescripcionservicioExists(int id)
        {
            return _context.Descripcionservicios.Any(e => e.IdDescServ == id);
        }
    }
}
