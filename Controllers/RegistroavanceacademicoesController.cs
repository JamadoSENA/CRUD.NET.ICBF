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
    public class RegistroavanceacademicoesController : Controller
    {
        private readonly IcbfContext _context;

        public RegistroavanceacademicoesController(IcbfContext context)
        {
            _context = context;
        }

        // GET: Registroavanceacademicoes
        public async Task<IActionResult> Index()
        {
            var icbfContext = _context.Registroavanceacademicos.Include(r => r.IdentificacionNinoNavigation);
            return View(await icbfContext.ToListAsync());
        }

        // GET: Registroavanceacademicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroavanceacademico = await _context.Registroavanceacademicos
                .Include(r => r.IdentificacionNinoNavigation)
                .FirstOrDefaultAsync(m => m.RegistroAvance == id);
            if (registroavanceacademico == null)
            {
                return NotFound();
            }

            return View(registroavanceacademico);
        }

        // GET: Registroavanceacademicoes/Create
        public IActionResult Create()
        {
            ViewData["IdentificacionNino"] = new SelectList(_context.Ninios, "RegistroNiup", "RegistroNiup");
            return View();
        }

        // POST: Registroavanceacademicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistroAvance,AnioEscolar,Nivel,Notas,Descripcion,FechaEntregaNota,IdentificacionNino")] Registroavanceacademico registroavanceacademico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroavanceacademico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentificacionNino"] = new SelectList(_context.Ninios, "RegistroNiup", "RegistroNiup", registroavanceacademico.IdentificacionNino);
            return View(registroavanceacademico);
        }

        // GET: Registroavanceacademicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroavanceacademico = await _context.Registroavanceacademicos.FindAsync(id);
            if (registroavanceacademico == null)
            {
                return NotFound();
            }
            ViewData["IdentificacionNino"] = new SelectList(_context.Ninios, "RegistroNiup", "RegistroNiup", registroavanceacademico.IdentificacionNino);
            return View(registroavanceacademico);
        }

        // POST: Registroavanceacademicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistroAvance,AnioEscolar,Nivel,Notas,Descripcion,FechaEntregaNota,IdentificacionNino")] Registroavanceacademico registroavanceacademico)
        {
            if (id != registroavanceacademico.RegistroAvance)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroavanceacademico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroavanceacademicoExists(registroavanceacademico.RegistroAvance))
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
            ViewData["IdentificacionNino"] = new SelectList(_context.Ninios, "RegistroNiup", "RegistroNiup", registroavanceacademico.IdentificacionNino);
            return View(registroavanceacademico);
        }

        // GET: Registroavanceacademicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroavanceacademico = await _context.Registroavanceacademicos
                .Include(r => r.IdentificacionNinoNavigation)
                .FirstOrDefaultAsync(m => m.RegistroAvance == id);
            if (registroavanceacademico == null)
            {
                return NotFound();
            }

            return View(registroavanceacademico);
        }

        // POST: Registroavanceacademicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroavanceacademico = await _context.Registroavanceacademicos.FindAsync(id);
            if (registroavanceacademico != null)
            {
                _context.Registroavanceacademicos.Remove(registroavanceacademico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroavanceacademicoExists(int id)
        {
            return _context.Registroavanceacademicos.Any(e => e.RegistroAvance == id);
        }
    }
}
