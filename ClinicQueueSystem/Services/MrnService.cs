using ClinicQueueSystem.Data;
using ClinicQueueSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicQueueSystem.Services;

public interface IMrnService
{
    Task<string> GenerateUniqueMrnAsync(CancellationToken cancellationToken = default);
}

public class MrnService : IMrnService
{
    private readonly ApplicationDbContext _dbContext;
    private static readonly Random _random = new Random();

    public MrnService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GenerateUniqueMrnAsync(CancellationToken cancellationToken = default)
    {
        // Format: MRN-YYYYMMDD-XXXXX (random 5 digits). Retry to avoid collisions.
        for (var attempt = 0; attempt < 10; attempt++)
        {
            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var randomPart = _random.Next(0, 100000).ToString("D5");
            var mrn = $"MRN-{datePart}-{randomPart}";

            var exists = await _dbContext.Patients.AnyAsync(p => p.MedicalRecordNumber == mrn, cancellationToken);
            if (!exists)
            {
                return mrn;
            }
        }

        // Fallback with GUID tail (extremely unlikely to hit)
        var fallback = $"MRN-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
        return fallback;
    }
}


