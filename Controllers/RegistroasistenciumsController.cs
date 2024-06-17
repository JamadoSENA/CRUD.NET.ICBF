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
    public class RegistroasistenciumsController : Controller
    {
        private readonly IcbfContext _context;

        public RegistroasistenciumsController(IcbfContext context)
        {
            _context = context;
        }

        // GET: Registroasistenciums
        public async Task<IActionResult> Index()
        {
            var icbfContext = _context.Registroasistencia.Include(r => r.IdentificacionNinoNavigation);
            return View(await icbfContext.ToListAsync());
        }

        // GET: Registroasistenciums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroasistencium = await _context.Registroasistencia
                .Include(r => r.IdentificacionNinoNavigation)
                .FirstOrDefaultAsync(m => m.RegistroAsistencia == id);
            if (registroasistencium == null)
            {
                return NotFound();
            }

            return View(registroasistencium);
        }

        // GET: Registroasistenciums/Create
        public IActionResult Create()
        {
            ViewData["IdentificacionNino"] = new SelectList(_context.Ninios, "RegistroNiup", "RegistroNiup");
            return View();
        }

        // POST: Registroasistenciums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistroAsistencia,Fecha,Estado,IdentificacionNino")] Registroasistencium registroasistencium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroasistencium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentificacionNino"] = new SelectList(_context.Ninios, "RegistroNiup", "RegistroNiup", registroasistencium.IdentificacionNino);
            return View(registroasistencium);
        }

        // GET: Registroasistenciums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroasistencium = await _context.Registroasistencia.FindAsync(id);
            if (registroasistencium == null)
            {
                return NotFound();
            }
            ViewData["IdentificacionNino"] = new SelectList(_context.Ninios, "RegistroNiup", "RegistroNiup", registroasistencium.IdentificacionNino);
            return View(registroasistencium);
        }

        // POST: Registroasistenciums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistroAsistencia,Fecha,Estado,IdentificacionNino")] Registroasistencium registroasistencium)
        {
            if (id != registroasistencium.RegistroAsistencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroasistencium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroasistenciumExists(registroasistencium.RegistroAsistencia))
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
            ViewData["IdentificacionNino"] = new SelectList(_context.Ninios, "RegistroNiup", "RegistroNiup", registroasistencium.IdentificacionNino);
            return View(registroasistencium);
        }

        // GET: Registroasistenciums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroasistencium = await _context.Registroasistencia
                .Include(r => r.IdentificacionNinoNavigation)
                .FirstOrDefaultAsync(m => m.RegistroAsistencia == id);
            if (registroasistencium == null)
            {
                return NotFound();
            }

            return View(registroasistencium);
        }

        // POST: Registroasistenciums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroasistencium = await _context.Registroasistencia.FindAsync(id);
            if (registroasistencium != null)
            {
                _context.Registroasistencia.Remove(registroasistencium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroasistenciumExists(int id)
        {
            return _context.Registroasistencia.Any(e => e.RegistroAsistencia == id);
        }
    }
}
