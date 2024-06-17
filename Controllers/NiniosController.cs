using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudICBF.Models;

namespace CrudICBF.Controllers
{
    public class NiniosController : Controller
    {
        private readonly IcbfContext _context;

        public NiniosController(IcbfContext context)
        {
            _context = context;
        }

        // GET: Ninios
        public async Task<IActionResult> Index()
        {
            var icbfContext = _context.Ninios.Include(n => n.IdentificacionAcudienteNavigation).Include(n => n.IdentificacionMadreComNavigation).Include(n => n.IdentificadorJardinNavigation);
            return View(await icbfContext.ToListAsync());
        }

        // GET: Ninios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninio = await _context.Ninios
                .Include(n => n.IdentificacionAcudienteNavigation)
                .Include(n => n.IdentificacionMadreComNavigation)
                .Include(n => n.IdentificadorJardinNavigation)
                .FirstOrDefaultAsync(m => m.RegistroNiup == id);
            if (ninio == null)
            {
                return NotFound();
            }

            return View(ninio);
        }

        // GET: Ninios/Create
        public IActionResult Create()
        {
            ViewData["IdentificacionAcudiente"] = new SelectList(_context.Acudientes, "Cedula", "Cedula");
            ViewData["IdentificacionMadreCom"] = new SelectList(_context.Madrecoms, "Cedula", "Cedula");
            ViewData["IdentificadorJardin"] = new SelectList(_context.Jardins, "IdentificadorJardin", "IdentificadorJardin");
            return View();
        }

        // POST: Ninios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistroNiup,Nombre,FechaNacimiento,TipoSangre,CiudadNacimiento,Direccion,Eps,IdentificacionAcudiente,IdentificacionMadreCom,IdentificadorJardin")] Ninio ninio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ninio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentificacionAcudiente"] = new SelectList(_context.Acudientes, "Cedula", "Cedula", ninio.IdentificacionAcudiente);
            ViewData["IdentificacionMadreCom"] = new SelectList(_context.Madrecoms, "Cedula", "Cedula", ninio.IdentificacionMadreCom);
            ViewData["IdentificadorJardin"] = new SelectList(_context.Jardins, "IdentificadorJardin", "IdentificadorJardin", ninio.IdentificadorJardin);
            return View(ninio);
        }

        // GET: Ninios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninio = await _context.Ninios.FindAsync(id);
            if (ninio == null)
            {
                return NotFound();
            }
            ViewData["IdentificacionAcudiente"] = new SelectList(_context.Acudientes, "Cedula", "Cedula", ninio.IdentificacionAcudiente);
            ViewData["IdentificacionMadreCom"] = new SelectList(_context.Madrecoms, "Cedula", "Cedula", ninio.IdentificacionMadreCom);
            ViewData["IdentificadorJardin"] = new SelectList(_context.Jardins, "IdentificadorJardin", "IdentificadorJardin", ninio.IdentificadorJardin);
            return View(ninio);
        }

        // POST: Ninios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistroNiup,Nombre,FechaNacimiento,TipoSangre,CiudadNacimiento,Direccion,Eps,IdentificacionAcudiente,IdentificacionMadreCom,IdentificadorJardin")] Ninio ninio)
        {
            if (id != ninio.RegistroNiup)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ninio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinioExists(ninio.RegistroNiup))
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
            ViewData["IdentificacionAcudiente"] = new SelectList(_context.Acudientes, "Cedula", "Cedula", ninio.IdentificacionAcudiente);
            ViewData["IdentificacionMadreCom"] = new SelectList(_context.Madrecoms, "Cedula", "Cedula", ninio.IdentificacionMadreCom);
            ViewData["IdentificadorJardin"] = new SelectList(_context.Jardins, "IdentificadorJardin", "IdentificadorJardin", ninio.IdentificadorJardin);
            return View(ninio);
        }

        // GET: Ninios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninio = await _context.Ninios
                .Include(n => n.IdentificacionAcudienteNavigation)
                .Include(n => n.IdentificacionMadreComNavigation)
                .Include(n => n.IdentificadorJardinNavigation)
                .FirstOrDefaultAsync(m => m.RegistroNiup == id);
            if (ninio == null)
            {
                return NotFound();
            }

            return View(ninio);
        }

        // POST: Ninios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ninio = await _context.Ninios.FindAsync(id);
            if (ninio != null)
            {
                _context.Ninios.Remove(ninio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinioExists(int id)
        {
            return _context.Ninios.Any(e => e.RegistroNiup == id);
        }
    }
}
