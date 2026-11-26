using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigiApp.Models;

namespace VeterinerKlinigiApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<ClinicService> ClinicServices { get; set; }
    public DbSet<Veterinarian> Veterinarians { get; set; }
    public DbSet<VeterinarianService> VeterinarianServices { get; set; }
    public DbSet<VeterinarianAvailability> VeterinarianAvailabilities { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<VaccineRecord> VaccineRecords { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // VeterinarianService çoka-çok ara tablosu için bileşik anahtar ve Restrict ilişkileri
        builder.Entity<VeterinarianService>()
            .HasKey(vs => new { vs.VeterinarianId, vs.ClinicServiceId });

        builder.Entity<VeterinarianService>()
            .HasOne(vs => vs.Veterinarian)
            .WithMany(v => v.VeterinarianServices)
            .HasForeignKey(vs => vs.VeterinarianId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VeterinarianService>()
            .HasOne(vs => vs.ClinicService)
            .WithMany(cs => cs.VeterinarianServices)
            .HasForeignKey(vs => vs.ClinicServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Appointments ilişkilerinde cascade çakışmasını önlemek için Restrict ayarı
        builder.Entity<Appointment>()
            .HasOne(a => a.Veterinarian)
            .WithMany(v => v.Appointments)
            .HasForeignKey(a => a.VeterinarianId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Appointment>()
            .HasOne(a => a.ClinicService)
            .WithMany(cs => cs.Appointments)
            .HasForeignKey(a => a.ClinicServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
