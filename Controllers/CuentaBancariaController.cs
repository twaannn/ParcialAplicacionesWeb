using AplicacionesWeb.Data;
using AplicacionesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AplicacionesWeb.Controllers
{
    public class CuentaBancariaController : Controller
    {
        private readonly AppDbContext _db;

        public CuentaBancariaController(AppDbContext db)
        {
            _db = db;
        }

        // GET: CuentaBancaria
        public async Task<IActionResult> Index()
        {
            var cuentas = await _db.CuentasBancarias
                .AsNoTracking()
                .ToListAsync();

            return View(cuentas);
        }

        // GET: CuentaBancaria/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cuenta = await _db.CuentasBancarias
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cuenta == null)
                return NotFound();

            return View(cuenta);
        }

        // GET: CuentaBancaria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CuentaBancaria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroCuenta,Saldo")] CuentaBancaria cuenta)
        {
            if (!ModelState.IsValid)
                return View(cuenta);

            _db.CuentasBancarias.Add(cuenta);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: CuentaBancaria/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cuenta = await _db.CuentasBancarias.FindAsync(id);

            if (cuenta == null)
                return NotFound();

            return View(cuenta);
        }

        // POST: CuentaBancaria/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroCuenta,Saldo")] CuentaBancaria cuenta)
        {
            if (id != cuenta.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(cuenta);

            try
            {
                _db.CuentasBancarias.Update(cuenta);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaBancariaExists(cuenta.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CuentaBancaria/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cuenta = await _db.CuentasBancarias
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cuenta == null)
                return NotFound();

            return View(cuenta);
        }

        // POST: CuentaBancaria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuenta = await _db.CuentasBancarias.FindAsync(id);

            if (cuenta == null)
                return NotFound();

            _db.CuentasBancarias.Remove(cuenta);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: CuentaBancaria/SaldoDisponible/5
        public async Task<IActionResult> SaldoDisponible(int id)
        {
            var cuenta = await _db.CuentasBancarias
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cuenta == null)
                return NotFound();

            ViewBag.SaldoDisponible = cuenta.ObtenerSaldo();
            return View(cuenta);
        }

        // GET: CuentaBancaria/Retirar/5
        public async Task<IActionResult> Retirar(int id)
        {
            var cuenta = await _db.CuentasBancarias
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cuenta == null)
                return NotFound();

            return View(cuenta);
        }

        // POST: CuentaBancaria/Retirar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Retirar(int id, decimal cantidad)
        {
            var cuenta = await _db.CuentasBancarias.FindAsync(id);

            if (cuenta == null)
                return NotFound();

            var retirado = cuenta.Retirar(cantidad);

            if (retirado == 0)
            {
                ViewBag.Mensaje = "No se pudo realizar el retiro. Verifique la cantidad o el saldo disponible.";
                return View(cuenta);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = cuenta.Id });
        }

        private bool CuentaBancariaExists(int id)
        {
            return _db.CuentasBancarias.Any(c => c.Id == id);
        }
    }
}