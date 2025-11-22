# Clinic Queue & Appointments System

A modern .NET Blazor web application for managing patient flow, appointments, and queue management in healthcare clinics.

## Team Members

- Francois Matenda Tshibala (Team Lead) - GitHub: francktshibala
- Adetokunbo Olutola Osibo
- Daniel Adetaba Adongo - GitHub: Adetabanat
- Bimbola Peace Osho-Lowu - GitHub: bimbolin
- Ernest Ojakol
- Andrew Chikezie Obinna Onyekwere
- Olubisi Olatunde Ayantoye
- Gerald Robert Newell - GitHub: elcapitan-g
- Andrew Omoniyi Mogbeyiromore

## Project Description

The Clinic Queue & Appointments System is a comprehensive digital solution designed to modernize patient flow management in small to medium-sized healthcare clinics. The application enables patients to book appointments, check in digitally, and track their queue status in real-time.

### Key Features

1. **User Authentication** - Secure role-based access control
2. **Patient Self Check-In** - Digital check-in with queue position tracking
3. **Appointment Booking** - Online appointment scheduling and management
4. **Queue Management** - Real-time queue status for staff and patients
5. **Provider Schedules** - Doctor and nurse schedule management
6. **Visit Notes** - Medical history and visit documentation
7. **Admin Dashboard** - System administration and user management

## Technology Stack

- **Framework:** .NET 8.0 with Blazor Server
- **Database:** SQLite (Development) / SQL Server (Production)
- **ORM:** Entity Framework Core 8.0
- **Authentication:** ASP.NET Core Identity
- **UI:** Bootstrap 5 with Blazor components

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/downloads)
- SQLite (included with .NET EF Core tools)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/francktshibala/cse_325_team05.git
cd cse_325_team05
```

### 2. Install .NET EF Core Tools (if not already installed)

```bash
dotnet tool install --global dotnet-ef
```

### 3. Restore NuGet Packages

```bash
cd ClinicQueueSystem
dotnet restore
```

### 4. Apply Database Migrations

The database will be created automatically when you first run the application. However, you can manually create/update it:

```bash
dotnet ef database update
```

This creates a SQLite database file named `clinicqueue.db` in the project directory.

### 5. Run the Application

```bash
dotnet run
```

Or press **F5** in Visual Studio.

The application will start and be accessible at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

### 6. Default Admin Credentials

A default admin account is created automatically:

- **Email:** admin@clinicqueue.com
- **Password:** Admin123!

**Important:** Change this password immediately in production!

## Project Structure

```
ClinicQueueSystem/
├── Components/
│   ├── Layout/           # Layout components (MainLayout, NavMenu)
│   ├── Pages/            # Blazor pages/routes
│   └── _Imports.razor    # Global using directives
├── Data/
│   ├── Models/           # Entity models (Patient, Provider, Appointment, etc.)
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs  # Database seeding
├── wwwroot/              # Static files (CSS, JS, images)
├── Program.cs            # Application entry point
└── appsettings.json      # Configuration
```

## Database Schema

The application uses the following main entities:

- **ApplicationUser** - User accounts with Identity
- **Patient** - Patient profiles and medical information
- **Provider** - Doctors and nurses
- **Appointment** - Scheduled appointments
- **QueueTicket** - Check-in queue tracking
- **Visit** - Patient visit records
- **VisitNote** - Medical notes and documentation
- **Schedule** - Provider availability schedules

## Development Workflow

### Creating a New Feature Branch

```bash
git checkout develop
git pull origin develop
git checkout -b feature/your-feature-name
```

### Making Changes

1. Write your code
2. Test locally
3. Commit with meaningful messages:

```bash
git add .
git commit -m "Add appointment booking feature"
```

### Submitting a Pull Request

```bash
git push origin feature/your-feature-name
```

Then create a Pull Request on GitHub to merge into `develop`.

## Database Migrations

### Adding a New Migration

After modifying entity models:

```bash
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

### Reverting a Migration

```bash
dotnet ef migrations remove
```

## Common Tasks

### Running the Application

```bash
dotnet run
```

### Running with Hot Reload (Watch Mode)

```bash
dotnet watch run
```

### Building for Production

```bash
dotnet publish -c Release -o ./publish
```

### Running Tests (when tests are added)

```bash
dotnet test
```

## Troubleshooting

### Database Issues

**Problem:** Database not found or migration errors

**Solution:**
```bash
# Delete the database file
rm clinicqueue.db

# Recreate with migrations
dotnet ef database update
```

### Port Already in Use

**Problem:** Application fails to start due to port conflict

**Solution:** Change the port in `appsettings.json` or `launchSettings.json`

### NuGet Package Restore Fails

**Solution:**
```bash
dotnet clean
dotnet restore --force
```

## Team Development Guidelines

1. **Never commit directly to `main`** - Always use feature branches
2. **Pull before you push** - Always `git pull` before starting work
3. **Write meaningful commit messages** - Describe what and why, not how
4. **Test your changes** - Ensure the app builds and runs before committing
5. **Ask for help** - Use the team chat if you're stuck

## User Roles

The system has four predefined roles:

- **Admin** - Full system access and user management
- **Doctor** - Patient information, schedules, medical notes
- **Nurse** - Queue management and patient intake
- **Patient** - Personal appointments and check-in

## Resources

- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.0/)

## Project Links

- **Repository:** https://github.com/francktshibala/cse_325_team05
- **Trello Board:** https://trello.com/b/UHH3f7Lh/team-5
- **Implementation Plan:** See team shared documents

## Support

If you encounter any issues or have questions:

1. Check this README first
2. Search existing GitHub Issues
3. Ask in the team Microsoft Teams channel
4. Create a new GitHub Issue if needed
5. Contact the team lead: Francois Matenda Tshibala

## License

This project is developed as part of CSE 325 coursework at BYU-Idaho.

---

**Last Updated:** November 2024
**Course:** CSE 325 - .NET Software Development
**Institution:** BYU-Idaho
