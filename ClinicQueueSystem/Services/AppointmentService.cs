using ClinicQueueSystem.Data;
using ClinicQueueSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicQueueSystem.Services;

public interface IAppointmentService
{
    Task<IReadOnlyList<DateTime>> GetAvailableSlotsAsync(int providerId, DateTime date, int slotMinutes, CancellationToken ct = default);
    Task<bool> IsSlotAvailableAsync(int providerId, DateTime start, int durationMinutes, CancellationToken ct = default);
}

public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _db;

    public AppointmentService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<DateTime>> GetAvailableSlotsAsync(int providerId, DateTime date, int slotMinutes, CancellationToken ct = default)
    {
        if (slotMinutes <= 0) slotMinutes = 30;
        date = date.Date;

        // Determine schedules for the day:
        // If any dated exceptions exist for the date, they override defaults; otherwise use defaults only.
        var dayOfWeek = date.DayOfWeek;
        var exceptionSchedules = await _db.Schedules
            .Where(s => s.ProviderId == providerId
                        && s.DayOfWeek == dayOfWeek
                        && s.EffectiveDate.HasValue
                        && s.EffectiveDate.Value.Date <= date
                        && (!s.ExpirationDate.HasValue || s.ExpirationDate.Value.Date >= date))
            .ToListAsync(ct);

        List<Schedule> schedules;
        if (exceptionSchedules.Any())
        {
            schedules = exceptionSchedules.Where(s => s.IsAvailable).ToList();
        }
        else
        {
            schedules = await _db.Schedules
                .Where(s => s.ProviderId == providerId
                            && s.DayOfWeek == dayOfWeek
                            && s.IsAvailable
                            && !s.EffectiveDate.HasValue
                            && !s.ExpirationDate.HasValue)
                .ToListAsync(ct);
        }

        if (schedules.Count == 0)
        {
            return Array.Empty<DateTime>();
        }

        // Existing non-cancelled appointments for that provider on that date
        var dayStart = date;
        var dayEnd = date.AddDays(1);
        var existing = await _db.Appointments
            .Where(a => a.ProviderId == providerId
                        && a.Status != "Cancelled"
                        && a.ScheduledDateTime >= dayStart
                        && a.ScheduledDateTime < dayEnd)
            .Select(a => new
            {
                Start = a.ScheduledDateTime,
                End = a.ScheduledDateTime.AddMinutes(a.DurationMinutes)
            })
            .OrderBy(x => x.Start)
            .ToListAsync(ct);

        var existingIntervals = existing.Select(x => (start: x.Start, end: x.End)).ToList();

        var slots = new List<DateTime>();
        foreach (var sch in schedules)
        {
            var intervalStart = date.Add(sch.StartTime);
            var intervalEnd = date.Add(sch.EndTime);

            var cursor = intervalStart;
            while (cursor.AddMinutes(slotMinutes) <= intervalEnd)
            {
                var candidateStart = cursor;
                var candidateEnd = cursor.AddMinutes(slotMinutes);

                // Ensure no overlap with existing appointments
                var overlaps = existingIntervals.Any(e => !(candidateEnd <= e.start || candidateStart >= e.end));
                if (!overlaps && candidateStart >= DateTime.Now) // do not offer past slots
                {
                    slots.Add(candidateStart);
                }

                cursor = cursor.AddMinutes(slotMinutes);
            }
        }

        return slots
            .OrderBy(s => s)
            .ToList();
    }

    public async Task<bool> IsSlotAvailableAsync(int providerId, DateTime start, int durationMinutes, CancellationToken ct = default)
    {
        var date = start.Date;
        var end = start.AddMinutes(durationMinutes <= 0 ? 30 : durationMinutes);

        // Check provider has availability schedule covering this time.
        // Apply exception precedence: if any exceptions exist for the date, only those apply; otherwise defaults.
        var dayOfWeek = date.DayOfWeek;
        var exceptionSchedules = await _db.Schedules
            .Where(s => s.ProviderId == providerId
                        && s.DayOfWeek == dayOfWeek
                        && s.EffectiveDate.HasValue
                        && s.EffectiveDate.Value.Date <= date
                        && (!s.ExpirationDate.HasValue || s.ExpirationDate.Value.Date >= date))
            .ToListAsync(ct);
        List<Schedule> schedules;
        if (exceptionSchedules.Any())
        {
            schedules = exceptionSchedules.Where(s => s.IsAvailable).ToList();
        }
        else
        {
            schedules = await _db.Schedules
                .Where(s => s.ProviderId == providerId
                            && s.DayOfWeek == dayOfWeek
                            && s.IsAvailable
                            && !s.EffectiveDate.HasValue
                            && !s.ExpirationDate.HasValue)
                .ToListAsync(ct);
        }

        var withinSchedule = schedules.Any(s =>
        {
            var sStart = date.Add(s.StartTime);
            var sEnd = date.Add(s.EndTime);
            return start >= sStart && end <= sEnd;
        });
        if (!withinSchedule)
        {
            return false;
        }

        // Check no overlapping appointments
        var overlap = await _db.Appointments.AnyAsync(a =>
            a.ProviderId == providerId
            && a.Status != "Cancelled"
            && a.ScheduledDateTime < end
            && a.ScheduledDateTime.AddMinutes(a.DurationMinutes) > start, ct);

        return !overlap;
    }
}


