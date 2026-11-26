using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigiApp.Data;
using VeterinerKlinigiApp.Models;

namespace VeterinerKlinigiApp.Controllers
{
    public class ClinicServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClinicServices
        public async Task<IActionResult> Index()
        {
            var services = await _context.ClinicServices
                .Include(s => s.Clinic)
                .ToListAsync();
            return View(services);
        }

        // GET: ClinicServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var service = await _context.ClinicServices
                .Include(s => s.Clinic)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (service == null) return NotFound();
            return View(service);
        }

        // GET: ClinicServices/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ClinicId"] = new SelectList(await _context.Clinics.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: ClinicServices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClinicService clinicService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clinicService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClinicId"] = new SelectList(await _context.Clinics.ToListAsync(), "Id", "Name", clinicService.ClinicId);
            return View(clinicService);
        }

        // GET: ClinicServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var clinicService = await _context.ClinicServices.FindAsync(id);
            if (clinicService == null) return NotFound();

            ViewData["ClinicId"] = new SelectList(await _context.Clinics.ToListAsync(), "Id", "Name", clinicService.ClinicId);
            return View(clinicService);
        }

        // POST: ClinicServices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClinicService clinicService)
        {
            if (id != clinicService.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinicService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.ClinicServices.AnyAsync(e => e.Id == clinicService.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClinicId"] = new SelectList(await _context.Clinics.ToListAsync(), "Id", "Name", clinicService.ClinicId);
            return View(clinicService);
        }

        // GET: ClinicServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var service = await _context.ClinicServices
                .Include(s => s.Clinic)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (service == null) return NotFound();
            return View(service);
        }

        // POST: ClinicServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.ClinicServices.FindAsync(id);
            if (service != null)
            {
                _context.ClinicServices.Remove(service);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
