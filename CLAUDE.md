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

## Logging Best Practices

**IMPORTANT**: This project uses **Serilog** for structured logging. Logging extensively is critical for debugging, monitoring, and understanding application behavior.

### Logging Philosophy

- **Log liberally** - When in doubt, add a log statement
- **Use structured logging** - Include context properties for filtering and searching
- **Log at appropriate levels** - Use the correct log level for each situation
- **Log meaningful context** - Include IDs, user information, and relevant state

### When to Log

**ALWAYS log the following**:

1. **Service method entry/exit** - Log when entering and exiting application service methods
   ```csharp
   _logger.LogInformation("Creating task with title {TaskTitle} for user {UserId}", title, userId);
   ```

2. **Domain events** - Log when domain events are raised or handled
   ```csharp
   _logger.LogInformation("Task {TaskId} completed by user {UserId}", taskId, userId);
   ```

3. **External service calls** - Log before and after calling external APIs (Teams, Graph API)
   ```csharp
   _logger.LogInformation("Fetching assignments from Teams for user {UserId}", userId);
   ```

4. **Database operations** - Log repository operations (EF Core logs queries automatically)
   ```csharp
   _logger.LogInformation("Saving {EntityCount} entities to database", entities.Count);
   ```

5. **State changes** - Log significant application state changes
   ```csharp
   _logger.LogInformation("Pomodoro session {SessionId} started for task {TaskId}", sessionId, taskId);
   ```

6. **Errors and exceptions** - ALWAYS log exceptions with full context
   ```csharp
   _logger.LogError(ex, "Failed to create task {TaskTitle} for user {UserId}", title, userId);
   ```

7. **Validation failures** - Log when validation fails
   ```csharp
   _logger.LogWarning("Task validation failed for user {UserId}: {ValidationErrors}", userId, errors);
   ```

8. **Background job execution** - Log background task start, completion, and failures
   ```csharp
   _logger.LogInformation("Teams sync job started for user {UserId}", userId);
   ```

### Log Levels

- **Trace**: Very detailed diagnostic information (rarely used)
- **Debug**: Detailed debugging information (development only)
- **Information**: General informational messages about application flow
- **Warning**: Unexpected but handled situations (validation failures, retries)
- **Error**: Error events that don't stop execution
- **Critical**: Fatal errors that cause application failure

### What NOT to Log

- ❌ Passwords, tokens, or API keys
- ❌ Personally identifiable information (PII) without masking
- ❌ Complete sensitive user data
- ❌ Inside tight loops (use sampling or aggregate logging)

### Structured Logging Example

```csharp
// Good - Structured logging with context
_logger.LogInformation(
    "User {UserId} completed task {TaskId} in category {CategoryName} after {Duration}ms",
    userId, taskId, categoryName, duration);

// Bad - String interpolation loses structure
_logger.LogInformation($"User {userId} completed task {taskId}");
```

### Best Practices

1. **Use dependency injection** - Inject `ILogger<T>` into your classes
2. **Use semantic naming** - Property names should be clear and consistent
3. **Include correlation IDs** - For tracking requests across layers
4. **Log performance metrics** - For slow operations, log duration
5. **Log user actions** - Help understand user behavior and debug issues
6. **Review logs regularly** - Logs should be useful and not noise

### Configuration

Logging is configured in `appsettings.json`:
- Console sink for development
- File sink for production (`logs/` folder)
- Minimum level configurable per namespace
- Request logging middleware captures HTTP requests
