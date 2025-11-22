# Authorization and Permissions Guide

This document provides detailed information about the role-based access control (RBAC) and permissions system in the Clinic Queue System.

## Overview

The system implements a two-tier authorization model:
1. **Role-Based Access Control (RBAC)** - Users are assigned roles
2. **Permission-Based Authorization** - Each role has specific permissions

## Roles

### Admin
Full system access including:
- All permissions
- User and role management
- System configuration
- Reporting and analytics

### Doctor
Medical provider with access to:
- Patient records and medical history
- Appointment scheduling and management
- Visit notes creation and editing
- Schedule management
- Basic reporting

### Nurse
Clinical staff with access to:
- Patient records (view and edit)
- Queue management
- Patient check-in
- Visit notes (create and edit)
- Appointment viewing

### Health Records
Receptionist/administrative staff with access to:
- Appointment booking and management
- Patient record creation and editing
- Queue management and check-in
- Schedule viewing

### Patient
Limited access for self-service:
- Own appointment viewing and booking
- Own record viewing
- Queue check-in

## Permissions

All permissions are defined in `ClinicQueueSystem.Data.Models.Permissions` class.

### Permission Categories

#### Appointment Permissions
- `Appointments.View` - View appointments
- `Appointments.Book` - Book new appointments
- `Appointments.Cancel` - Cancel appointments
- `Appointments.Manage` - Full appointment management (CRUD)

#### Patient Permissions
- `Patients.View` - View patient records
- `Patients.ViewOwn` - View own patient record
- `Patients.Edit` - Edit patient records
- `Patients.Create` - Create new patient records

#### Queue Permissions
- `Queue.View` - View queue status
- `Queue.Manage` - Manage queue (update status, prioritize)
- `Queue.CheckIn` - Check in to queue

#### Visit Notes Permissions
- `VisitNotes.View` - View visit notes
- `VisitNotes.Create` - Create visit notes
- `VisitNotes.Edit` - Edit visit notes

#### Schedule Permissions
- `Schedules.View` - View schedules
- `Schedules.Manage` - Manage schedules
- `Schedules.ViewOwn` - View own schedule

#### User Management Permissions
- `Users.View` - View user list
- `Users.Create` - Create new users
- `Users.Edit` - Edit users
- `Users.Delete` - Delete users
- `Users.ManageRoles` - Assign/remove roles

#### Reporting Permissions
- `Reports.View` - View reports
- `Reports.Export` - Export reports

## Usage Examples

### In Blazor Components

#### Check Permission
```razor
@page "/appointments"
@inject AuthorizationService AuthService
@inject NavigationManager Nav

<h1>Appointments</h1>

@if (await AuthService.HasPermissionAsync(Permissions.Appointments_Manage))
{
    <button class="btn btn-primary" @onclick="CreateAppointment">
        Create Appointment
    </button>
}

@code {
    private void CreateAppointment()
    {
        Nav.NavigateTo("/appointments/create");
    }
}
```

#### Check Role
```razor
@inject AuthorizationService AuthService

@if (await AuthService.IsAdminAsync())
{
    <AdminPanel />
}

@if (await AuthService.HasRoleAsync("Doctor", "Nurse"))
{
    <ProviderDashboard />
}
```

#### Get All User Permissions
```razor
@inject AuthorizationService AuthService

@code {
    private string[]? userPermissions;

    protected override async Task OnInitializedAsync()
    {
        userPermissions = await AuthService.GetUserPermissionsAsync();
    }
}
```

### In Controllers

