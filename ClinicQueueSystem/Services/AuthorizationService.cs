using ClinicQueueSystem.Authorization;
using ClinicQueueSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ClinicQueueSystem.Services;

/// <summary>
/// Service for checking user permissions and roles
/// </summary>
public class AuthorizationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationService(
        UserManager<ApplicationUser> userManager,
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Checks if the current user has a specific permission
    /// </summary>
    public async Task<bool> HasPermissionAsync(string permission)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || user.Identity?.IsAuthenticated != true)
        {
            return false;
        }

        var requirement = new PermissionRequirement(permission);
        var result = await _authorizationService.AuthorizeAsync(user, null, requirement);
        return result.Succeeded;
    }

    /// <summary>
    /// Checks if the current user has one of the specified roles
    /// </summary>
    public async Task<bool> HasRoleAsync(params string[] roles)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || user.Identity?.IsAuthenticated != true)
        {
            return false;
        }

        var requirement = new RoleRequirement(roles);
        var result = await _authorizationService.AuthorizeAsync(user, null, requirement);
        return result.Succeeded;
    }

    /// <summary>
    /// Gets all permissions for the current user based on their roles
    /// </summary>
    public async Task<string[]> GetUserPermissionsAsync()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || user.Identity?.IsAuthenticated != true)
        {
            return Array.Empty<string>();
        }

        var applicationUser = await _userManager.GetUserAsync(user);
        if (applicationUser == null)
        {
            return Array.Empty<string>();
        }

        var userRoles = await _userManager.GetRolesAsync(applicationUser);
        var allPermissions = new HashSet<string>();

        foreach (var role in userRoles)
        {
            var rolePermissions = Permissions.GetPermissionsForRole(role);
            foreach (var permission in rolePermissions)
            {
                allPermissions.Add(permission);
            }
        }

        return allPermissions.ToArray();
    }

    /// <summary>
    /// Gets all roles for the current user
    /// </summary>
    public async Task<string[]> GetUserRolesAsync()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || user.Identity?.IsAuthenticated != true)
        {
            return Array.Empty<string>();
        }

        var applicationUser = await _userManager.GetUserAsync(user);
        if (applicationUser == null)
        {
            return Array.Empty<string>();
        }

        var roles = await _userManager.GetRolesAsync(applicationUser);
        return roles.ToArray();
    }

    /// <summary>
    /// Checks if the current user is an admin
    /// </summary>
    public async Task<bool> IsAdminAsync()
    {
        return await HasRoleAsync("Admin");
    }

    /// <summary>
    /// Checks if the current user is a doctor
    /// </summary>
    public async Task<bool> IsDoctorAsync()
    {
        return await HasRoleAsync("Doctor");
    }

    /// <summary>
    /// Checks if the current user is a nurse
    /// </summary>
    public async Task<bool> IsNurseAsync()
    {
        return await HasRoleAsync("Nurse");
    }

    /// <summary>
    /// Checks if the current user is a health records staff
    /// </summary>
    public async Task<bool> IsHealthRecordsAsync()
    {
        return await HasRoleAsync("Health Records");
    }

    /// <summary>
    /// Checks if the current user is a patient
    /// </summary>
    public async Task<bool> IsPatientAsync()
    {
        return await HasRoleAsync("Patient");
    }
}

