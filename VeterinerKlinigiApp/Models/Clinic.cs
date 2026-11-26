using System.ComponentModel.DataAnnotations;


namespace VeterinerKlinigiApp.Models;

public class Clinic
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Klinik adı zorunludur.")]
    [StringLength(100, ErrorMessage = "Klinik adı en fazla 100 karakter olabilir.")]
    [Display(Name = "Klinik Adı")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Adres bilgisi zorunludur.")]
    [StringLength(250)]
    [Display(Name = "Adres")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon numarası zorunludur.")]
    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
    [Display(Name = "Telefon")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açılış saati zorunludur.")]
    [DataType(DataType.Time)]
    [Display(Name = "Açılış Saati")]
    public TimeSpan OpeningTime { get; set; }

    [Required(ErrorMessage = "Kapanış saati zorunludur.")]
    [DataType(DataType.Time)]
    [Display(Name = "Kapanış Saati")]
    public TimeSpan ClosingTime { get; set; }

    // Navigation property 
    public ICollection<ClinicService> Services { get; set; } = new List<ClinicService>();

    public ICollection<Veterinarian> Veterinarians { get; set; } = new List<Veterinarian>();
}  