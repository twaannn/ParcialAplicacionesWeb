using AplicacionesWeb.Data;
using AplicacionesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AplicacionesWeb.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly AppDbContext _db;

        public EstudianteController(AppDbContext db)
        {
            _db = db;
        }

        // GET: Estudiante
        public async Task<IActionResult> Index()
        {
            var estudiantes = await _db.Personas
                .OfType<Estudiante>()
                .AsNoTracking()
                .ToListAsync();

            return View(estudiantes);
        }

        // GET: Estudiante/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var estudiante = await _db.Personas
                .OfType<Estudiante>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        // GET: Estudiante/Create
        public IActionResult Create() => View();

        // POST: Estudiante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Edad,Matricula")] Estudiante estudiante)
        {
            if (!ModelState.IsValid) return View(estudiante);

            _db.Add(estudiante);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Estudiante/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var estudiante = await _db.Personas
                .OfType<Estudiante>()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        // POST: Estudiante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Edad,Matricula")] Estudiante estudiante)
        {
            if (id != estudiante.Id) return NotFound();

            if (!ModelState.IsValid) return View(estudiante);

            try
            {
                _db.Update(estudiante);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool existe = await _db.Personas
                    .OfType<Estudiante>()
                    .AnyAsync(e => e.Id == estudiante.Id);

                if (!existe) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Estudiante/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _db.Personas
                .OfType<Estudiante>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        // POST: Estudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _db.Personas
                .OfType<Estudiante>()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null) return NotFound();

            _db.Personas.Remove(estudiante);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}