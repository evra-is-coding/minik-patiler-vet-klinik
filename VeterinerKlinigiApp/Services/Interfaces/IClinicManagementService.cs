using VeterinerKlinigiApp.Models;

namespace VeterinerKlinigiApp.Services.Interfaces;

public interface IClinicManagementService
{
    Task<List<Clinic>> GetAllClinicsAsync();
    Task<Clinic?> GetClinicByIdAsync(int id);
    Task AddClinicAsync(Clinic clinic);
    Task UpdateClinicAsync(Clinic clinic);
    Task DeleteClinicAsync(int id);

    Task<List<ClinicService>> GetServicesByClinicIdAsync(int clinicId);
    Task<ClinicService?> GetServiceByIdAsync(int id);
    Task AddServiceAsync(ClinicService service);
    Task UpdateServiceAsync(ClinicService service);
    Task DeleteServiceAsync(int id);
}  