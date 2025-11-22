using Microsoft.AspNetCore.Authorization;

namespace ClinicQueueSystem.Authorization;

/// <summary>
/// Authorization requirement for role-based access control
/// </summary>
public class RoleRequirement : IAuthorizationRequirement
{
    public string[] AllowedRoles { get; }

    public RoleRequirement(params string[] allowedRoles)
    {
        AllowedRoles = allowedRoles;
    }
}

