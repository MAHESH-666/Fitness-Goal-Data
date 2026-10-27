# Fitness Goal — ASP.NET Core MVC Starter

A .NET 8 ASP.NET Core MVC project scaffold for a fitness-goal themed web application. The current source includes the MVC home and privacy/error pages, styles, and static assets.

## Technology

- C#
- .NET 8
- ASP.NET Core MVC
- Razor views
- HTML, CSS, JavaScript

## Run locally

1. Install the .NET 8 SDK.
2. Clone the repository and open `Fitness Goal.sln` in Visual Studio or open the project folder in a terminal.
3. Restore packages:
   ```bash
   dotnet restore
   ```
4. Run the web project:
   ```bash
   dotnet run --project "Fitness Goal/Fitness Goal.csproj"
   ```
5. Open the local URL printed by the application.

## Current scope

The repository is primarily an MVC starter/landing-page structure; the currently inspected controllers do not establish a full fitness tracking backend. Add documented goal, workout, or progress features as they are implemented.

## Contribution ideas

- Add validated fitness-goal forms and persistence.
- Add tests for any business logic.
- Keep real personal/health records out of source control.
