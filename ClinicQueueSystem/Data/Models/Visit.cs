namespace ClinicQueueSystem.Data.Models;

public class Visit
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int AppointmentId { get; set; }
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime? EndTime { get; set; }
    public string Status { get; set; } = "InProgress"; // InProgress, Completed
    public string? ChiefComplaint { get; set; }
    public string? Diagnosis { get; set; }
    public string? TreatmentPlan { get; set; }

    // Navigation properties
    public Patient Patient { get; set; } = null!;
    public Appointment Appointment { get; set; } = null!;
    public ICollection<VisitNote> Notes { get; set; } = new List<VisitNote>();
}
