using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigiApp.Models;

public class Veterinarian
{
    public int Id { get; set; }

    [Required]
    public int ClinicId { get; set; }
    public Clinic? Clinic { get; set; }

    // Opsiyonel: Hekimin sisteme giriş yapabilmesi için Identity hesabı bağlantısı
    public string? UserId { get; set; }
    public IdentityUser? User { get; set; }

    [Required(ErrorMessage = "Hekim adı soyadı zorunludur.")]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Uzmanlık alanı zorunludur.")]
    [StringLength(100)]
    public string Specialty { get; set; } = string.Empty;

    // Navigation Properties
    public ICollection<VeterinarianService> VeterinarianServices { get; set; } = new List<VeterinarianService>();
    public ICollection<VeterinarianAvailability> Availabilities { get; set; } = new List<VeterinarianAvailability>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
