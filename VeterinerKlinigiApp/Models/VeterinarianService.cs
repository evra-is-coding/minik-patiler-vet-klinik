namespace VeterinerKlinigiApp.Models;

public class VeterinarianService
{
    public int VeterinarianId { get; set; }
    public Veterinarian? Veterinarian { get; set; }

    public int ClinicServiceId { get; set; }
    public ClinicService? ClinicService { get; set; }
}   