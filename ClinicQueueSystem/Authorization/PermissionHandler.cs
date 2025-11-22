using ClinicQueueSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ClinicQueueSystem.Authorization;

/// <summary>
/// Authorization handler that checks if a user has a specific permission based on their role
/// </summary>
public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly UserManager<Data.Models.ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public PermissionHandler(
        UserManager<Data.Models.ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User == null || context.User.Identity?.IsAuthenticated != true)
        {
            return;
        }

        var user = await _userManager.GetUserAsync(context.User);
        if (user == null)
        {
            return;
        }

        // Get all roles for the user
        var userRoles = await _userManager.GetRolesAsync(user);

        // Check if user has the required permission through any of their roles
        foreach (var roleName in userRoles)
        {
            var rolePermissions = Permissions.GetPermissionsForRole(roleName);
            if (rolePermissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}

