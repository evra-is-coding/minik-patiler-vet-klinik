using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigiApp.Data;
using VeterinerKlinigiApp.Models;

namespace VeterinerKlinigiApp.Controllers
{
    public class VeterinarianAvailabilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeterinarianAvailabilitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VeterinarianAvailabilities
        public async Task<IActionResult> Index()
        {
            var availabilities = await _context.VeterinarianAvailabilities
                .Include(a => a.Veterinarian)
                    .ThenInclude(v => v!.Clinic)
                .OrderBy(a => a.StartTime)
                .ToListAsync();

            return View(availabilities);
        }

        // GET: VeterinarianAvailabilities/Create
        public async Task<IActionResult> Create(int? veterinarianId = null)
        {
            await PopulateVeterinariansSelectListAsync(veterinarianId);

            var now = DateTime.Now;
            var defaultStart = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);
            var defaultEnd = new DateTime(now.Year, now.Month, now.Day, 17, 0, 0);

            var model = new VeterinarianAvailability
            {
                VeterinarianId = veterinarianId ?? 0,
                StartTime = defaultStart,
                EndTime = defaultEnd
            };

            return View(model);
        }

        // POST: VeterinarianAvailabilities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VeterinarianAvailability availability)
        {
            if (availability.EndTime <= availability.StartTime)
            {
                ModelState.AddModelError(nameof(availability.EndTime), "Bitiş zamanı başlangıç zamanından sonra olmalıdır.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(availability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await PopulateVeterinariansSelectListAsync(availability.VeterinarianId);
            return View(availability);
        }

        // GET: VeterinarianAvailabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var availability = await _context.VeterinarianAvailabilities.FindAsync(id);
            if (availability == null) return NotFound();

            await PopulateVeterinariansSelectListAsync(availability.VeterinarianId);
            return View(availability);
        }

        // POST: VeterinarianAvailabilities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VeterinarianAvailability availability)
        {
            if (id != availability.Id) return NotFound();

            if (availability.EndTime <= availability.StartTime)
            {
                ModelState.AddModelError(nameof(availability.EndTime), "Bitiş zamanı başlangıç zamanından sonra olmalıdır.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.VeterinarianAvailabilities.AnyAsync(e => e.Id == availability.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            await PopulateVeterinariansSelectListAsync(availability.VeterinarianId);
            return View(availability);
        }

        // GET: VeterinarianAvailabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var availability = await _context.VeterinarianAvailabilities
                .Include(a => a.Veterinarian)
                    .ThenInclude(v => v!.Clinic)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (availability == null) return NotFound();

            return View(availability);
        }

        // POST: VeterinarianAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availability = await _context.VeterinarianAvailabilities.FindAsync(id);
            if (availability != null)
            {
                _context.VeterinarianAvailabilities.Remove(availability);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateVeterinariansSelectListAsync(int? selectedVeterinarianId = null)
        {
            var veterinarians = await _context.Veterinarians
                .Include(v => v.Clinic)
                .OrderBy(v => v.FullName)
                .Select(v => new
                {
                    v.Id,
                    DisplayText = v.Clinic != null ? $"{v.FullName} ({v.Clinic.Name})" : v.FullName
                })
                .ToListAsync();

            ViewData["VeterinarianId"] = new SelectList(veterinarians, "Id", "DisplayText", selectedVeterinarianId);
        }
    }
}   
