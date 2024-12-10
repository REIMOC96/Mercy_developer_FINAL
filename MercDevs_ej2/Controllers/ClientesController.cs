using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercDevs_ej2.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;


namespace MercDevs_ej2.Controllers
{
    [Authorize]

    public class ClientesController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public ClientesController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Debug.WriteLine("console wirle line");
            Debug.Write("write");
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Nombre,Apellido,Correo,Telefono,Direccion,Estado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                // consultamos a la bbdd por el correo del cliente, y comparamos el correo entregado del cliente hacia la bbbd
                //como es tipo bool, el resultado del contenido de la variante es true o false, eso me permite hacer el if..
                bool emailExists = await _context.Clientes.AnyAsync(c => c.Correo == cliente.Correo);

                if (emailExists)
                {
                    // si esta en l bbdd agregas error al modelo hacia el apartado de correo
                    ModelState.AddModelError("Correo", "El correo electrónico ya está registrado.");
                }
                else
                {
                    // Si no está registrado, agregar el cliente a la base de datos
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            // Si algo sale mal o el correo ya existe, volver la vista y mostrar los errores
            return View(cliente);
        }



        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Nombre,Apellido,Correo,Telefono,Direccion,Estado")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Buscar el cliente por su ID
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            // Verificar si el cliente tiene registros relacionados en RecepcionEquipos
            bool tieneForanea = await _context.Recepcionequipos.AnyAsync(r => r.IdCliente == id);

            if (tieneForanea)

                if (tieneForanea)
                {
                    TempData["ErrorMessage"] = "No se puede eliminar este registro porque está relacionado con otros datos tecnicos.";
                    return RedirectToAction(nameof(Delete), new { id });
                }


            // Si no hay registros relacionados, eliminar el cliente
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
