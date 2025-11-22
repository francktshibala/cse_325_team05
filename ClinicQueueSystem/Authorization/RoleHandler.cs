using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ClinicQueueSystem.Authorization;

/// <summary>
/// Authorization handler that checks if a user has one of the required roles
/// </summary>
public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly UserManager<Data.Models.ApplicationUser> _userManager;

    public RoleHandler(UserManager<Data.Models.ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleRequirement requirement)
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

        var userRoles = await _userManager.GetRolesAsync(user);

        if (userRoles.Any(role => requirement.AllowedRoles.Contains(role)))
        {
            context.Succeed(requirement);
        }
    }
}

