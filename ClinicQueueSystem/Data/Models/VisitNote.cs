namespace ClinicQueueSystem.Data.Models;

public class VisitNote
{
    public int Id { get; set; }
    public int VisitId { get; set; }
    public int ProviderId { get; set; }
    public string NoteText { get; set; } = string.Empty;
    public string NoteType { get; set; } = "General"; // General, Diagnosis, Treatment, Prescription
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Visit Visit { get; set; } = null!;
    public Provider Provider { get; set; } = null!;
}
