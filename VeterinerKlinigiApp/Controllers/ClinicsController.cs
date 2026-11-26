using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigiApp.Data;
using VeterinerKlinigiApp.Models;

namespace VeterinerKlinigiApp.Controllers
{
    public class ClinicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clinics
        public async Task<IActionResult> Index()
        {
            var clinics = await _context.Clinics.ToListAsync();
            return View(clinics);
        }

        // GET: Clinics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var clinic = await _context.Clinics
                .Include(c => c.Services)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (clinic == null) return NotFound();
            return View(clinic);
        }

        // GET: Clinics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clinics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Clinic clinic)
        {
            if (clinic.ClosingTime <= clinic.OpeningTime)
            {
                ModelState.AddModelError(nameof(clinic.ClosingTime), "Kapanış saati açılış saatinden sonra olmalıdır.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(clinic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        // GET: Clinics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null) return NotFound();
            return View(clinic);
        }

        // POST: Clinics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Clinic clinic)
        {
            if (id != clinic.Id) return NotFound();

            if (clinic.ClosingTime <= clinic.OpeningTime)
            {
                ModelState.AddModelError(nameof(clinic.ClosingTime), "Kapanış saati açılış saatinden sonra olmalıdır.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Clinics.AnyAsync(e => e.Id == clinic.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var clinic = await _context.Clinics.FirstOrDefaultAsync(m => m.Id == id);
            if (clinic == null) return NotFound();
            return View(clinic);
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}