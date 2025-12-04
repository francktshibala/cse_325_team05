using ClinicQueueSystem.Data;
using ClinicQueueSystem.Data.Models;
using ClinicQueueSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClinicQueueSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;
    private readonly IMrnService _mrnService;

    public AccountController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext db,
        IMrnService mrnService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
        _mrnService = mrnService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password, [FromForm] bool rememberMe = false)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return Redirect("/login?error=Please provide email and password");
        }

        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return Redirect("/");
        }

        if (result.IsLockedOut)
        {
            return Redirect("/login?error=Account is locked. Please try again later.");
        }

        return Redirect("/login?error=Invalid email or password");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromForm] string firstName,
        [FromForm] string lastName,
        [FromForm] string email,
        [FromForm] string password,
        [FromForm] string confirmPassword,
        [FromForm] string role = "Patient")
    {
        if (password != confirmPassword)
        {
            return Redirect("/register?error=Passwords do not match");
        }

        // Validate role
        var validRoles = new[] { "Patient", "Nurse", "Doctor", "Health Records", "Admin" };
        if (!validRoles.Contains(role))
        {
            role = "Patient";
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            // Ensure role exists
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            // Add user to role
            await _userManager.AddToRoleAsync(user, role);

            // If registering as Patient, create a Patient entity automatically
            if (role == "Patient")
            {
                var mrn = await _mrnService.GenerateUniqueMrnAsync();
                var patient = new Patient
                {
                    UserId = user.Id,
                    MedicalRecordNumber = mrn,
                    CreatedAt = DateTime.UtcNow
                };
                _db.Patients.Add(patient);
                await _db.SaveChangesAsync();
            }

            // Sign in the user
            await _signInManager.SignInAsync(user, isPersistent: false);

            return Redirect("/");
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        return Redirect($"/register?error={Uri.EscapeDataString(errors)}");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> LogoutGet()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }
}
