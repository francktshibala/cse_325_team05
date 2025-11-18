namespace ClinicQueueSystem.Data.Models;

public class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int ProviderId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public int DurationMinutes { get; set; } = 30;
    public string AppointmentType { get; set; } = string.Empty; // Checkup, Consultation, Follow-up
    public string Status { get; set; } = "Scheduled"; // Scheduled, Confirmed, Cancelled, Completed
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CancelledAt { get; set; }

    // Navigation properties
    public Patient Patient { get; set; } = null!;
    public Provider Provider { get; set; } = null!;
    public Visit? Visit { get; set; }
}
