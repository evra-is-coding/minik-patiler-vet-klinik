using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VeterinerKlinigiApp.Models;

public class Pet
{
    public int Id { get; set; }

    [Required]
    public string OwnerId { get; set; } = string.Empty;
    public IdentityUser? Owner { get; set; }

    [Required(ErrorMessage = "Evcil hayvan adı zorunludur.")]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tür bilgisi zorunludur.")]
    [StringLength(50)]
    public string Species { get; set; } = string.Empty;

    [StringLength(80)]
    public string? Breed { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? Weight { get; set; }

    [StringLength(300)]
    public string? PhotoUrl { get; set; }

    // Navigation Properties
    public ICollection<VaccineRecord> VaccineRecords { get; set; } = new List<VaccineRecord>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
} 
