using ClinicQueueSystem.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace ClinicQueueSystem.Data;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // Define roles
        string[] roleNames = { "Admin", "Doctor", "Nurse", "Patient", "Health Records" };

        // Create roles if they don't exist
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create default admin user if it doesn't exist
        var adminEmail = "admin@clinicqueue.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "System",
                LastName = "Administrator",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(admin, "Admin123!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
<<<<<<< HEAD

        // Create default doctor users and providers with schedules
        await SeedDoctorsAndSchedules(userManager, dbContext);

        // Create sample patient users
        await SeedPatients(userManager, dbContext);
    }

    private static async Task SeedDoctorsAndSchedules(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
    {
        // Doctor 1: Dr. Sarah Johnson (General Medicine)
        var doctor1Email = "dr.johnson@clinicqueue.com";
        var doctor1User = await userManager.FindByEmailAsync(doctor1Email);

        if (doctor1User == null)
        {
            doctor1User = new ApplicationUser
            {
                UserName = doctor1Email,
                Email = doctor1Email,
                FirstName = "Sarah",
                LastName = "Johnson",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(doctor1User, "Doctor123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(doctor1User, "Doctor");

                var provider1 = new Provider
                {
                    UserId = doctor1User.Id,
                    Specialty = "General Medicine",
                    LicenseNumber = "MD-12345",
                    IsActive = true
                };
                dbContext.Providers.Add(provider1);
                await dbContext.SaveChangesAsync();

                for (int day = 1; day <= 5; day++) // Monday to Friday
                {
                    dbContext.Schedules.Add(new Schedule
                    {
                        ProviderId = provider1.Id,
                        DayOfWeek = (DayOfWeek)day,
                        StartTime = new TimeSpan(9, 0, 0),
                        EndTime = new TimeSpan(17, 0, 0),
                        IsAvailable = true,
                        EffectiveDate = DateTime.Today.AddDays(-30),
                        ExpirationDate = DateTime.Today.AddYears(1)
                    });
                }
                await dbContext.SaveChangesAsync();
            }
        }

        // Doctor 2: Dr. Michael Chen (Pediatrics)
        var doctor2Email = "dr.chen@clinicqueue.com";
        var doctor2User = await userManager.FindByEmailAsync(doctor2Email);

        if (doctor2User == null)
        {
            doctor2User = new ApplicationUser
            {
                UserName = doctor2Email,
                Email = doctor2Email,
                FirstName = "Michael",
                LastName = "Chen",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(doctor2User, "Doctor123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(doctor2User, "Doctor");

                var provider2 = new Provider
                {
                    UserId = doctor2User.Id,
                    Specialty = "Pediatrics",
                    LicenseNumber = "MD-67890",
                    IsActive = true
                };
                dbContext.Providers.Add(provider2);
                await dbContext.SaveChangesAsync();

                for (int day = 1; day <= 4; day++) // Monday to Thursday
                {
                    dbContext.Schedules.Add(new Schedule
                    {
                        ProviderId = provider2.Id,
                        DayOfWeek = (DayOfWeek)day,
                        StartTime = new TimeSpan(8, 0, 0),
                        EndTime = new TimeSpan(16, 0, 0),
                        IsAvailable = true,
                        EffectiveDate = DateTime.Today.AddDays(-30),
                        ExpirationDate = DateTime.Today.AddYears(1)
                    });
                }
                await dbContext.SaveChangesAsync();
            }
        }

        // Doctor 3: Dr. Emily Rodriguez (Cardiology)
        var doctor3Email = "dr.rodriguez@clinicqueue.com";
        var doctor3User = await userManager.FindByEmailAsync(doctor3Email);

        if (doctor3User == null)
        {
            doctor3User = new ApplicationUser
            {
                UserName = doctor3Email,
                Email = doctor3Email,
                FirstName = "Emily",
                LastName = "Rodriguez",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(doctor3User, "Doctor123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(doctor3User, "Doctor");

                var provider3 = new Provider
                {
                    UserId = doctor3User.Id,
                    Specialty = "Cardiology",
                    LicenseNumber = "MD-54321",
                    IsActive = true
                };
                dbContext.Providers.Add(provider3);
                await dbContext.SaveChangesAsync();

                for (int day = 2; day <= 6; day++) // Tuesday to Saturday
                {
                    dbContext.Schedules.Add(new Schedule
                    {
                        ProviderId = provider3.Id,
                        DayOfWeek = (DayOfWeek)day,
                        StartTime = new TimeSpan(10, 0, 0),
                        EndTime = new TimeSpan(18, 0, 0),
                        IsAvailable = true,
                        EffectiveDate = DateTime.Today.AddDays(-30),
                        ExpirationDate = DateTime.Today.AddYears(1)
                    });
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private static async Task SeedPatients(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
    {
        // Patient 1: John Doe
        var patient1Email = "john.doe@example.com";
        var patient1User = await userManager.FindByEmailAsync(patient1Email);

        if (patient1User == null)
        {
            patient1User = new ApplicationUser
            {
                UserName = patient1Email,
                Email = patient1Email,
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(patient1User, "Patient123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(patient1User, "Patient");

                var patient1 = new Patient
                {
                    UserId = patient1User.Id,
                    MedicalRecordNumber = "MRN-001",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    Gender = "Male",
                    PhoneNumber = "(555) 123-4567",
                    Address = "123 Main St, Anytown, USA",
                    EmergencyContact = "Jane Doe",
                    EmergencyPhone = "(555) 987-6543"
                };
                dbContext.Patients.Add(patient1);
                await dbContext.SaveChangesAsync();
            }
        }

        // Patient 2: Mary Smith
        var patient2Email = "mary.smith@example.com";
        var patient2User = await userManager.FindByEmailAsync(patient2Email);

        if (patient2User == null)
        {
            patient2User = new ApplicationUser
            {
                UserName = patient2Email,
                Email = patient2Email,
                FirstName = "Mary",
                LastName = "Smith",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(patient2User, "Patient123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(patient2User, "Patient");

                var patient2 = new Patient
                {
                    UserId = patient2User.Id,
                    MedicalRecordNumber = "MRN-002",
                    DateOfBirth = new DateTime(1990, 8, 22),
                    Gender = "Female",
                    PhoneNumber = "(555) 234-5678",
                    Address = "456 Oak Ave, Springfield, USA",
                    EmergencyContact = "Robert Smith",
                    EmergencyPhone = "(555) 876-5432"
                };
                dbContext.Patients.Add(patient2);
                await dbContext.SaveChangesAsync();
            }
        }
=======
>>>>>>> parent of 413e72e (feat: provider/schedule UI + availability/booking fixes)
    }
}
