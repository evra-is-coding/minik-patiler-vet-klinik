using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigiApp.Models;

public class VeterinarianAvailability
{
    public int Id { get; set; }

    [Required]
    public int VeterinarianId { get; set; }
    public Veterinarian? Veterinarian { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }
}   