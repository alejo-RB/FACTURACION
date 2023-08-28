using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Facturacion.Models;

namespace Facturacion.Controllers
{
    public class FacturaCabecerasController : Controller
    {
        private readonly FacturaContext _context;

        public FacturaCabecerasController(FacturaContext context)
        {
            _context = context;
        }

        // GET: FacturaCabeceras
        public async Task<IActionResult> Index()
        {
            var facturaContext = _context.FacturaCabeceras.Include(f => f.IdClienteNavigation);
            return View(await facturaContext.ToListAsync());
        }

        // GET: FacturaCabeceras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FacturaCabeceras == null)
            {
                return NotFound();
            }

            var facturaCabecera = await _context.FacturaCabeceras
                .Include(f => f.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdFacturaCabecera == id);
            if (facturaCabecera == null)
            {
                return NotFound();
            }

            return View(facturaCabecera);
        }

        // GET: FacturaCabeceras/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            return View();
        }

        // POST: FacturaCabeceras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFacturaCabecera,FechaFactura,IdCliente,Total")] FacturaCabecera facturaCabecera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturaCabecera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", facturaCabecera.IdCliente);
            return View(facturaCabecera);
        }

        // GET: FacturaCabeceras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FacturaCabeceras == null)
            {
                return NotFound();
            }

            var facturaCabecera = await _context.FacturaCabeceras.FindAsync(id);
            if (facturaCabecera == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", facturaCabecera.IdCliente);
            return View(facturaCabecera);
        }

        // POST: FacturaCabeceras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFacturaCabecera,FechaFactura,IdCliente,Total")] FacturaCabecera facturaCabecera)
        {
            if (id != facturaCabecera.IdFacturaCabecera)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturaCabecera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaCabeceraExists(facturaCabecera.IdFacturaCabecera))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", facturaCabecera.IdCliente);
            return View(facturaCabecera);
        }

        // GET: FacturaCabeceras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FacturaCabeceras == null)
            {
                return NotFound();
            }

            var facturaCabecera = await _context.FacturaCabeceras
                .Include(f => f.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdFacturaCabecera == id);
            if (facturaCabecera == null)
            {
                return NotFound();
            }

            return View(facturaCabecera);
        }

        // POST: FacturaCabeceras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FacturaCabeceras == null)
            {
                return Problem("Entity set 'FacturaContext.FacturaCabeceras'  is null.");
            }
            var facturaCabecera = await _context.FacturaCabeceras.FindAsync(id);
            if (facturaCabecera != null)
            {
                _context.FacturaCabeceras.Remove(facturaCabecera);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaCabeceraExists(int id)
        {
          return (_context.FacturaCabeceras?.Any(e => e.IdFacturaCabecera == id)).GetValueOrDefault();
        }
    }
}
