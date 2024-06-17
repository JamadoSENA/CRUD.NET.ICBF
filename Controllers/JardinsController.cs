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
    public class JardinsController : Controller
    {
        private readonly IcbfContext _context;

        public JardinsController(IcbfContext context)
        {
            _context = context;
        }

        // GET: Jardins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jardins.ToListAsync());
        }

        // GET: Jardins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jardin = await _context.Jardins
                .FirstOrDefaultAsync(m => m.IdentificadorJardin == id);
            if (jardin == null)
            {
                return NotFound();
            }

            return View(jardin);
        }

        // GET: Jardins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jardins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdentificadorJardin,NombreJardin,Direccion,Estado")] Jardin jardin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jardin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jardin);
        }

        // GET: Jardins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jardin = await _context.Jardins.FindAsync(id);
            if (jardin == null)
            {
                return NotFound();
            }
            return View(jardin);
        }

        // POST: Jardins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdentificadorJardin,NombreJardin,Direccion,Estado")] Jardin jardin)
        {
            if (id != jardin.IdentificadorJardin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jardin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JardinExists(jardin.IdentificadorJardin))
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
            return View(jardin);
        }

        // GET: Jardins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jardin = await _context.Jardins
                .FirstOrDefaultAsync(m => m.IdentificadorJardin == id);
            if (jardin == null)
            {
                return NotFound();
            }

            return View(jardin);
        }

        // POST: Jardins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jardin = await _context.Jardins.FindAsync(id);
            if (jardin != null)
            {
                _context.Jardins.Remove(jardin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JardinExists(int id)
        {
            return _context.Jardins.Any(e => e.IdentificadorJardin == id);
        }
    }
}
