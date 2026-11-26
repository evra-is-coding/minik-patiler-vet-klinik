using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VeterinerKlinigiApp.Models;

public class ClinicService
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Hizmet adı zorunludur.")]
    [StringLength(100)]
    [Display(Name = "Hizmet Adı")]
    public string Name { get; set; } = string.Empty; // Örn: Muayene, Aşı, Kısırlaştırma

    [StringLength(500)]
    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Süre (dakika) zorunludur.")]
    [Range(5, 480, ErrorMessage = "Hizmet süresi 5 ile 480 dakika arasında olmalıdır.")]
    [Display(Name = "Süre (Dakika)")]
    public int DurationMinutes { get; set; }

    [Required(ErrorMessage = "Ücret zorunludur.")]
    [Range(0, 100000, ErrorMessage = "Ücret 0'dan küçük olamaz.")]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Ücret (₺)")]
    public decimal Price { get; set; }

    // klinik ile birlestirme 
    [Required(ErrorMessage = "Klinik seçimi zorunludur.")]
    [Display(Name = "Klinik")]
    public int ClinicId { get; set; }

    public Clinic? Clinic { get; set; }

    //  Bu hizmeti verebilen hekim eşleşmeleri
    public ICollection<VeterinarianService> VeterinarianServices { get; set; } = new List<VeterinarianService>();

    // Bu hizmet için alınan randevular
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
