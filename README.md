# TaskScheduler

A task management application designed to help neurodiverse individuals manage their daily tasks, homework, routines, and schedules effectively.

## Overview

TaskScheduler is built using .NET and Blazor Server, following Clean Architecture principles. The application will eventually support .NET MAUI for cross-platform desktop and mobile deployment.

## Key Features (Planned)

- Task management with sub-tasks and checklists
- Microsoft Teams for Education integration
- Calendar views (Day/Week/Month)
- Pomodoro timer (20-minute focus sessions)
- Daily routines and checklists
- School packing reminders
- Visual progress tracking
- Neurodiverse-friendly UI design

## Solution Structure

The solution follows Clean Architecture with clear separation of concerns:

```
TaskScheduler/
├── src/
│   ├── TaskScheduler.Domain/           # Core business entities and domain logic
│   ├── TaskScheduler.Application/      # Use cases, DTOs, and application services
│   ├── TaskScheduler.Infrastructure/   # Data access, external services, and implementations
│   └── TaskScheduler.BlazorServer/     # Blazor Server web UI
├── docs/
│   ├── PRD.md                          # Product Requirements Document
│   ├── TechnicalDesign.md              # Technical architecture and design
│   └── DevelopmentRoadmap.md           # Development tasks and sprints
└── TaskScheduler.sln                   # Solution file
```

### Project Dependencies

- **Domain**: No dependencies (core business logic)
- **Application**: Depends on Domain
- **Infrastructure**: Depends on Application and Domain
- **BlazorServer**: Depends on Application and Infrastructure

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later
- Visual Studio 2022, VS Code, or Rider

### Building the Solution

```bash
dotnet build TaskScheduler.sln
```

### Running the Application

```bash
dotnet run --project src/TaskScheduler.BlazorServer
```

## Documentation

For detailed information about the project:

- [Product Requirements Document](docs/PRD.md) - Complete feature requirements and user stories
- [Technical Design](docs/TechnicalDesign.md) - Architecture and design patterns
- [Development Roadmap](docs/DevelopmentRoadmap.md) - Sprint planning and task breakdown
- [CLAUDE.md](CLAUDE.md) - Guidance for Claude Code AI assistant

## Technology Stack

- **.NET 9.0** - Application framework
- **Blazor Server** - Web UI framework (Phase 1)
- **.NET MAUI Blazor Hybrid** - Cross-platform deployment (Phase 2)
- **Entity Framework Core 8** - ORM and data access
- **SQLite** - Local database
- **Microsoft Graph API** - Teams for Education integration
- **xUnit, bUnit, Moq** - Testing frameworks

## Development Status

Currently in **Sprint 0** - Project setup and foundation layer development.

See the [Development Roadmap](docs/DevelopmentRoadmap.md) for detailed progress and upcoming tasks.
