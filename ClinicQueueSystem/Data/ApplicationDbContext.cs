using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClinicQueueSystem.Data.Models;

namespace ClinicQueueSystem.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<QueueTicket> QueueTickets { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<VisitNote> VisitNotes { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Patient - User relationship
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.User)
            .WithOne(u => u.Patient)
            .HasForeignKey<Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Provider - User relationship
        modelBuilder.Entity<Provider>()
            .HasOne(p => p.User)
            .WithOne(u => u.Provider)
            .HasForeignKey<Provider>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Appointment relationships
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Provider)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure QueueTicket relationships
        modelBuilder.Entity<QueueTicket>()
            .HasOne(q => q.Patient)
            .WithMany(p => p.QueueTickets)
            .HasForeignKey(q => q.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QueueTicket>()
            .HasOne(q => q.Appointment)
            .WithMany()
            .HasForeignKey(q => q.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure Visit relationships
        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Patient)
            .WithMany(p => p.Visits)
            .HasForeignKey(v => v.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Appointment)
            .WithOne(a => a.Visit)
            .HasForeignKey<Visit>(v => v.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure VisitNote relationships
        modelBuilder.Entity<VisitNote>()
            .HasOne(vn => vn.Visit)
            .WithMany(v => v.Notes)
            .HasForeignKey(vn => vn.VisitId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VisitNote>()
            .HasOne(vn => vn.Provider)
            .WithMany(p => p.VisitNotes)
            .HasForeignKey(vn => vn.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Schedule relationship
        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.Provider)
            .WithMany(p => p.Schedules)
            .HasForeignKey(s => s.ProviderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Add indexes for better query performance
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.MedicalRecordNumber)
            .IsUnique();

        modelBuilder.Entity<Appointment>()
            .HasIndex(a => new { a.ProviderId, a.ScheduledDateTime });

        modelBuilder.Entity<QueueTicket>()
            .HasIndex(q => new { q.Status, q.CheckInTime });

        modelBuilder.Entity<Schedule>()
            .HasIndex(s => new { s.ProviderId, s.DayOfWeek });
    }
}