#### Using Authorization Policies
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "CanManageAppointments")]
public class AppointmentsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAppointments()
    {
        // Only users with Appointments.Manage permission can access
        return Ok();
    }
}
```

#### Using Role-Based Policies
```csharp
[Authorize(Policy = "AdminOnly")]
[HttpPost("users")]
public IActionResult CreateUser([FromBody] CreateUserModel model)
{
    // Only admins can access
    return Ok();
}
```

#### Using Permission Requirements Directly
```csharp
[Authorize]
[HttpPost("patients")]
public async Task<IActionResult> CreatePatient([FromBody] PatientModel model)
{
    var authResult = await _authorizationService.AuthorizeAsync(
        User, 
        new PermissionRequirement(Permissions.Patients_Create));
    
    if (!authResult.Succeeded)
    {
        return Forbid();
    }
    
    // Create patient logic
    return Ok();
}
```

### In Razor Pages

#### Using Authorize Attribute
```razor
@page "/admin/users"
@attribute [Authorize(Policy = "AdminOnly")]

<h1>User Management</h1>
<!-- Only admins can see this page -->
```

#### Conditional Rendering
```razor
@page "/dashboard"
@inject AuthorizationService AuthService

<div class="dashboard">
    @if (await AuthService.HasPermissionAsync(Permissions.Appointments_View))
    {
        <AppointmentWidget />
    }
    
    @if (await AuthService.HasPermissionAsync(Permissions.Queue_View))
    {
        <QueueWidget />
    }
    
    @if (await AuthService.HasPermissionAsync(Permissions.Reports_View))
    {
        <ReportsWidget />
    }
</div>
```

## Authorization Service Methods

The `AuthorizationService` provides the following methods:

### Permission Checks
- `HasPermissionAsync(string permission)` - Check if user has a specific permission
- `GetUserPermissionsAsync()` - Get all permissions for the current user

### Role Checks
- `HasRoleAsync(params string[] roles)` - Check if user has one of the specified roles
- `GetUserRolesAsync()` - Get all roles for the current user
- `IsAdminAsync()` - Check if user is an admin
- `IsDoctorAsync()` - Check if user is a doctor
- `IsNurseAsync()` - Check if user is a nurse
- `IsHealthRecordsAsync()` - Check if user is health records staff
- `IsPatientAsync()` - Check if user is a patient

## Best Practices

1. **Use Permissions for Feature Access**
   - Prefer permission checks over role checks for feature access
   - This allows for more granular control and easier maintenance

2. **Use Roles for UI Layout**
   - Use role checks for determining UI layout and navigation
   - Example: Show different dashboards for different roles

3. **Defense in Depth**
   - Always check permissions on both the client (UI) and server (API) side
   - Never rely solely on client-side checks

4. **Consistent Permission Names**
   - Follow the naming convention: `Resource.Action`
   - Example: `Appointments.Manage`, `Patients.View`

5. **Document Permission Requirements**
   - Document which permissions are required for each feature
   - Update this documentation when adding new features

## Adding New Permissions

1. Add the permission constant to `Permissions.cs`:
```csharp
public const string NewFeature_Action = "NewFeature.Action";
```

2. Add the permission to the appropriate role method:
```csharp
private static string[] GetDoctorPermissions()
{
    return new[]
    {
        // ... existing permissions
        Permissions.NewFeature_Action
    };
}
```

3. Create an authorization policy in `Program.cs` (optional):
```csharp
options.AddPolicy("CanAccessNewFeature", policy =>
    policy.Requirements.Add(new PermissionRequirement(Permissions.NewFeature_Action)));
```

4. Use the permission in your code:
```csharp
if (await AuthService.HasPermissionAsync(Permissions.NewFeature_Action))
{
    // Feature code
}
```

## Troubleshooting

### Permission Not Working
1. Verify the role is assigned to the user
2. Check that the permission is included in the role's permission list
3. Ensure the authorization handler is registered in `Program.cs`
4. Check that the user is authenticated

### Role Not Working
1. Verify the role exists in the database
2. Check that the user is assigned to the role
3. Ensure the role name matches exactly (case-sensitive)

### Access Denied Errors
- Check the `AccessDeniedPath` in `Program.cs` cookie configuration
- Ensure proper authorization attributes are applied
- Verify the user has the required permissions/roles

