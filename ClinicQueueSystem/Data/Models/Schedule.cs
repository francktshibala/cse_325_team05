namespace ClinicQueueSystem.Data.Models;

public class Schedule
{
    public int Id { get; set; }
    public int ProviderId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime? EffectiveDate { get; set; }
    public DateTime? ExpirationDate { get; set; }

    // Navigation properties
    public Provider Provider { get; set; } = null!;
}
