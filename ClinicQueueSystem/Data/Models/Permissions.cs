namespace ClinicQueueSystem.Data.Models;

/// <summary>
/// Defines all available permissions in the system
/// </summary>
public static class Permissions
{
    // Appointment Permissions
    public const string Appointments_View = "Appointments.View";
    public const string Appointments_Book = "Appointments.Book";
    public const string Appointments_Cancel = "Appointments.Cancel";
    public const string Appointments_Manage = "Appointments.Manage"; // Full CRUD for staff

    // Patient Records Permissions
    public const string Patients_View = "Patients.View";
    public const string Patients_ViewOwn = "Patients.ViewOwn";
    public const string Patients_Edit = "Patients.Edit";
    public const string Patients_Create = "Patients.Create";

    // Queue Management Permissions
    public const string Queue_View = "Queue.View";
    public const string Queue_Manage = "Queue.Manage";
    public const string Queue_CheckIn = "Queue.CheckIn";

    // Visit Notes Permissions
    public const string VisitNotes_View = "VisitNotes.View";
    public const string VisitNotes_Create = "VisitNotes.Create";
    public const string VisitNotes_Edit = "VisitNotes.Edit";

    // Schedule Permissions
    public const string Schedules_View = "Schedules.View";
    public const string Schedules_Manage = "Schedules.Manage";
    public const string Schedules_ViewOwn = "Schedules.ViewOwn";

    // User Management Permissions
    public const string Users_View = "Users.View";
    public const string Users_Create = "Users.Create";
    public const string Users_Edit = "Users.Edit";
    public const string Users_Delete = "Users.Delete";
    public const string Users_ManageRoles = "Users.ManageRoles";

    // Reporting Permissions
    public const string Reports_View = "Reports.View";
    public const string Reports_Export = "Reports.Export";

    /// <summary>
    /// Gets all permissions for a specific role
    /// </summary>
    public static string[] GetPermissionsForRole(string role)
    {
        return role switch
        {
            "Admin" => GetAllPermissions(),
            "Doctor" => GetDoctorPermissions(),
            "Nurse" => GetNursePermissions(),
            "Health Records" => GetHealthRecordsPermissions(),
            "Patient" => GetPatientPermissions(),
            _ => Array.Empty<string>()
        };
    }

    private static string[] GetAllPermissions()
    {
        return new[]
        {
            Appointments_View, Appointments_Book, Appointments_Cancel, Appointments_Manage,
            Patients_View, Patients_ViewOwn, Patients_Edit, Patients_Create,
            Queue_View, Queue_Manage, Queue_CheckIn,
            VisitNotes_View, VisitNotes_Create, VisitNotes_Edit,
            Schedules_View, Schedules_Manage, Schedules_ViewOwn,
            Users_View, Users_Create, Users_Edit, Users_Delete, Users_ManageRoles,
            Reports_View, Reports_Export
        };
    }

    private static string[] GetDoctorPermissions()
    {
        return new[]
        {
            Appointments_View, Appointments_Manage,
            Patients_View, Patients_ViewOwn, Patients_Edit,
            Queue_View,
            VisitNotes_View, VisitNotes_Create, VisitNotes_Edit,
            Schedules_View, Schedules_Manage, Schedules_ViewOwn,
            Reports_View
        };
    }

    private static string[] GetNursePermissions()
    {
        return new[]
        {
            Appointments_View,
            Patients_View, Patients_ViewOwn, Patients_Edit,
            Queue_View, Queue_Manage, Queue_CheckIn,
            VisitNotes_View, VisitNotes_Create, VisitNotes_Edit,
            Schedules_View, Schedules_ViewOwn
        };
    }

    private static string[] GetHealthRecordsPermissions()
    {
        return new[]
        {
            Appointments_View, Appointments_Manage, Appointments_Book,
            Patients_View, Patients_ViewOwn, Patients_Edit, Patients_Create,
            Queue_View, Queue_Manage, Queue_CheckIn,
            Schedules_View
        };
    }

    private static string[] GetPatientPermissions()
    {
        return new[]
        {
            Appointments_View, Appointments_Book, Appointments_Cancel,
            Patients_ViewOwn,
            Queue_CheckIn
        };
    }
}

