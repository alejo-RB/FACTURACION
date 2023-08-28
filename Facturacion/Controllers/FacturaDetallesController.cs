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
    public class FacturaDetallesController : Controller
    {
        private readonly FacturaContext _context;

        public FacturaDetallesController(FacturaContext context)
        {
            _context = context;
        }

        // GET: FacturaDetalles
        public async Task<IActionResult> Index()
        {
            var facturaContext = _context.FacturaDetalles.Include(f => f.IdFacturaCabeceraNavigation).Include(f => f.IdProductoNavigation);
            return View(await facturaContext.ToListAsync());
        }

        // GET: FacturaDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FacturaDetalles == null)
            {
                return NotFound();
            }

            var facturaDetalle = await _context.FacturaDetalles
                .Include(f => f.IdFacturaCabeceraNavigation)
                .Include(f => f.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdFacturaDetalle == id);
            if (facturaDetalle == null)
            {
                return NotFound();
            }

            return View(facturaDetalle);
        }

        // GET: FacturaDetalles/Create
        public IActionResult Create()
        {
            ViewData["IdFacturaCabecera"] = new SelectList(_context.FacturaCabeceras, "IdFacturaCabecera", "IdFacturaCabecera");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: FacturaDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFacturaDetalle,IdFacturaCabecera,IdProducto,Cantidad,PrecioUnitario,Total")] FacturaDetalle facturaDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturaDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFacturaCabecera"] = new SelectList(_context.FacturaCabeceras, "IdFacturaCabecera", "IdFacturaCabecera", facturaDetalle.IdFacturaCabecera);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", facturaDetalle.IdProducto);
            return View(facturaDetalle);
        }

        // GET: FacturaDetalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FacturaDetalles == null)
            {
                return NotFound();
            }

            var facturaDetalle = await _context.FacturaDetalles.FindAsync(id);
            if (facturaDetalle == null)
            {
                return NotFound();
            }
            ViewData["IdFacturaCabecera"] = new SelectList(_context.FacturaCabeceras, "IdFacturaCabecera", "IdFacturaCabecera", facturaDetalle.IdFacturaCabecera);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", facturaDetalle.IdProducto);
            return View(facturaDetalle);
        }

        // POST: FacturaDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFacturaDetalle,IdFacturaCabecera,IdProducto,Cantidad,PrecioUnitario,Total")] FacturaDetalle facturaDetalle)
        {
            if (id != facturaDetalle.IdFacturaDetalle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturaDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaDetalleExists(facturaDetalle.IdFacturaDetalle))
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
            ViewData["IdFacturaCabecera"] = new SelectList(_context.FacturaCabeceras, "IdFacturaCabecera", "IdFacturaCabecera", facturaDetalle.IdFacturaCabecera);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", facturaDetalle.IdProducto);
            return View(facturaDetalle);
        }

        // GET: FacturaDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FacturaDetalles == null)
            {
                return NotFound();
            }

            var facturaDetalle = await _context.FacturaDetalles
                .Include(f => f.IdFacturaCabeceraNavigation)
                .Include(f => f.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdFacturaDetalle == id);
            if (facturaDetalle == null)
            {
                return NotFound();
            }

            return View(facturaDetalle);
        }

        // POST: FacturaDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FacturaDetalles == null)
            {
                return Problem("Entity set 'FacturaContext.FacturaDetalles'  is null.");
            }
            var facturaDetalle = await _context.FacturaDetalles.FindAsync(id);
            if (facturaDetalle != null)
            {
                _context.FacturaDetalles.Remove(facturaDetalle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaDetalleExists(int id)
        {
          return (_context.FacturaDetalles?.Any(e => e.IdFacturaDetalle == id)).GetValueOrDefault();
        }
    }
}
