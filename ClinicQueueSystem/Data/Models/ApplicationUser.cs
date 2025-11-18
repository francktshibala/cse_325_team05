using Microsoft.AspNetCore.Identity;

namespace ClinicQueueSystem.Data.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Patient? Patient { get; set; }
    public Provider? Provider { get; set; }
}
