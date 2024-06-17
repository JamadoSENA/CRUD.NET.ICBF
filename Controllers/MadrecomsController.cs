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
    public class MadrecomsController : Controller
    {
        private readonly IcbfContext _context;

        public MadrecomsController(IcbfContext context)
        {
            _context = context;
        }

        // GET: Madrecoms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Madrecoms.ToListAsync());
        }

        // GET: Madrecoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var madrecom = await _context.Madrecoms
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (madrecom == null)
            {
                return NotFound();
            }

            return View(madrecom);
        }

        // GET: Madrecoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Madrecoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,Telefono,Direccion,FechaNacimiento")] Madrecom madrecom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(madrecom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(madrecom);
        }

        // GET: Madrecoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var madrecom = await _context.Madrecoms.FindAsync(id);
            if (madrecom == null)
            {
                return NotFound();
            }
            return View(madrecom);
        }

        // POST: Madrecoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cedula,Nombre,Telefono,Direccion,FechaNacimiento")] Madrecom madrecom)
        {
            if (id != madrecom.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(madrecom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MadrecomExists(madrecom.Cedula))
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
            return View(madrecom);
        }

        // GET: Madrecoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var madrecom = await _context.Madrecoms
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (madrecom == null)
            {
                return NotFound();
            }

            return View(madrecom);
        }

        // POST: Madrecoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var madrecom = await _context.Madrecoms.FindAsync(id);
            if (madrecom != null)
            {
                _context.Madrecoms.Remove(madrecom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MadrecomExists(int id)
        {
            return _context.Madrecoms.Any(e => e.Cedula == id);
        }
    }
}
