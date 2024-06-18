using CrudICBF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CrudICBF.Controllers
{
    public class AcudientesController : Controller
    {
        private readonly IcbfContext _context;

        public AcudientesController(IcbfContext context)
        {
            _context = context;
        }

        // GET: Acudientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Acudientes.ToListAsync());
        }

        // GET: Acudientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acudiente = await _context.Acudientes
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (acudiente == null)
            {
                return NotFound();
            }

            return View(acudiente);
        }

        // GET: Acudientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Acudientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,Telefono,Celular,Direccion,Correo")] Acudiente acudiente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acudiente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(acudiente);
        }

        // GET: Acudientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acudiente = await _context.Acudientes.FindAsync(id);
            if (acudiente == null)
            {
                return NotFound();
            }
            return View(acudiente);
        }

        // POST: Acudientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cedula,Nombre,Telefono,Celular,Direccion,Correo")] Acudiente acudiente)
        {
            if (id != acudiente.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acudiente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcudienteExists(acudiente.Cedula))
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
            return View(acudiente);
        }

        // GET: Acudientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acudiente = await _context.Acudientes
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (acudiente == null)
            {
                return NotFound();
            }

            return View(acudiente);
        }

        // POST: Acudientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acudiente = await _context.Acudientes.FindAsync(id);
            if (acudiente != null)
            {
                _context.Acudientes.Remove(acudiente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcudienteExists(int id)
        {
            return _context.Acudientes.Any(e => e.Cedula == id);
        }
    }
}
