using VeterinerKlinigiApp.Models;

namespace VeterinerKlinigiApp.Services.Interfaces;

public interface IPetManagementService
{
    Task<List<Pet>> GetPetsByOwnerIdAsync(string ownerId);
    Task<Pet?> GetPetByIdAsync(int id);
    Task AddPetAsync(Pet pet);
    Task UpdatePetAsync(Pet pet);
    Task DeletePetAsync(int id);

    Task<List<VaccineRecord>> GetVaccinesByPetIdAsync(int petId);
    Task AddVaccineRecordAsync(VaccineRecord record);
    Task DeleteVaccineRecordAsync(int recordId);
}   