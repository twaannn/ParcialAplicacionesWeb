using AplicacionesWeb.Data;
using AplicacionesWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AplicacionesWeb.Controllers
{
    public class PersonaController : Controller
    {
        private readonly AppDbContext _db;
        public PersonaController(AppDbContext db)
        {
            _db = db;
        }

        // GET: Persona
        public async Task<IActionResult> Index()
        {
            var people = await _db.Personas
            .Where(p => p is Persona && !(p is Estudiante))
            .AsNoTracking()
            .ToListAsync();
            return View(people);

        }

        // GET: Persona/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null) return NotFound();

            var person = await _db.Personas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id && !(m is Estudiante));

            if (person == null) return NotFound();
            return View(person);

        }

        // GET: Persona/Create
        public IActionResult Create() => View();

        // POST: Persona/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Edad")] Persona person)
        {
            if (!ModelState.IsValid) return View(person);

            _db.Add(person);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Persona/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();

            var person = await _db.Personas.FirstOrDefaultAsync(p => p.Id == id && !(p is Estudiante));
            if (person == null) return NotFound();

            return View(person);


        }
        // GET: Persona/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _db.Personas
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && !(p is Estudiante));

            if (person == null) return NotFound();

            return View(person);
        }

        // POST: Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _db.Personas
                .FirstOrDefaultAsync(p => p.Id == id && !(p is Estudiante));

            if (person == null) return NotFound();

            _db.Personas.Remove(person);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}