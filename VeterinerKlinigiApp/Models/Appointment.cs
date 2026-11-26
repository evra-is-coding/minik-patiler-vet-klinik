using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigiApp.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required]
    public int PetId { get; set; }
    public Pet? Pet { get; set; }

    [Required]
    public int VeterinarianId { get; set; }
    public Veterinarian? Veterinarian { get; set; }

    [Required]
    public int ClinicServiceId { get; set; }
    public ClinicService? ClinicService { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    // Bekliyor, Onaylandı, Reddedildi, Tamamlandı
    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Bekliyor";

    [StringLength(1000)]
    public string? ExaminationNote { get; set; }
}