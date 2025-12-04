using ClinicQueueSystem.Authorization;
using ClinicQueueSystem.Components;
using ClinicQueueSystem.Data;
using ClinicQueueSystem.Data.Models;
using ClinicQueueSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ClinicQueueSystem.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add controllers for authentication endpoints
builder.Services.AddControllers();

// Add HttpContextAccessor for auth state
builder.Services.AddHttpContextAccessor();

// Add cascading authentication state for Blazor components
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, Microsoft.AspNetCore.Components.Server.ServerAuthenticationStateProvider>();

// Add database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=clinicqueue.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure application cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
    options.ExpireTimeSpan = TimeSpan.FromHours(2);
    options.SlidingExpiration = true;
});

// Add authorization services
builder.Services.AddAuthorization(options =>
{
    // Role-based policies
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("DoctorOnly", policy => policy.RequireRole("Doctor"));
    options.AddPolicy("NurseOnly", policy => policy.RequireRole("Nurse"));
    options.AddPolicy("HealthRecordsOnly", policy => policy.RequireRole("Health Records"));
    options.AddPolicy("PatientOnly", policy => policy.RequireRole("Patient"));
    options.AddPolicy("StaffOnly", policy => policy.RequireRole("Admin", "Doctor", "Nurse", "Health Records"));
    options.AddPolicy("ProviderOnly", policy => policy.RequireRole("Doctor", "Nurse"));

    // Permission-based policies for common operations
    options.AddPolicy("CanManageAppointments", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.Appointments_Manage)));
    options.AddPolicy("CanViewPatients", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.Patients_View)));
    options.AddPolicy("CanManageQueue", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.Queue_Manage)));
    options.AddPolicy("CanManageUsers", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.Users_View)));
    options.AddPolicy("CanViewReports", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.Reports_View)));
});

// Register authorization handlers
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RoleHandler>();

// Register custom authorization service
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

// SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Initialize database with roles and admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DbInitializer.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Hubs
app.MapHub<QueueHub>("/hubs/queue");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
