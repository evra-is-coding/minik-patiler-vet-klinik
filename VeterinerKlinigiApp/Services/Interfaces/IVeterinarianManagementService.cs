using VeterinerKlinigiApp.Models;

namespace VeterinerKlinigiApp.Services.Interfaces;

public interface IVeterinarianManagementService
{
    Task<List<Veterinarian>> GetAllVeterinariansAsync();
    Task<Veterinarian?> GetVeterinarianByIdAsync(int id);
    Task AddVeterinarianAsync(Veterinarian veterinarian, List<int> selectedServiceIds);
    Task UpdateVeterinarianAsync(Veterinarian veterinarian, List<int> selectedServiceIds);
    Task DeleteVeterinarianAsync(int id);

    Task<List<VeterinarianAvailability>> GetAvailabilitiesByVetIdAsync(int veterinarianId);
    Task AddAvailabilityAsync(VeterinarianAvailability availability);
    Task DeleteAvailabilityAsync(int availabilityId);
}   