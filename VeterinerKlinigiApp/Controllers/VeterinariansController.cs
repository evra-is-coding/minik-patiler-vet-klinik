using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigiApp.Data;
using VeterinerKlinigiApp.Models;

namespace VeterinerKlinigiApp.Controllers
{
    public class VeterinariansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeterinariansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Veterinarians
        public async Task<IActionResult> Index()
        {
            var veterinarians = await _context.Veterinarians
                .Include(v => v.Clinic)
                .Include(v => v.VeterinarianServices)
                    .ThenInclude(vs => vs.ClinicService)
                .ToListAsync();

            return View(veterinarians);
        }

        // GET: Veterinarians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var veterinarian = await _context.Veterinarians
                .Include(v => v.Clinic)
                .Include(v => v.VeterinarianServices)
                    .ThenInclude(vs => vs.ClinicService)
                .Include(v => v.Availabilities)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (veterinarian == null) return NotFound();

            return View(veterinarian);
        }

        // GET: Veterinarians/Create
        public async Task<IActionResult> Create()
        {
            await PopulateFormSelectListsAsync();
            return View();
        }

        // POST: Veterinarians/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Veterinarian veterinarian, int[] selectedServiceIds)
        {
            if (ModelState.IsValid)
            {
                if (selectedServiceIds != null && selectedServiceIds.Length > 0)
                {
                    foreach (var serviceId in selectedServiceIds.Distinct())
                    {
                        veterinarian.VeterinarianServices.Add(new VeterinarianService
                        {
                            ClinicServiceId = serviceId
                        });
                    }
                }

                _context.Add(veterinarian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await PopulateFormSelectListsAsync(veterinarian.ClinicId, selectedServiceIds);
            return View(veterinarian);
        }

        // GET: Veterinarians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var veterinarian = await _context.Veterinarians
                .Include(v => v.VeterinarianServices)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (veterinarian == null) return NotFound();

            var selectedIds = veterinarian.VeterinarianServices
                .Select(vs => vs.ClinicServiceId)
                .ToArray();

            await PopulateFormSelectListsAsync(veterinarian.ClinicId, selectedIds);
            return View(veterinarian);
        }

        // POST: Veterinarians/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Veterinarian veterinarian, int[] selectedServiceIds)
        {
            if (id != veterinarian.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingVet = await _context.Veterinarians
                        .Include(v => v.VeterinarianServices)
                        .FirstOrDefaultAsync(v => v.Id == id);

                    if (existingVet == null) return NotFound();

                    existingVet.ClinicId = veterinarian.ClinicId;
                    existingVet.UserId = veterinarian.UserId;
                    existingVet.FullName = veterinarian.FullName;
                    existingVet.Specialty = veterinarian.Specialty;

                    // Mevcut hizmet eşleştirmelerini güncelleyelim
                    existingVet.VeterinarianServices.Clear();
                    if (selectedServiceIds != null && selectedServiceIds.Length > 0)
                    {
                        foreach (var serviceId in selectedServiceIds.Distinct())
                        {
                            existingVet.VeterinarianServices.Add(new VeterinarianService
                            {
                                VeterinarianId = existingVet.Id,
                                ClinicServiceId = serviceId
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Veterinarians.AnyAsync(e => e.Id == veterinarian.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            await PopulateFormSelectListsAsync(veterinarian.ClinicId, selectedServiceIds);
            return View(veterinarian);
        }

        // GET: Veterinarians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var veterinarian = await _context.Veterinarians
                .Include(v => v.Clinic)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (veterinarian == null) return NotFound();

            return View(veterinarian);
        }

        // POST: Veterinarians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinarian = await _context.Veterinarians
                .Include(v => v.VeterinarianServices)
                .Include(v => v.Availabilities)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (veterinarian != null)
            {
                if (veterinarian.VeterinarianServices.Any())
                {
                    _context.VeterinarianServices.RemoveRange(veterinarian.VeterinarianServices);
                }

                if (veterinarian.Availabilities.Any())
                {
                    _context.VeterinarianAvailabilities.RemoveRange(veterinarian.Availabilities);
                }

                _context.Veterinarians.Remove(veterinarian);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateFormSelectListsAsync(int? selectedClinicId = null, int[]? selectedServiceIds = null)
        {
            var clinics = await _context.Clinics.OrderBy(c => c.Name).ToListAsync();
            ViewData["ClinicId"] = new SelectList(clinics, "Id", "Name", selectedClinicId);

            var services = await _context.ClinicServices
                .Include(s => s.Clinic)
                .OrderBy(s => s.Name)
                .ToListAsync();

            ViewBag.AllServices = services;
            ViewBag.SelectedServiceIds = selectedServiceIds ?? Array.Empty<int>();
        }
    }
}