namespace ClinicQueueSystem.Data.Models;

public class QueueTicket
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int? AppointmentId { get; set; }
    public DateTime CheckInTime { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Waiting"; // Waiting, Called, InRoom, Completed
    public int Priority { get; set; } = 1; // 1=Normal, 2=Urgent, 3=Emergency
    public int EstimatedWaitMinutes { get; set; } = 15;
    public int QueuePosition { get; set; }
    public DateTime? CalledTime { get; set; }
    public DateTime? CompletedTime { get; set; }

    // Navigation properties
    public Patient Patient { get; set; } = null!;
    public Appointment? Appointment { get; set; }
}
