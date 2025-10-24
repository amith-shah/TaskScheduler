# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TaskScheduler is an application designed to help neurodiverse folks manage tasks. The project uses .NET as its primary technology stack.

## Project Documentation

Before working on this project, please review the following comprehensive documentation located in the `docs/` folder:

1. **[Product Requirements Document (PRD)](docs/PRD.md)** - Complete feature requirements, user stories, and product vision organized into 3 development phases. Includes must-have features like Microsoft Teams for Education integration, 20-minute Pomodoro sessions, calendar views, and school packing reminders.

2. **[Technical Design](docs/TechnicalDesign.md)** - Detailed technical architecture using Clean Architecture principles with 4 layers (Domain, Application, Infrastructure, Presentation). Includes state management patterns, component design, testing strategy, and the path for migrating from Blazor Server to .NET MAUI Blazor Hybrid with 90%+ code reuse.

3. **[Development Roadmap](docs/DevelopmentRoadmap.md)** - All 85 development tasks broken into 6 sprints over 12 weeks. Each task is scoped to maximum 1 day effort with clear dependencies and acceptance criteria. Tasks are tracked as GitHub issues organized into sprint milestones.

## Development Commands

*Note: This section will be populated once the project structure is established.*

### Building
- Build command: TBD
- Build configuration options: TBD

### Testing
- Run all tests: `dotnet test`
- Run specific test project: `dotnet test <project-path>`
- Test frameworks:
  - **xUnit** - Unit testing for domain and application logic
  - **bUnit** - Component testing for Blazor components
  - **Moq** - Mocking framework for dependencies

See [Technical Design - Testing Strategy](docs/TechnicalDesign.md#testing-strategy) for detailed testing approach and examples.

### Running the Application
- Start application: TBD
- Development mode: TBD

## Architecture

This project follows **Clean Architecture** principles with a clear separation of concerns. For complete architectural details, see [Technical Design](docs/TechnicalDesign.md).

### Project Structure

The solution is organized into 4 main layers:
- **Domain** (`TaskScheduler.Domain`) - Core business entities, value objects, and domain logic
- **Application** (`TaskScheduler.Application`) - Use cases, DTOs, interfaces, and application services
- **Infrastructure** (`TaskScheduler.Infrastructure`) - Data access, external services, and Microsoft Graph integration
- **Presentation** (`TaskScheduler.BlazorServer`) - Blazor Server UI with Razor components

See [Technical Design - Project Structure](docs/TechnicalDesign.md#project-structure) for detailed folder organization.

### Key Components

- **State Management**: Custom event aggregator pattern for component communication
- **Data Access**: Repository pattern with Unit of Work using Entity Framework Core 8 and SQLite
- **Authentication**: MSAL (Microsoft Authentication Library) for Microsoft 365 integration
- **UI Components**: Smart (container) and Presentational component separation

See [Technical Design - Architecture Overview](docs/TechnicalDesign.md#architecture-overview) for detailed component interactions.

### Data Models

Core domain entities include:
- `TaskItem` - Tasks with support for subtasks, recurrence, and Pomodoro sessions
- `Category` - Configurable task categories with color coding
- `Routine` - Daily/weekly routines with checklists
- `PackingList` - School packing reminders (sports kit, PE bag, etc.)
- `PomodoroSession` - 20-minute focus sessions with breaks
- `CalendarEvent` - Integrated calendar entries from Teams and manual entries

See [Technical Design - Domain Layer](docs/TechnicalDesign.md#domain-layer) for complete entity definitions and relationships.
