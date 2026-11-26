using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigiApp.Models;

public class VaccineRecord
{
    public int Id { get; set; }

    [Required]
    public int PetId { get; set; }
    public Pet? Pet { get; set; }

    [Required(ErrorMessage = "Aşı adı zorunludur.")]
    [StringLength(100)]
    public string VaccineName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime AdministeredDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? NextDueDate { get; set; }
} 