namespace ClinicQueueSystem.Data.Models;

public class Provider
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string ProviderType { get; set; } = string.Empty; // Doctor, Nurse
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ApplicationUser User { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    public ICollection<VisitNote> VisitNotes { get; set; } = new List<VisitNote>();
}
