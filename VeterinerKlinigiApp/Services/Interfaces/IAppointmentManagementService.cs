using VeterinerKlinigiApp.Models;

namespace VeterinerKlinigiApp.Services.Interfaces;

public interface IAppointmentManagementService
{
    Task<List<Appointment>> GetAllAppointmentsAsync();
    Task<List<Appointment>> GetAppointmentsByOwnerIdAsync(string ownerId);
    Task<List<Appointment>> GetAppointmentsByVeterinarianIdAsync(int veterinarianId);
    Task<Appointment?> GetAppointmentByIdAsync(int id);

    Task<(bool Success, string Message)> CreateAppointmentAsync(Appointment appointment);
    Task UpdateStatusAsync(int appointmentId, string newStatus);
    Task AddExaminationNoteAsync(int appointmentId, string note);
}  