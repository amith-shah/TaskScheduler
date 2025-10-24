# Technical Design Document

## TaskScheduler - Architecture & Design

**Version:** 1.0
**Date:** October 24, 2025
**Status:** Design Phase

---

## Table of Contents

1. [Introduction](#1-introduction)
2. [Architectural Principles](#2-architectural-principles)
3. [Clean Architecture Overview](#3-clean-architecture-overview)
4. [Project Structure](#4-project-structure)
5. [Domain Layer](#5-domain-layer)
6. [Application Layer](#6-application-layer)
7. [Infrastructure Layer](#7-infrastructure-layer)
8. [Presentation Layer](#8-presentation-layer)
9. [State Management Architecture](#9-state-management-architecture)
10. [Component Design Patterns](#10-component-design-patterns)
11. [Dependency Injection Strategy](#11-dependency-injection-strategy)
12. [Testing Strategy](#12-testing-strategy)
13. [Data Flow & Communication](#13-data-flow--communication)
14. [MAUI Migration Path](#14-maui-migration-path)
15. [Design Patterns Catalog](#15-design-patterns-catalog)
16. [Code Examples](#16-code-examples)
17. [Best Practices & Guidelines](#17-best-practices--guidelines)

---

## 1. Introduction

### 1.1 Purpose

This document defines the technical architecture and design patterns for the TaskScheduler application, a neurodiverse-friendly task management system. The architecture prioritizes:

- **Clean Architecture principles** for maintainability and testability
- **SOLID principles** for robust, extensible code
- **Separation of concerns** for clear responsibility boundaries
- **MAUI-ready design** to minimize migration effort in Phase 2
- **Comprehensive testing** at all layers

### 1.2 Technology Stack

- **.NET 8** (LTS)
- **ASP.NET Core Blazor Server** (Phase 1)
- **Entity Framework Core 8** with SQLite
- **xUnit** for unit testing
- **bUnit** for component testing
- **Moq** for mocking
- **FluentAssertions** for readable assertions

### 1.3 Key Design Goals

1. **Testability**: Every layer and component can be tested in isolation
2. **Reusability**: Components and services can be reused across platforms
3. **Maintainability**: Clear structure makes code easy to understand and modify
4. **Extensibility**: New features can be added with minimal changes to existing code
5. **MAUI Readiness**: Architecture supports migration to .NET MAUI Blazor Hybrid with minimal refactoring

---

## 2. Architectural Principles

### 2.1 SOLID Principles

#### Single Responsibility Principle (SRP)
- Each class has one reason to change
- Services handle single concerns (TaskService, NotificationService, etc.)
- Components focus on single UI responsibilities

#### Open/Closed Principle (OCP)
- Open for extension, closed for modification
- Use interfaces and abstractions for platform-specific behavior
- Strategy pattern for varying behaviors

#### Liskov Substitution Principle (LSP)
- Implementations can be swapped without breaking functionality
- Interface contracts are honored
- Mock implementations for testing

#### Interface Segregation Principle (ISP)
- Focused, cohesive interfaces
- No "fat" interfaces forcing unnecessary implementations
- Role-based interfaces (IReadRepository, IWriteRepository)

#### Dependency Inversion Principle (DIP)
- Depend on abstractions, not concretions
- All cross-layer dependencies use interfaces
- Dependency injection for all services

### 2.2 Clean Architecture Principles

1. **Independence of Frameworks**: Business logic doesn't depend on Blazor, EF, or any framework
2. **Testability**: Business rules testable without UI, database, or external services
3. **Independence of UI**: UI can change without affecting business logic
4. **Independence of Database**: Can swap SQLite for another database with minimal changes
5. **Independence of External Agencies**: Business rules don't know about external services

### 2.3 Domain-Driven Design Concepts

- **Entities**: Core business objects with identity (Task, Category, Routine)
- **Value Objects**: Immutable objects without identity (TimeRange, RecurrencePattern)
- **Aggregates**: Cluster of entities treated as a unit (Task with SubTasks)
- **Domain Events**: Represent something that happened (TaskCompleted, RoutineStarted)
- **Repositories**: Abstraction for data access
- **Services**: Operations that don't fit naturally on entities

---

## 3. Clean Architecture Overview

### 3.1 Layer Dependency Flow

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                        │
│              (Blazor Components, Pages, ViewModels)          │
│                   Depends on: Application                    │
└─────────────────────────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────┐
│                    Application Layer                         │
│          (Use Cases, Services, Interfaces, DTOs)             │
│                   Depends on: Domain                         │
└─────────────────────────────────────────────────────────────┘
                             ↓
┌─────────────────────────────────────────────────────────────┐
│                      Domain Layer                            │
│           (Entities, Value Objects, Domain Events)           │
│                   Depends on: Nothing                        │
└─────────────────────────────────────────────────────────────┘
                             ↑
┌─────────────────────────────────────────────────────────────┐
│                  Infrastructure Layer                        │
│        (Data Access, External Services, Notifications)       │
│            Depends on: Application, Domain                   │
└─────────────────────────────────────────────────────────────┘
```

**Key Rules:**
- Domain layer has NO dependencies on other layers
- Application layer depends only on Domain
- Infrastructure implements interfaces defined in Application
- Presentation depends on Application (and transitively Domain)
- Infrastructure is "pluggable" via dependency injection

---

## 4. Project Structure

### 4.1 Solution Organization

```
TaskScheduler/
├── src/
│   ├── TaskScheduler.Domain/              # Core business entities
│   │   ├── Entities/
│   │   │   ├── TaskItem.cs
│   │   │   ├── Category.cs
│   │   │   ├── Routine.cs
│   │   │   ├── RoutineStep.cs
│   │   │   ├── PackItem.cs
│   │   │   ├── PomodoroSession.cs
│   │   │   └── BaseEntity.cs
│   │   ├── ValueObjects/
│   │   │   ├── RecurrencePattern.cs
│   │   │   ├── TimeRange.cs
│   │   │   ├── NotificationSettings.cs
│   │   │   └── Priority.cs
│   │   ├── Enums/
│   │   │   ├── TaskStatus.cs
│   │   │   ├── RoutineStepType.cs
│   │   │   ├── PackItemCategory.cs
│   │   │   └── DayOfWeek.cs
│   │   ├── Events/
│   │   │   ├── TaskCompletedEvent.cs
│   │   │   ├── PomodoroSessionCompletedEvent.cs
│   │   │   └── RoutineCompletedEvent.cs
│   │   └── Exceptions/
│   │       ├── DomainException.cs
│   │       └── ValidationException.cs
│   │
│   ├── TaskScheduler.Application/          # Business logic & use cases
│   │   ├── Common/
│   │   │   ├── Interfaces/
│   │   │   │   ├── IDateTime.cs
│   │   │   │   ├── IEventAggregator.cs
│   │   │   │   └── IStateContainer.cs
│   │   │   ├── Models/
│   │   │   │   ├── Result.cs
│   │   │   │   └── PagedResult.cs
│   │   │   └── Behaviors/
│   │   ├── Interfaces/
│   │   │   ├── Repositories/
│   │   │   │   ├── ITaskRepository.cs
│   │   │   │   ├── ICategoryRepository.cs
│   │   │   │   ├── IRoutineRepository.cs
│   │   │   │   ├── IPackItemRepository.cs
│   │   │   │   └── IPomodoroSessionRepository.cs
│   │   │   ├── Services/
│   │   │   │   ├── INotificationService.cs
│   │   │   │   ├── IFileService.cs
│   │   │   │   ├── ISecureStorageService.cs
│   │   │   │   └── ITeamsIntegrationService.cs
│   │   │   └── IUnitOfWork.cs
│   │   ├── Services/
│   │   │   ├── TaskService.cs
│   │   │   ├── CategoryService.cs
│   │   │   ├── RoutineService.cs
│   │   │   ├── PomodoroService.cs
│   │   │   ├── PackingListService.cs
│   │   │   ├── AnalyticsService.cs
│   │   │   └── RecurrenceService.cs
│   │   ├── DTOs/
│   │   │   ├── TaskDto.cs
│   │   │   ├── CategoryDto.cs
│   │   │   ├── RoutineDto.cs
│   │   │   └── PomodoroSessionDto.cs
│   │   ├── Mapping/
│   │   │   └── AutoMapperProfile.cs
│   │   └── Validators/
│   │       ├── TaskValidator.cs
│   │       └── CategoryValidator.cs
│   │
│   ├── TaskScheduler.Infrastructure/        # External concerns
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs
│   │   │   ├── Configurations/
│   │   │   │   ├── TaskConfiguration.cs
│   │   │   │   ├── CategoryConfiguration.cs
│   │   │   │   └── RoutineConfiguration.cs
│   │   │   ├── Repositories/
│   │   │   │   ├── TaskRepository.cs
│   │   │   │   ├── CategoryRepository.cs
│   │   │   │   ├── RoutineRepository.cs
│   │   │   │   └── BaseRepository.cs
│   │   │   ├── UnitOfWork.cs
│   │   │   └── Migrations/
│   │   ├── Services/
│   │   │   ├── BrowserNotificationService.cs  # Phase 1
│   │   │   ├── LocalFileService.cs
│   │   │   ├── TeamsIntegrationService.cs
│   │   │   └── DateTimeService.cs
│   │   └── DependencyInjection.cs
│   │
│   ├── TaskScheduler.Web/                   # Blazor Server (Phase 1)
│   │   ├── Components/
│   │   │   ├── Shared/
│   │   │   │   ├── MainLayout.razor
│   │   │   │   ├── NavMenu.razor
│   │   │   │   └── FocusLayout.razor
│   │   │   ├── Tasks/
│   │   │   │   ├── TaskList.razor
│   │   │   │   ├── TaskCard.razor
│   │   │   │   ├── TaskDetail.razor
│   │   │   │   ├── TaskForm.razor
│   │   │   │   └── SubTaskList.razor
│   │   │   ├── Calendar/
│   │   │   │   ├── CalendarView.razor
│   │   │   │   ├── DayView.razor
│   │   │   │   ├── WeekView.razor
│   │   │   │   └── MonthView.razor
│   │   │   ├── Pomodoro/
│   │   │   │   ├── PomodoroTimer.razor
│   │   │   │   ├── BreakScreen.razor
│   │   │   │   └── SessionProgress.razor
│   │   │   ├── Routines/
│   │   │   │   ├── RoutineList.razor
│   │   │   │   ├── RoutineExecution.razor
│   │   │   │   └── RoutineStepItem.razor
│   │   │   ├── PackList/
│   │   │   │   ├── PackingList.razor
│   │   │   │   └── PackItemCard.razor
│   │   │   └── Common/
│   │   │       ├── ProgressRing.razor
│   │   │       ├── CategoryBadge.razor
│   │   │       ├── StreakDisplay.razor
│   │   │       └── LoadingSpinner.razor
│   │   ├── Pages/
│   │   │   ├── Index.razor                  # Dashboard
│   │   │   ├── TasksPage.razor
│   │   │   ├── CalendarPage.razor
│   │   │   ├── RoutinesPage.razor
│   │   │   └── SettingsPage.razor
│   │   ├── State/
│   │   │   ├── AppState.cs
│   │   │   ├── TaskState.cs
│   │   │   ├── PomodoroState.cs
│   │   │   ├── RoutineState.cs
│   │   │   └── EventAggregator.cs
│   │   ├── ViewModels/
│   │   │   ├── TaskViewModel.cs
│   │   │   ├── CalendarViewModel.cs
│   │   │   └── PomodoroViewModel.cs
│   │   ├── wwwroot/
│   │   │   ├── css/
│   │   │   ├── js/
│   │   │   └── sounds/
│   │   ├── Program.cs
│   │   ├── App.razor
│   │   └── _Imports.razor
│   │
│   └── TaskScheduler.Shared/                # Shared for MAUI migration
│       └── (Components will move here in Phase 2)
│
├── tests/
│   ├── TaskScheduler.Domain.Tests/
│   │   ├── Entities/
│   │   │   ├── TaskItemTests.cs
│   │   │   └── RoutineTests.cs
│   │   └── ValueObjects/
│   │       └── RecurrencePatternTests.cs
│   ├── TaskScheduler.Application.Tests/
│   │   ├── Services/
│   │   │   ├── TaskServiceTests.cs
│   │   │   ├── PomodoroServiceTests.cs
│   │   │   └── RoutineServiceTests.cs
│   │   └── Validators/
│   │       └── TaskValidatorTests.cs
│   ├── TaskScheduler.Infrastructure.Tests/
│   │   ├── Repositories/
│   │   │   └── TaskRepositoryTests.cs
│   │   └── Services/
│   │       └── TeamsIntegrationServiceTests.cs
│   └── TaskScheduler.Web.Tests/
│       ├── Components/
│       │   ├── TaskCardTests.cs
│       │   ├── PomodoroTimerTests.cs
│       │   └── RoutineExecutionTests.cs
│       └── Pages/
│           └── IndexPageTests.cs
│
├── docs/
│   ├── PRD.md
│   ├── TechnicalDesign.md
│   └── API.md
│
├── TaskScheduler.sln
├── .gitignore
├── README.md
└── CLAUDE.md
```

### 4.2 Project References

```
TaskScheduler.Web
  ├─> TaskScheduler.Application
  ├─> TaskScheduler.Infrastructure
  └─> TaskScheduler.Domain (transitive)

TaskScheduler.Infrastructure
  ├─> TaskScheduler.Application
  └─> TaskScheduler.Domain (transitive)

TaskScheduler.Application
  └─> TaskScheduler.Domain

TaskScheduler.Domain
  └─> (no dependencies)
```

---

## 5. Domain Layer

### 5.1 Purpose

The Domain layer contains the core business logic and is the heart of the application. It has **zero dependencies** on other layers or frameworks.

### 5.2 Key Components

#### 5.2.1 Entities

**Base Entity:**
```csharp
namespace TaskScheduler.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime ModifiedDate { get; protected set; }

    protected BaseEntity()
    {
        CreatedDate = DateTime.UtcNow;
        ModifiedDate = DateTime.UtcNow;
    }

    public void UpdateModifiedDate()
    {
        ModifiedDate = DateTime.UtcNow;
    }
}
```

**TaskItem Entity:**
```csharp
namespace TaskScheduler.Domain.Entities;

public class TaskItem : BaseEntity
{
    private readonly List<TaskItem> _subTasks = new();

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public DateTime? DueDate { get; private set; }
    public int? EstimatedDuration { get; private set; } // minutes
    public Priority Priority { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedDate { get; private set; }
    public bool IsRecurring { get; private set; }
    public RecurrencePattern? RecurrencePattern { get; private set; }
    public int? ParentTaskId { get; private set; }
    public TaskItem? ParentTask { get; private set; }
    public IReadOnlyCollection<TaskItem> SubTasks => _subTasks.AsReadOnly();
    public bool IsFromTeams { get; private set; }
    public string? TeamsAssignmentId { get; private set; }

    // Domain Events
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // Private constructor for EF
    private TaskItem() { }

    // Factory method
    public static TaskItem Create(
        string title,
        int categoryId,
        string? description = null,
        DateTime? dueDate = null,
        int? estimatedDuration = null,
        Priority priority = Priority.Medium)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Task title cannot be empty");

        if (title.Length > 200)
            throw new DomainException("Task title cannot exceed 200 characters");

        var task = new TaskItem
        {
            Title = title,
            CategoryId = categoryId,
            Description = description,
            DueDate = dueDate,
            EstimatedDuration = estimatedDuration,
            Priority = priority,
            IsCompleted = false
        };

        return task;
    }

    // Business logic methods
    public void Complete()
    {
        if (IsCompleted)
            return;

        IsCompleted = true;
        CompletedDate = DateTime.UtcNow;
        UpdateModifiedDate();

        _domainEvents.Add(new TaskCompletedEvent(this));
    }

    public void Uncomplete()
    {
        IsCompleted = false;
        CompletedDate = null;
        UpdateModifiedDate();
    }

    public void UpdateDetails(
        string? title = null,
        string? description = null,
        DateTime? dueDate = null,
        int? estimatedDuration = null,
        Priority? priority = null)
    {
        if (title != null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Task title cannot be empty");
            Title = title;
        }

        if (description != null)
            Description = description;

        if (dueDate.HasValue)
            DueDate = dueDate.Value;

        if (estimatedDuration.HasValue)
            EstimatedDuration = estimatedDuration.Value;

        if (priority.HasValue)
            Priority = priority.Value;

        UpdateModifiedDate();
    }

    public void AddSubTask(TaskItem subTask)
    {
        if (ParentTaskId.HasValue)
            throw new DomainException("Cannot add subtasks to a subtask");

        if (subTask.ParentTaskId.HasValue)
            throw new DomainException("Task already has a parent");

        _subTasks.Add(subTask);
        subTask.ParentTaskId = Id;
        UpdateModifiedDate();
    }

    public void RemoveSubTask(TaskItem subTask)
    {
        _subTasks.Remove(subTask);
        subTask.ParentTaskId = null;
        UpdateModifiedDate();
    }

    public void SetRecurrence(RecurrencePattern pattern)
    {
        IsRecurring = true;
        RecurrencePattern = pattern;
        UpdateModifiedDate();
    }

    public void RemoveRecurrence()
    {
        IsRecurring = false;
        RecurrencePattern = null;
        UpdateModifiedDate();
    }

    public void MarkAsFromTeams(string assignmentId)
    {
        IsFromTeams = true;
        TeamsAssignmentId = assignmentId;
        UpdateModifiedDate();
    }

    public int GetCompletionPercentage()
    {
        if (!_subTasks.Any())
            return IsCompleted ? 100 : 0;

        var completedCount = _subTasks.Count(st => st.IsCompleted);
        return (int)Math.Round((double)completedCount / _subTasks.Count * 100);
    }

    public bool ShouldBreakdownIntoPomodoros()
    {
        return EstimatedDuration.HasValue && EstimatedDuration.Value > 20;
    }

    public int GetSuggestedPomodoroCount(int sessionDuration = 20)
    {
        if (!EstimatedDuration.HasValue)
            return 1;

        return (int)Math.Ceiling((double)EstimatedDuration.Value / sessionDuration);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
```

#### 5.2.2 Value Objects

**RecurrencePattern Value Object:**
```csharp
namespace TaskScheduler.Domain.ValueObjects;

public class RecurrencePattern : ValueObject
{
    public RecurrenceType Type { get; private set; }
    public int Interval { get; private set; } // every X days/weeks/months
    public List<DayOfWeek>? DaysOfWeek { get; private set; } // for weekly
    public int? DayOfMonth { get; private set; } // for monthly
    public DateTime? EndDate { get; private set; }

    private RecurrencePattern() { }

    public static RecurrencePattern Daily(int interval = 1, DateTime? endDate = null)
    {
        return new RecurrencePattern
        {
            Type = RecurrenceType.Daily,
            Interval = interval,
            EndDate = endDate
        };
    }

    public static RecurrencePattern Weekly(
        List<DayOfWeek> daysOfWeek,
        int interval = 1,
        DateTime? endDate = null)
    {
        if (!daysOfWeek.Any())
            throw new DomainException("Weekly recurrence must specify at least one day");

        return new RecurrencePattern
        {
            Type = RecurrenceType.Weekly,
            Interval = interval,
            DaysOfWeek = daysOfWeek,
            EndDate = endDate
        };
    }

    public static RecurrencePattern Monthly(
        int dayOfMonth,
        int interval = 1,
        DateTime? endDate = null)
    {
        if (dayOfMonth < 1 || dayOfMonth > 31)
            throw new DomainException("Day of month must be between 1 and 31");

        return new RecurrencePattern
        {
            Type = RecurrenceType.Monthly,
            Interval = interval,
            DayOfMonth = dayOfMonth,
            EndDate = endDate
        };
    }

    public DateTime? GetNextOccurrence(DateTime fromDate)
    {
        if (EndDate.HasValue && fromDate >= EndDate.Value)
            return null;

        return Type switch
        {
            RecurrenceType.Daily => fromDate.AddDays(Interval),
            RecurrenceType.Weekly => GetNextWeeklyOccurrence(fromDate),
            RecurrenceType.Monthly => GetNextMonthlyOccurrence(fromDate),
            _ => throw new DomainException($"Unknown recurrence type: {Type}")
        };
    }

    private DateTime GetNextWeeklyOccurrence(DateTime fromDate)
    {
        // Implementation for finding next matching day of week
        var daysToAdd = 1;
        var nextDate = fromDate.AddDays(daysToAdd);

        while (!DaysOfWeek!.Contains(nextDate.DayOfWeek))
        {
            nextDate = nextDate.AddDays(1);
        }

        return nextDate;
    }

    private DateTime GetNextMonthlyOccurrence(DateTime fromDate)
    {
        var nextMonth = fromDate.AddMonths(Interval);
        return new DateTime(nextMonth.Year, nextMonth.Month, DayOfMonth!.Value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Type;
        yield return Interval;
        yield return DaysOfWeek;
        yield return DayOfMonth;
        yield return EndDate;
    }
}

public abstract class ValueObject
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}
```

#### 5.2.3 Domain Events

```csharp
namespace TaskScheduler.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}

public class TaskCompletedEvent : IDomainEvent
{
    public TaskItem Task { get; }
    public DateTime OccurredOn { get; }

    public TaskCompletedEvent(TaskItem task)
    {
        Task = task;
        OccurredOn = DateTime.UtcNow;
    }
}

public class PomodoroSessionCompletedEvent : IDomainEvent
{
    public PomodoroSession Session { get; }
    public DateTime OccurredOn { get; }

    public PomodoroSessionCompletedEvent(PomodoroSession session)
    {
        Session = session;
        OccurredOn = DateTime.UtcNow;
    }
}

public class RoutineCompletedEvent : IDomainEvent
{
    public Routine Routine { get; }
    public int DurationMinutes { get; }
    public DateTime OccurredOn { get; }

    public RoutineCompletedEvent(Routine routine, int durationMinutes)
    {
        Routine = routine;
        DurationMinutes = durationMinutes;
        OccurredOn = DateTime.UtcNow;
    }
}
```

### 5.3 Domain Layer Principles

1. **No Framework Dependencies**: Pure C# code only
2. **Rich Domain Models**: Entities contain business logic, not just data
3. **Validation in Domain**: Business rule validation in entity methods
4. **Immutability Where Appropriate**: Value objects are immutable
5. **Domain Events**: Communicate changes without tight coupling

---

## 6. Application Layer

### 6.1 Purpose

The Application layer contains business logic that doesn't fit naturally in domain entities, plus interfaces for infrastructure services.

### 6.2 Services

#### 6.2.1 Task Service

```csharp
namespace TaskScheduler.Application.Services;

public interface ITaskService
{
    Task<Result<TaskDto>> CreateTaskAsync(CreateTaskCommand command);
    Task<Result<TaskDto>> UpdateTaskAsync(int id, UpdateTaskCommand command);
    Task<Result> DeleteTaskAsync(int id);
    Task<Result> CompleteTaskAsync(int id);
    Task<Result> UncompleteTaskAsync(int id);
    Task<Result<TaskDto>> GetTaskByIdAsync(int id);
    Task<Result<List<TaskDto>>> GetTasksByCategoryAsync(int categoryId);
    Task<Result<List<TaskDto>>> GetTasksForDateAsync(DateTime date);
    Task<Result<List<TaskDto>>> GetOverdueTasksAsync();
    Task<Result<List<TaskDto>>> GetUpcomingTasksAsync(int daysAhead = 7);
    Task<Result> AddSubTaskAsync(int parentId, CreateTaskCommand subTaskCommand);
    Task<Result> BreakdownTaskIntoPomodoros(int taskId, int sessionDuration = 20);
}

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILogger<TaskService> _logger;

    public TaskService(
        ITaskRepository taskRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IEventAggregator eventAggregator,
        ILogger<TaskService> logger)
    {
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _eventAggregator = eventAggregator;
        _logger = logger;
    }

    public async Task<Result<TaskDto>> CreateTaskAsync(CreateTaskCommand command)
    {
        try
        {
            // Validate category exists
            var category = await _categoryRepository.GetByIdAsync(command.CategoryId);
            if (category == null)
                return Result<TaskDto>.Failure("Category not found");

            // Create domain entity
            var task = TaskItem.Create(
                command.Title,
                command.CategoryId,
                command.Description,
                command.DueDate,
                command.EstimatedDuration,
                command.Priority);

            // Set recurrence if specified
            if (command.RecurrencePattern != null)
                task.SetRecurrence(command.RecurrencePattern);

            // Save
            await _taskRepository.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();

            // Publish event
            await _eventAggregator.PublishAsync(new TaskCreatedEvent(task.Id));

            _logger.LogInformation("Created task {TaskId}: {Title}", task.Id, task.Title);

            return Result<TaskDto>.Success(MapToDto(task));
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Domain validation failed for task creation");
            return Result<TaskDto>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating task");
            return Result<TaskDto>.Failure("An error occurred while creating the task");
        }
    }

    public async Task<Result> CompleteTaskAsync(int id)
    {
        try
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return Result.Failure("Task not found");

            task.Complete();

            await _unitOfWork.SaveChangesAsync();

            // Publish domain events
            foreach (var domainEvent in task.DomainEvents)
            {
                await _eventAggregator.PublishAsync(domainEvent);
            }
            task.ClearDomainEvents();

            _logger.LogInformation("Completed task {TaskId}", id);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error completing task {TaskId}", id);
            return Result.Failure("An error occurred while completing the task");
        }
    }

    public async Task<Result> BreakdownTaskIntoPomodoros(int taskId, int sessionDuration = 20)
    {
        try
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                return Result.Failure("Task not found");

            if (!task.ShouldBreakdownIntoPomodoros())
                return Result.Failure("Task duration does not require breakdown");

            var pomodoroCount = task.GetSuggestedPomodoroCount(sessionDuration);

            // Create sub-tasks for each Pomodoro session
            for (int i = 1; i <= pomodoroCount; i++)
            {
                var subTask = TaskItem.Create(
                    $"{task.Title} - Part {i} of {pomodoroCount}",
                    task.CategoryId,
                    $"Session {i}",
                    task.DueDate,
                    sessionDuration,
                    task.Priority);

                task.AddSubTask(subTask);
            }

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation(
                "Broke down task {TaskId} into {Count} Pomodoro sessions",
                taskId,
                pomodoroCount);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error breaking down task {TaskId}", taskId);
            return Result.Failure("An error occurred while breaking down the task");
        }
    }

    private TaskDto MapToDto(TaskItem task)
    {
        // Mapping logic (or use AutoMapper)
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CategoryId = task.CategoryId,
            DueDate = task.DueDate,
            EstimatedDuration = task.EstimatedDuration,
            Priority = task.Priority,
            IsCompleted = task.IsCompleted,
            CompletedDate = task.CompletedDate,
            CompletionPercentage = task.GetCompletionPercentage(),
            SubTaskCount = task.SubTasks.Count,
            IsFromTeams = task.IsFromTeams
        };
    }
}
```

#### 6.2.2 Pomodoro Service

```csharp
namespace TaskScheduler.Application.Services;

public interface IPomodoroService
{
    Task<Result<PomodoroSessionDto>> StartSessionAsync(int taskId, int workDuration = 20);
    Task<Result> CompleteSessionAsync(int sessionId);
    Task<Result> PauseSessionAsync(int sessionId);
    Task<Result> ResumeSessionAsync(int sessionId);
    Task<Result<PomodoroSessionDto>> GetActiveSessionAsync();
    Task<Result<List<PomodoroSessionDto>>> GetSessionsForTaskAsync(int taskId);
    Task<Result<PomodoroStats>> GetStatsAsync(DateTime? startDate = null, DateTime? endDate = null);
}

public class PomodoroService : IPomodoroService
{
    private readonly IPomodoroSessionRepository _sessionRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventAggregator _eventAggregator;
    private readonly INotificationService _notificationService;
    private readonly ILogger<PomodoroService> _logger;

    public PomodoroService(
        IPomodoroSessionRepository sessionRepository,
        ITaskRepository taskRepository,
        IUnitOfWork unitOfWork,
        IEventAggregator eventAggregator,
        INotificationService notificationService,
        ILogger<PomodoroService> logger)
    {
        _sessionRepository = sessionRepository;
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
        _eventAggregator = eventAggregator;
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task<Result<PomodoroSessionDto>> StartSessionAsync(int taskId, int workDuration = 20)
    {
        try
        {
            // Check for active session
            var activeSession = await _sessionRepository.GetActiveSessionAsync();
            if (activeSession != null)
                return Result<PomodoroSessionDto>.Failure("Another session is already active");

            // Validate task
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                return Result<PomodoroSessionDto>.Failure("Task not found");

            if (task.IsCompleted)
                return Result<PomodoroSessionDto>.Failure("Cannot start session for completed task");

            // Create session
            var session = PomodoroSession.Start(taskId, workDuration);

            await _sessionRepository.AddAsync(session);
            await _unitOfWork.SaveChangesAsync();

            // Schedule notification for when work session ends
            await _notificationService.ScheduleNotificationAsync(
                "Work session complete!",
                "Time for a 5-minute break",
                DateTime.UtcNow.AddMinutes(workDuration));

            // Publish event
            await _eventAggregator.PublishAsync(new PomodoroSessionStartedEvent(session.Id, taskId));

            _logger.LogInformation(
                "Started Pomodoro session {SessionId} for task {TaskId}",
                session.Id,
                taskId);

            return Result<PomodoroSessionDto>.Success(MapToDto(session));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting Pomodoro session for task {TaskId}", taskId);
            return Result<PomodoroSessionDto>.Failure("An error occurred while starting the session");
        }
    }

    public async Task<Result> CompleteSessionAsync(int sessionId)
    {
        try
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
                return Result.Failure("Session not found");

            session.Complete();

            await _unitOfWork.SaveChangesAsync();

            // Publish domain events
            foreach (var domainEvent in session.DomainEvents)
            {
                await _eventAggregator.PublishAsync(domainEvent);
            }
            session.ClearDomainEvents();

            _logger.LogInformation("Completed Pomodoro session {SessionId}", sessionId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error completing session {SessionId}", sessionId);
            return Result.Failure("An error occurred while completing the session");
        }
    }

    private PomodoroSessionDto MapToDto(PomodoroSession session)
    {
        return new PomodoroSessionDto
        {
            Id = session.Id,
            TaskId = session.TaskId,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            WorkDuration = session.WorkDuration,
            BreakDuration = session.BreakDuration,
            WasCompleted = session.WasCompleted,
            ElapsedMinutes = session.GetElapsedMinutes()
        };
    }
}
```

### 6.3 Result Pattern

```csharp
namespace TaskScheduler.Application.Common.Models;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }

    protected Result(bool isSuccess, string? error)
    {
        if (isSuccess && error != null)
            throw new InvalidOperationException("Success result cannot have an error");
        if (!isSuccess && error == null)
            throw new InvalidOperationException("Failure result must have an error");

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);

    public static Result<T> Success<T>(T value) => new(value, true, null);
    public static Result<T> Failure<T>(string error) => new(default!, false, error);
}

public class Result<T> : Result
{
    public T Value { get; }

    protected internal Result(T value, bool isSuccess, string? error)
        : base(isSuccess, error)
    {
        Value = value;
    }
}
```

---

## 7. Infrastructure Layer

### 7.1 Data Access

#### 7.1.1 DbContext

```csharp
namespace TaskScheduler.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Routine> Routines { get; set; } = null!;
    public DbSet<RoutineStep> RoutineSteps { get; set; } = null!;
    public DbSet<PackItem> PackItems { get; set; } = null!;
    public DbSet<PomodoroSession> PomodoroSessions { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update ModifiedDate for all modified entities
        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Entity.UpdateModifiedDate();
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
```

#### 7.1.2 Entity Configuration

```csharp
namespace TaskScheduler.Infrastructure.Data.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(1000);

        builder.Property(t => t.Priority)
            .HasConversion<string>();

        // Value object: RecurrencePattern stored as JSON
        builder.OwnsOne(t => t.RecurrencePattern, rp =>
        {
            rp.Property(r => r.Type).HasConversion<string>();
            rp.Property(r => r.DaysOfWeek).HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<DayOfWeek>>(v, (JsonSerializerOptions?)null));
        });

        // Relationships
        builder.HasOne(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.ParentTask)
            .WithMany(t => t.SubTasks)
            .HasForeignKey(t => t.ParentTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(t => t.CategoryId);
        builder.HasIndex(t => t.DueDate);
        builder.HasIndex(t => t.IsCompleted);
        builder.HasIndex(t => t.TeamsAssignmentId);

        // Ignore domain events (not persisted)
        builder.Ignore(t => t.DomainEvents);
    }
}
```

#### 7.1.3 Repository Pattern

```csharp
namespace TaskScheduler.Application.Interfaces.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}

public interface ITaskRepository : IRepository<TaskItem>
{
    Task<List<TaskItem>> GetTasksByCategoryAsync(int categoryId);
    Task<List<TaskItem>> GetTasksForDateAsync(DateTime date);
    Task<List<TaskItem>> GetOverdueTasksAsync();
    Task<List<TaskItem>> GetUpcomingTasksAsync(DateTime startDate, DateTime endDate);
    Task<List<TaskItem>> GetTasksWithSubTasksAsync();
    Task<TaskItem?> GetTaskWithSubTasksByIdAsync(int id);
    Task<TaskItem?> GetByTeamsAssignmentIdAsync(string assignmentId);
}
```

```csharp
namespace TaskScheduler.Infrastructure.Data.Repositories;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
}

public class TaskRepository : BaseRepository<TaskItem>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<TaskItem>> GetTasksByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Where(t => t.CategoryId == categoryId && !t.ParentTaskId.HasValue)
            .Include(t => t.SubTasks)
            .Include(t => t.Category)
            .OrderBy(t => t.DueDate)
            .ToListAsync();
    }

    public async Task<List<TaskItem>> GetTasksForDateAsync(DateTime date)
    {
        var startOfDay = date.Date;
        var endOfDay = startOfDay.AddDays(1);

        return await _dbSet
            .Where(t => t.DueDate.HasValue &&
                       t.DueDate.Value >= startOfDay &&
                       t.DueDate.Value < endOfDay)
            .Include(t => t.SubTasks)
            .Include(t => t.Category)
            .OrderBy(t => t.DueDate)
            .ToListAsync();
    }

    public async Task<List<TaskItem>> GetOverdueTasksAsync()
    {
        var now = DateTime.UtcNow;

        return await _dbSet
            .Where(t => t.DueDate.HasValue &&
                       t.DueDate.Value < now &&
                       !t.IsCompleted)
            .Include(t => t.Category)
            .OrderBy(t => t.DueDate)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskWithSubTasksByIdAsync(int id)
    {
        return await _dbSet
            .Include(t => t.SubTasks)
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem?> GetByTeamsAssignmentIdAsync(string assignmentId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(t => t.TeamsAssignmentId == assignmentId);
    }
}
```

#### 7.1.4 Unit of Work Pattern

```csharp
namespace TaskScheduler.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ITaskRepository Tasks { get; }
    ICategoryRepository Categories { get; }
    IRoutineRepository Routines { get; }
    IPackItemRepository PackItems { get; }
    IPomodoroSessionRepository PomodoroSessions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
```

```csharp
namespace TaskScheduler.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    public ITaskRepository Tasks { get; }
    public ICategoryRepository Categories { get; }
    public IRoutineRepository Routines { get; }
    public IPackItemRepository PackItems { get; }
    public IPomodoroSessionRepository PomodoroSessions { get; }

    public UnitOfWork(
        ApplicationDbContext context,
        ITaskRepository taskRepository,
        ICategoryRepository categoryRepository,
        IRoutineRepository routineRepository,
        IPackItemRepository packItemRepository,
        IPomodoroSessionRepository pomodoroSessionRepository)
    {
        _context = context;
        Tasks = taskRepository;
        Categories = categoryRepository;
        Routines = routineRepository;
        PackItems = packItemRepository;
        PomodoroSessions = pomodoroSessionRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await SaveChangesAsync();
            await _transaction!.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
```

### 7.2 Platform Abstractions

```csharp
namespace TaskScheduler.Application.Interfaces.Services;

public interface INotificationService
{
    Task ScheduleNotificationAsync(string title, string body, DateTime scheduledTime);
    Task CancelNotificationAsync(int notificationId);
    Task CancelAllNotificationsAsync();
    Task ShowImmediateNotificationAsync(string title, string body);
}

public interface IFileService
{
    Task<string> ReadTextFileAsync(string filePath);
    Task WriteTextFileAsync(string filePath, string content);
    Task<bool> FileExistsAsync(string filePath);
    Task DeleteFileAsync(string filePath);
    Task<string> GetAppDataFolderAsync();
}

public interface ISecureStorageService
{
    Task SetAsync(string key, string value);
    Task<string?> GetAsync(string key);
    Task RemoveAsync(string key);
    Task ClearAllAsync();
}
```

#### Blazor Server Implementation (Phase 1)

```csharp
namespace TaskScheduler.Infrastructure.Services;

public class BrowserNotificationService : INotificationService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger<BrowserNotificationService> _logger;

    public BrowserNotificationService(
        IJSRuntime jsRuntime,
        ILogger<BrowserNotificationService> logger)
    {
        _jsRuntime = jsRuntime;
        _logger = logger;
    }

    public async Task ScheduleNotificationAsync(string title, string body, DateTime scheduledTime)
    {
        // For Blazor Server, use JavaScript interop for browser notifications
        var delayMs = (int)(scheduledTime - DateTime.UtcNow).TotalMilliseconds;

        if (delayMs > 0)
        {
            await _jsRuntime.InvokeVoidAsync("scheduleNotification", title, body, delayMs);
            _logger.LogInformation("Scheduled notification: {Title} at {Time}", title, scheduledTime);
        }
    }

    public async Task ShowImmediateNotificationAsync(string title, string body)
    {
        await _jsRuntime.InvokeVoidAsync("showNotification", title, body);
        _logger.LogInformation("Showed immediate notification: {Title}", title);
    }

    public Task CancelNotificationAsync(int notificationId)
    {
        // Browser notifications are fire-and-forget, cancellation not supported
        return Task.CompletedTask;
    }

    public Task CancelAllNotificationsAsync()
    {
        return Task.CompletedTask;
    }
}
```

#### MAUI Implementation (Phase 2)

```csharp
// This will be in TaskScheduler.MAUI project
public class MauiNotificationService : INotificationService
{
    public async Task ScheduleNotificationAsync(string title, string body, DateTime scheduledTime)
    {
        var request = new NotificationRequest
        {
            Title = title,
            Description = body,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = scheduledTime
            }
        };

        await LocalNotificationCenter.Current.Show(request);
    }

    // ... other implementations using MAUI native APIs
}
```

---

## 9. State Management Architecture

### 9.1 Custom Service-Based State Management

We'll use a **custom service-based state management** pattern with **event aggregator** for cross-component communication.

### 9.2 State Container Pattern

```csharp
namespace TaskScheduler.Web.State;

public interface IStateContainer<T>
{
    T State { get; }
    event EventHandler<T>? StateChanged;
    void UpdateState(T newState);
    void UpdateState(Action<T> updateAction);
}

public class StateContainer<T> : IStateContainer<T> where T : new()
{
    private T _state = new();

    public T State
    {
        get => _state;
        private set
        {
            _state = value;
            StateChanged?.Invoke(this, _state);
        }
    }

    public event EventHandler<T>? StateChanged;

    public void UpdateState(T newState)
    {
        State = newState;
    }

    public void UpdateState(Action<T> updateAction)
    {
        var newState = State;
        updateAction(newState);
        State = newState;
    }
}
```

### 9.3 Application State

```csharp
namespace TaskScheduler.Web.State;

public class AppState
{
    public TaskState Tasks { get; set; } = new();
    public PomodoroState Pomodoro { get; set; } = new();
    public RoutineState Routines { get; set; } = new();
    public CalendarState Calendar { get; set; } = new();
    public NotificationState Notifications { get; set; } = new();
}

public class TaskState
{
    public List<TaskDto> AllTasks { get; set; } = new();
    public List<TaskDto> TodayTasks { get; set; } = new();
    public TaskDto? SelectedTask { get; set; }
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
}

public class PomodoroState
{
    public PomodoroSessionDto? ActiveSession { get; set; }
    public bool IsRunning { get; set; }
    public bool IsBreak { get; set; }
    public int RemainingSeconds { get; set; }
    public int SessionNumber { get; set; }
    public int TotalSessions { get; set; }
}

public class RoutineState
{
    public List<RoutineDto> Routines { get; set; } = new();
    public RoutineDto? ActiveRoutine { get; set; }
    public int CurrentStepIndex { get; set; }
    public bool IsExecuting { get; set; }
}

public class CalendarState
{
    public DateTime SelectedDate { get; set; } = DateTime.Today;
    public CalendarViewType ViewType { get; set; } = CalendarViewType.Week;
    public List<int> VisibleCategoryIds { get; set; } = new();
    public bool ShowSchoolCalendar { get; set; } = true;
}
```

### 9.4 Event Aggregator Pattern

```csharp
namespace TaskScheduler.Application.Common.Interfaces;

public interface IEventAggregator
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : class;
    void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class;
    void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : class;
}
```

```csharp
namespace TaskScheduler.Web.State;

public class EventAggregator : IEventAggregator
{
    private readonly Dictionary<Type, List<Delegate>> _subscribers = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class
    {
        var eventType = typeof(TEvent);

        if (!_subscribers.ContainsKey(eventType))
            _subscribers[eventType] = new List<Delegate>();

        _subscribers[eventType].Add(handler);
    }

    public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : class
    {
        var eventType = typeof(TEvent);

        if (_subscribers.ContainsKey(eventType))
            _subscribers[eventType].Remove(handler);
    }

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class
    {
        var eventType = typeof(TEvent);

        if (!_subscribers.ContainsKey(eventType))
            return;

        await _semaphore.WaitAsync();
        try
        {
            var handlers = _subscribers[eventType].ToList();

            foreach (var handler in handlers)
            {
                if (handler is Action<TEvent> typedHandler)
                {
                    await Task.Run(() => typedHandler(@event));
                }
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}

// Application Events
public class TaskCreatedEvent
{
    public int TaskId { get; }
    public TaskCreatedEvent(int taskId) => TaskId = taskId;
}

public class TaskUpdatedEvent
{
    public int TaskId { get; }
    public TaskUpdatedEvent(int taskId) => TaskId = taskId;
}

public class PomodoroSessionStartedEvent
{
    public int SessionId { get; }
    public int TaskId { get; }

    public PomodoroSessionStartedEvent(int sessionId, int taskId)
    {
        SessionId = sessionId;
        TaskId = taskId;
    }
}

public class PomodoroTimerTickEvent
{
    public int RemainingSeconds { get; }
    public PomodoroTimerTickEvent(int remainingSeconds) => RemainingSeconds = remainingSeconds;
}
```

### 9.5 State Management Service Example

```csharp
namespace TaskScheduler.Web.State;

public interface ITaskStateService
{
    TaskState State { get; }
    event EventHandler? StateChanged;

    Task LoadTasksAsync();
    Task LoadTodayTasksAsync();
    Task SelectTaskAsync(int taskId);
    Task RefreshAsync();
}

public class TaskStateService : ITaskStateService
{
    private readonly ITaskService _taskService;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILogger<TaskStateService> _logger;

    public TaskState State { get; private set; } = new();
    public event EventHandler? StateChanged;

    public TaskStateService(
        ITaskService taskService,
        IEventAggregator eventAggregator,
        ILogger<TaskStateService> logger)
    {
        _taskService = taskService;
        _eventAggregator = eventAggregator;
        _logger = logger;

        // Subscribe to application events
        _eventAggregator.Subscribe<TaskCreatedEvent>(OnTaskCreated);
        _eventAggregator.Subscribe<TaskUpdatedEvent>(OnTaskUpdated);
        _eventAggregator.Subscribe<TaskCompletedEvent>(OnTaskCompleted);
    }

    public async Task LoadTasksAsync()
    {
        try
        {
            UpdateState(s => s.IsLoading = true);

            var result = await _taskService.GetAllTasksAsync();

            if (result.IsSuccess)
            {
                UpdateState(s =>
                {
                    s.AllTasks = result.Value;
                    s.ErrorMessage = null;
                });
            }
            else
            {
                UpdateState(s => s.ErrorMessage = result.Error);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading tasks");
            UpdateState(s => s.ErrorMessage = "Failed to load tasks");
        }
        finally
        {
            UpdateState(s => s.IsLoading = false);
        }
    }

    public async Task LoadTodayTasksAsync()
    {
        var result = await _taskService.GetTasksForDateAsync(DateTime.Today);

        if (result.IsSuccess)
        {
            UpdateState(s => s.TodayTasks = result.Value);
        }
    }

    public async Task SelectTaskAsync(int taskId)
    {
        var result = await _taskService.GetTaskByIdAsync(taskId);

        if (result.IsSuccess)
        {
            UpdateState(s => s.SelectedTask = result.Value);
        }
    }

    public async Task RefreshAsync()
    {
        await LoadTasksAsync();
        await LoadTodayTasksAsync();
    }

    private void OnTaskCreated(TaskCreatedEvent @event)
    {
        // Reload tasks when a new task is created
        _ = LoadTasksAsync();
    }

    private void OnTaskUpdated(TaskUpdatedEvent @event)
    {
        // Refresh the specific task
        _ = SelectTaskAsync(@event.TaskId);
    }

    private void OnTaskCompleted(TaskCompletedEvent @event)
    {
        // Update task in list
        UpdateState(s =>
        {
            var task = s.AllTasks.FirstOrDefault(t => t.Id == @event.Task.Id);
            if (task != null)
            {
                task.IsCompleted = true;
                task.CompletedDate = @event.Task.CompletedDate;
            }
        });
    }

    private void UpdateState(Action<TaskState> updateAction)
    {
        updateAction(State);
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}
```

---

## 10. Component Design Patterns

### 10.1 Smart vs. Presentational Components

#### Smart Components (Container Components)
- Manage state
- Call services
- Handle business logic
- Subscribe to state changes
- Examples: `TaskList.razor`, `CalendarPage.razor`

#### Presentational Components (Display Components)
- Receive data via parameters
- Emit events via EventCallback
- No direct service calls
- Pure UI logic only
- Examples: `TaskCard.razor`, `ProgressRing.razor`

### 10.2 Component Communication Patterns

```
┌──────────────────────────────────────────────────────────┐
│                    Communication Patterns                 │
└──────────────────────────────────────────────────────────┘

1. Parent → Child: Parameters
   [Parent] ──(Parameter)──> [Child]

2. Child → Parent: EventCallback
   [Child] ──(EventCallback)──> [Parent]

3. Sibling → Sibling: State Service
   [Component A] ──> [StateService] ──> [Component B]

4. Global Events: Event Aggregator
   [Any Component] ──> [EventAggregator] ──> [Any Subscriber]

5. Cascading Values: Cascading Parameters
   [Layout] ~~(CascadingValue)~~> [Nested Components]
```

### 10.3 Component Examples

#### Smart Component

```csharp
@page "/tasks"
@inject ITaskService TaskService
@inject ITaskStateService TaskState
@inject IEventAggregator EventAggregator
@implements IDisposable

<div class="tasks-page">
    <h1>Tasks</h1>

    @if (TaskState.State.IsLoading)
    {
        <LoadingSpinner />
    }
    else if (!string.IsNullOrEmpty(TaskState.State.ErrorMessage))
    {
        <ErrorMessage Message="@TaskState.State.ErrorMessage" />
    }
    else
    {
        <TaskList
            Tasks="@TaskState.State.AllTasks"
            OnTaskSelected="HandleTaskSelected"
            OnTaskCompleted="HandleTaskCompleted" />
    }
</div>

@code {
    protected override async Task OnInitializedAsync()
    {
        // Subscribe to state changes
        TaskState.StateChanged += OnStateChanged;

        // Load initial data
        await TaskState.LoadTasksAsync();
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        // UI will re-render when state changes
        InvokeAsync(StateHasChanged);
    }

    private async Task HandleTaskSelected(int taskId)
    {
        await TaskState.SelectTaskAsync(taskId);
        // Navigate or show detail
    }

    private async Task HandleTaskCompleted(int taskId)
    {
        var result = await TaskService.CompleteTaskAsync(taskId);

        if (result.IsSuccess)
        {
            // State service will handle updating UI via events
        }
    }

    public void Dispose()
    {
        TaskState.StateChanged -= OnStateChanged;
    }
}
```

#### Presentational Component

```csharp
@* TaskCard.razor - Pure presentational component *@

<div class="task-card @GetPriorityClass()" @onclick="OnCardClicked">
    <div class="task-header">
        <CategoryBadge Category="@Task.Category" />
        <span class="task-title">@Task.Title</span>
    </div>

    @if (!string.IsNullOrEmpty(Task.Description))
    {
        <p class="task-description">@Task.Description</p>
    }

    <div class="task-footer">
        @if (Task.DueDate.HasValue)
        {
            <span class="due-date">
                Due: @Task.DueDate.Value.ToString("MMM dd")
            </span>
        }

        @if (Task.SubTaskCount > 0)
        {
            <ProgressRing
                Percentage="@Task.CompletionPercentage"
                Size="24" />
        }

        <button
            class="complete-btn"
            @onclick="OnCompleteClicked"
            @onclick:stopPropagation="true">
            @(Task.IsCompleted ? "✓" : "○")
        </button>
    </div>
</div>

@code {
    [Parameter, EditorRequired]
    public TaskDto Task { get; set; } = null!;

    [Parameter]
    public EventCallback<int> OnSelected { get; set; }

    [Parameter]
    public EventCallback<int> OnCompleted { get; set; }

    private async Task OnCardClicked()
    {
        await OnSelected.InvokeAsync(Task.Id);
    }

    private async Task OnCompleteClicked()
    {
        await OnCompleted.InvokeAsync(Task.Id);
    }

    private string GetPriorityClass()
    {
        return Task.Priority switch
        {
            Priority.High => "priority-high",
            Priority.Medium => "priority-medium",
            Priority.Low => "priority-low",
            _ => ""
        };
    }
}
```

### 10.4 Cascading Parameters

```csharp
@* MainLayout.razor *@

<CascadingValue Value="@_theme">
    <CascadingValue Value="@_userSettings">
        <div class="app-container @_theme.Name">
            <NavMenu />
            <main>
                @Body
            </main>
        </div>
    </CascadingValue>
</CascadingValue>

@code {
    private Theme _theme = new();
    private UserSettings _userSettings = new();

    protected override async Task OnInitializedAsync()
    {
        _userSettings = await LoadUserSettings();
        _theme = GetThemeFromSettings(_userSettings);
    }
}

@* Any nested component can access cascading values *@
@code {
    [CascadingParameter]
    public Theme Theme { get; set; } = null!;

    [CascadingParameter]
    public UserSettings UserSettings { get; set; } = null!;
}
```

---

## 11. Dependency Injection Strategy

### 11.1 Service Lifetimes

```csharp
// Program.cs or Startup.cs

public void ConfigureServices(IServiceCollection services)
{
    // ═══════════════════════════════════════════════════════
    // SINGLETON - Created once, shared across entire app
    // ═══════════════════════════════════════════════════════

    // State containers (shared state)
    services.AddSingleton<ITaskStateService, TaskStateService>();
    services.AddSingleton<IPomodoroStateService, PomodoroStateService>();
    services.AddSingleton<IEventAggregator, EventAggregator>();

    // Platform-agnostic services (no state)
    services.AddSingleton<IDateTime, DateTimeService>();

    // ═══════════════════════════════════════════════════════
    // SCOPED - Created once per connection (Blazor Server)
    //          Created once per component tree (MAUI)
    // ═══════════════════════════════════════════════════════

    // Database context (per-request)
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite("Data Source=taskscheduler.db"),
        ServiceLifetime.Scoped);

    // Unit of Work (per-request)
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    // Repositories (per-request)
    services.AddScoped<ITaskRepository, TaskRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<IRoutineRepository, RoutineRepository>();
    services.AddScoped<IPackItemRepository, PackItemRepository>();
    services.AddScoped<IPomodoroSessionRepository, PomodoroSessionRepository>();

    // Application Services (per-request)
    services.AddScoped<ITaskService, TaskService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<IRoutineService, RoutineService>();
    services.AddScoped<IPomodoroService, PomodoroService>();
    services.AddScoped<IPackingListService, PackingListService>();
    services.AddScoped<IAnalyticsService, AnalyticsService>();

    // ═══════════════════════════════════════════════════════
    // TRANSIENT - Created every time requested
    // ═══════════════════════════════════════════════════════

    // Lightweight services with no state
    services.AddTransient<IRecurrenceService, RecurrenceService>();

    // ═══════════════════════════════════════════════════════
    // PLATFORM-SPECIFIC SERVICES
    // ═══════════════════════════════════════════════════════

    #if BLAZOR_SERVER
    services.AddScoped<INotificationService, BrowserNotificationService>();
    services.AddScoped<IFileService, ServerFileService>();
    services.AddScoped<ISecureStorageService, ServerSecureStorageService>();
    #elif MAUI
    services.AddSingleton<INotificationService, MauiNotificationService>();
    services.AddSingleton<IFileService, MauiFileService>();
    services.AddSingleton<ISecureStorageService, MauiSecureStorageService>();
    #endif

    // ═══════════════════════════════════════════════════════
    // EXTERNAL SERVICES
    // ═══════════════════════════════════════════════════════

    services.AddScoped<ITeamsIntegrationService, TeamsIntegrationService>();

    // Microsoft Graph SDK
    services.AddScoped<GraphServiceClient>(sp =>
    {
        // Configuration for Graph API
        var authProvider = sp.GetRequiredService<IAuthenticationProvider>();
        return new GraphServiceClient(authProvider);
    });

    // ═══════════════════════════════════════════════════════
    // LOGGING & MONITORING
    // ═══════════════════════════════════════════════════════

    services.AddLogging(builder =>
    {
        builder.AddConsole();
        builder.AddDebug();
    });
}
```

### 11.2 Service Registration Extension

```csharp
namespace TaskScheduler.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IRoutineRepository, RoutineRepository>();
        services.AddScoped<IPackItemRepository, PackItemRepository>();
        services.AddScoped<IPomodoroSessionRepository, PomodoroSessionRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Infrastructure Services
        services.AddSingleton<IDateTime, DateTimeService>();

        return services;
    }

    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Application Services
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IRoutineService, RoutineService>();
        services.AddScoped<IPomodoroService, PomodoroService>();
        services.AddScoped<IPackingListService, PackingListService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();
        services.AddTransient<IRecurrenceService, RecurrenceService>();

        // AutoMapper
        services.AddAutoMapper(typeof(AutoMapperProfile));

        return services;
    }

    public static IServiceCollection AddWebServices(
        this IServiceCollection services)
    {
        // State Management
        services.AddSingleton<IEventAggregator, EventAggregator>();
        services.AddSingleton<ITaskStateService, TaskStateService>();
        services.AddSingleton<IPomodoroStateService, PomodoroStateService>();
        services.AddSingleton<IRoutineStateService, RoutineStateService>();

        // Blazor Server platform services
        services.AddScoped<INotificationService, BrowserNotificationService>();
        services.AddScoped<IFileService, ServerFileService>();
        services.AddScoped<ISecureStorageService, ServerSecureStorageService>();

        return services;
    }
}

// Usage in Program.cs:
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddWebServices();
```

---

## 12. Testing Strategy

### 12.1 Test Pyramid

```
         ┌─────────────┐
         │     E2E     │  ← Few, slow, expensive
         │   (Future)  │
         ├─────────────┤
         │ Integration │  ← Some, moderate
         │    Tests    │
         ├─────────────┤
         │ Component   │  ← More, faster
         │   Tests     │     (bUnit)
         ├─────────────┤
         │    Unit     │  ← Most, fastest, cheapest
         │   Tests     │     (xUnit)
         └─────────────┘
```

### 12.2 Unit Testing (Domain & Application Layer)

#### Domain Entity Tests

```csharp
namespace TaskScheduler.Domain.Tests.Entities;

public class TaskItemTests
{
    [Fact]
    public void Create_ValidTask_ShouldSucceed()
    {
        // Arrange
        var title = "Complete homework";
        var categoryId = 1;

        // Act
        var task = TaskItem.Create(title, categoryId);

        // Assert
        task.Should().NotBeNull();
        task.Title.Should().Be(title);
        task.CategoryId.Should().Be(categoryId);
        task.IsCompleted.Should().BeFalse();
        task.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Create_EmptyTitle_ShouldThrowDomainException()
    {
        // Arrange
        var title = "";
        var categoryId = 1;

        // Act
        var act = () => TaskItem.Create(title, categoryId);

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Task title cannot be empty");
    }

    [Fact]
    public void Complete_UncompletedTask_ShouldSetCompletedProperties()
    {
        // Arrange
        var task = TaskItem.Create("Test task", 1);

        // Act
        task.Complete();

        // Assert
        task.IsCompleted.Should().BeTrue();
        task.CompletedDate.Should().NotBeNull();
        task.CompletedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        task.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<TaskCompletedEvent>();
    }

    [Fact]
    public void AddSubTask_ValidSubTask_ShouldAddToCollection()
    {
        // Arrange
        var parentTask = TaskItem.Create("Parent task", 1);
        var subTask = TaskItem.Create("Sub task", 1);

        // Act
        parentTask.AddSubTask(subTask);

        // Assert
        parentTask.SubTasks.Should().ContainSingle()
            .Which.Should().Be(subTask);
        subTask.ParentTaskId.Should().Be(parentTask.Id);
    }

    [Fact]
    public void AddSubTask_ToSubTask_ShouldThrowDomainException()
    {
        // Arrange
        var parentTask = TaskItem.Create("Parent", 1);
        var subTask = TaskItem.Create("SubTask", 1);
        var nestedSubTask = TaskItem.Create("Nested", 1);

        parentTask.AddSubTask(subTask);

        // Act
        var act = () => subTask.AddSubTask(nestedSubTask);

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Cannot add subtasks to a subtask");
    }

    [Theory]
    [InlineData(20, 1)]  // 20 minutes = 1 Pomodoro
    [InlineData(40, 2)]  // 40 minutes = 2 Pomodoros
    [InlineData(45, 3)]  // 45 minutes = 3 Pomodoros (rounds up)
    [InlineData(60, 3)]  // 60 minutes = 3 Pomodoros
    public void GetSuggestedPomodoroCount_VariousDurations_ShouldCalculateCorrectly(
        int estimatedDuration,
        int expectedCount)
    {
        // Arrange
        var task = TaskItem.Create("Task", 1, estimatedDuration: estimatedDuration);

        // Act
        var count = task.GetSuggestedPomodoroCount(20);

        // Assert
        count.Should().Be(expectedCount);
    }
}
```

#### Application Service Tests

```csharp
namespace TaskScheduler.Application.Tests.Services;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IEventAggregator> _eventAggregatorMock;
    private readonly Mock<ILogger<TaskService>> _loggerMock;
    private readonly TaskService _sut;  // System Under Test

    public TaskServiceTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _eventAggregatorMock = new Mock<IEventAggregator>();
        _loggerMock = new Mock<ILogger<TaskService>>();

        _sut = new TaskService(
            _taskRepositoryMock.Object,
            _categoryRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _eventAggregatorMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateTaskAsync_ValidCommand_ShouldReturnSuccessResult()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Homework" };
        _categoryRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(category);

        var command = new CreateTaskCommand
        {
            Title = "Math homework",
            CategoryId = 1,
            EstimatedDuration = 40
        };

        // Act
        var result = await _sut.CreateTaskAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Title.Should().Be("Math homework");

        _taskRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<TaskItem>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(default),
            Times.Once);

        _eventAggregatorMock.Verify(
            x => x.PublishAsync(It.IsAny<TaskCreatedEvent>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateTaskAsync_CategoryNotFound_ShouldReturnFailureResult()
    {
        // Arrange
        _categoryRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Category?)null);

        var command = new CreateTaskCommand
        {
            Title = "Task",
            CategoryId = 999
        };

        // Act
        var result = await _sut.CreateTaskAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Category not found");

        _taskRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<TaskItem>()),
            Times.Never);
    }

    [Fact]
    public async Task CompleteTaskAsync_ExistingTask_ShouldCompleteAndPublishEvent()
    {
        // Arrange
        var task = TaskItem.Create("Task", 1);
        _taskRepositoryMock
            .Setup(x => x.GetByIdAsync(task.Id))
            .ReturnsAsync(task);

        // Act
        var result = await _sut.CompleteTaskAsync(task.Id);

        // Assert
        result.IsSuccess.Should().BeTrue();
        task.IsCompleted.Should().BeTrue();

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(default),
            Times.Once);

        _eventAggregatorMock.Verify(
            x => x.PublishAsync(It.IsAny<TaskCompletedEvent>()),
            Times.Once);
    }

    [Fact]
    public async Task BreakdownTaskIntoPomodoros_TaskWith40Minutes_ShouldCreate2SubTasks()
    {
        // Arrange
        var task = TaskItem.Create("Long task", 1, estimatedDuration: 40);
        _taskRepositoryMock
            .Setup(x => x.GetByIdAsync(task.Id))
            .ReturnsAsync(task);

        // Act
        var result = await _sut.BreakdownTaskIntoPomodoros(task.Id, 20);

        // Assert
        result.IsSuccess.Should().BeTrue();
        task.SubTasks.Should().HaveCount(2);
        task.SubTasks.First().Title.Should().Contain("Part 1 of 2");
        task.SubTasks.Last().Title.Should().Contain("Part 2 of 2");

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(default),
            Times.Once);
    }
}
```

### 12.3 Component Testing with bUnit

```csharp
namespace TaskScheduler.Web.Tests.Components;

public class TaskCardTests : TestContext
{
    [Fact]
    public void TaskCard_RendersTaskTitle()
    {
        // Arrange
        var task = new TaskDto
        {
            Id = 1,
            Title = "Test Task",
            CategoryId = 1,
            Priority = Priority.High
        };

        // Act
        var cut = RenderComponent<TaskCard>(parameters => parameters
            .Add(p => p.Task, task));

        // Assert
        cut.Find(".task-title").TextContent.Should().Be("Test Task");
    }

    [Fact]
    public void TaskCard_Clicked_ShouldInvokeOnSelected()
    {
        // Arrange
        var task = new TaskDto { Id = 1, Title = "Test" };
        var selectedTaskId = 0;

        var cut = RenderComponent<TaskCard>(parameters => parameters
            .Add(p => p.Task, task)
            .Add(p => p.OnSelected, EventCallback.Factory.Create<int>(
                this,
                id => selectedTaskId = id)));

        // Act
        cut.Find(".task-card").Click();

        // Assert
        selectedTaskId.Should().Be(1);
    }

    [Fact]
    public void TaskCard_CompleteButtonClicked_ShouldInvokeOnCompleted()
    {
        // Arrange
        var task = new TaskDto { Id = 1, Title = "Test", IsCompleted = false };
        var completedTaskId = 0;

        var cut = RenderComponent<TaskCard>(parameters => parameters
            .Add(p => p.Task, task)
            .Add(p => p.OnCompleted, EventCallback.Factory.Create<int>(
                this,
                id => completedTaskId = id)));

        // Act
        cut.Find(".complete-btn").Click();

        // Assert
        completedTaskId.Should().Be(1);
    }

    [Fact]
    public void TaskCard_WithDueDate_ShouldDisplayDueDate()
    {
        // Arrange
        var dueDate = new DateTime(2025, 11, 15);
        var task = new TaskDto
        {
            Id = 1,
            Title = "Test",
            DueDate = dueDate
        };

        // Act
        var cut = RenderComponent<TaskCard>(parameters => parameters
            .Add(p => p.Task, task));

        // Assert
        var dueDateElement = cut.Find(".due-date");
        dueDateElement.TextContent.Should().Contain("Nov 15");
    }

    [Fact]
    public void TaskCard_WithSubTasks_ShouldShowProgressRing()
    {
        // Arrange
        var task = new TaskDto
        {
            Id = 1,
            Title = "Test",
            SubTaskCount = 5,
            CompletionPercentage = 60
        };

        // Act
        var cut = RenderComponent<TaskCard>(parameters => parameters
            .Add(p => p.Task, task));

        // Assert
        var progressRing = cut.FindComponent<ProgressRing>();
        progressRing.Instance.Percentage.Should().Be(60);
    }
}
```

#### PomodoroTimer Component Test

```csharp
public class PomodoroTimerTests : TestContext
{
    [Fact]
    public void PomodoroTimer_InitialRender_ShowsCorrectDuration()
    {
        // Arrange
        var session = new PomodoroSessionDto
        {
            Id = 1,
            TaskId = 1,
            WorkDuration = 20,
            StartTime = DateTime.UtcNow
        };

        // Act
        var cut = RenderComponent<PomodoroTimer>(parameters => parameters
            .Add(p => p.Session, session));

        // Assert
        cut.Find(".timer-display").TextContent.Should().Contain("20:00");
    }

    [Fact]
    public async Task PomodoroTimer_PauseButton_ShouldPauseTimer()
    {
        // Arrange
        var pomodoroServiceMock = new Mock<IPomodoroService>();
        Services.AddSingleton(pomodoroServiceMock.Object);

        var session = new PomodoroSessionDto
        {
            Id = 1,
            WorkDuration = 20
        };

        var cut = RenderComponent<PomodoroTimer>(parameters => parameters
            .Add(p => p.Session, session));

        // Act
        var pauseButton = cut.Find(".pause-btn");
        await pauseButton.ClickAsync(new MouseEventArgs());

        // Assert
        pomodoroServiceMock.Verify(
            x => x.PauseSessionAsync(1),
            Times.Once);
    }
}
```

### 12.4 Integration Tests (Repository Layer)

```csharp
namespace TaskScheduler.Infrastructure.Tests.Repositories;

public class TaskRepositoryIntegrationTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly TaskRepository _repository;

    public TaskRepositoryIntegrationTests()
    {
        // Use in-memory database for testing
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new TaskRepository(_context);

        SeedDatabase();
    }

    [Fact]
    public async Task GetTasksByCategoryAsync_ExistingCategory_ShouldReturnTasks()
    {
        // Arrange
        var categoryId = 1;

        // Act
        var tasks = await _repository.GetTasksByCategoryAsync(categoryId);

        // Assert
        tasks.Should().NotBeEmpty();
        tasks.Should().AllSatisfy(t => t.CategoryId.Should().Be(categoryId));
    }

    [Fact]
    public async Task GetOverdueTasksAsync_ShouldReturnOnlyOverdueTasks()
    {
        // Act
        var overdueTasks = await _repository.GetOverdueTasksAsync();

        // Assert
        overdueTasks.Should().NotBeEmpty();
        overdueTasks.Should().AllSatisfy(t =>
        {
            t.DueDate.Should().NotBeNull();
            t.DueDate.Should().BeBefore(DateTime.UtcNow);
            t.IsCompleted.Should().BeFalse();
        });
    }

    private void SeedDatabase()
    {
        var category = new Category { Id = 1, Name = "Homework" };
        _context.Categories.Add(category);

        _context.Tasks.AddRange(
            TaskItem.Create("Task 1", 1, dueDate: DateTime.UtcNow.AddDays(1)),
            TaskItem.Create("Task 2", 1, dueDate: DateTime.UtcNow.AddDays(-1)),
            TaskItem.Create("Task 3", 1, dueDate: DateTime.UtcNow.AddDays(-2)));

        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

### 12.5 Test Coverage Goals

- **Domain Layer**: 90%+ coverage
- **Application Layer**: 80%+ coverage
- **Infrastructure Layer**: 70%+ coverage (focus on complex logic, not EF plumbing)
- **Presentation Layer**: 60%+ coverage (complex components only)

---

## 14. MAUI Migration Path

### 14.1 What Stays the Same (90%+)

✅ **Domain Layer** - Zero changes
✅ **Application Layer** - Zero changes
✅ **Infrastructure Data Layer** - Zero changes (SQLite, repositories)
✅ **Razor Components** - 95% stay the same
✅ **State Management** - Same services and patterns
✅ **Business Logic** - All services unchanged

### 14.2 What Changes

#### Hosting Model

**Blazor Server (Phase 1):**
```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddWebServices();

var app = builder.Build();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
```

**MAUI Hybrid (Phase 2):**
```csharp
// MauiProgram.cs
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Add Blazor Web View
        builder.Services.AddMauiBlazorWebView();

        #if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        #endif

        // Same service registration!
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddMauiServices();  // Platform-specific

        return builder.Build();
    }
}
```

#### Platform Services

Replace browser-based services with native MAUI services:

```csharp
// Phase 1: Blazor Server
services.AddScoped<INotificationService, BrowserNotificationService>();

// Phase 2: MAUI
services.AddSingleton<INotificationService, MauiNotificationService>();
```

Implementation:
```csharp
public class MauiNotificationService : INotificationService
{
    public async Task ScheduleNotificationAsync(
        string title,
        string body,
        DateTime scheduledTime)
    {
        var request = new NotificationRequest
        {
            NotificationId = Random.Shared.Next(),
            Title = title,
            Description = body,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = scheduledTime
            }
        };

        await LocalNotificationCenter.Current.Show(request);
    }
}
```

### 14.3 Migration Checklist

#### Phase 1 → Phase 2 Migration Steps

1. **Create MAUI Project**
   ```bash
   dotnet new maui -n TaskScheduler.Maui
   ```

2. **Move Shared Components**
   - Move all `Components/` to new `TaskScheduler.Shared` project
   - Reference from both Web and MAUI projects

3. **Update Service Registration**
   - Create `AddMauiServices()` extension method
   - Replace platform-specific services
   - Keep all other services identical

4. **Configure MAUI-Specific Features**
   - Add native notification permissions
   - Configure file system paths
   - Set up secure storage

5. **Test on All Platforms**
   - Windows desktop
   - macOS desktop
   - Android device/emulator

6. **Migrate Data**
   - Same SQLite database works on all platforms
   - Copy database file on first run (if needed)

### 14.4 Code Sharing Strategy

```
┌─────────────────────────────────────────────────────────┐
│                   Shared Projects                        │
│  ┌────────────────┐  ┌────────────────┐                │
│  │    Domain      │  │  Application   │                │
│  │   (100%)       │  │    (100%)      │                │
│  └────────────────┘  └────────────────┘                │
│                                                         │
│  ┌────────────────┐  ┌────────────────┐                │
│  │Infrastructure  │  │ Razor Shared   │                │
│  │  Data (100%)   │  │  Components    │                │
│  └────────────────┘  │    (95%)       │                │
│                      └────────────────┘                │
└─────────────────────────────────────────────────────────┘
                          ↑
         ┌────────────────┼────────────────┐
         │                                  │
┌────────▼─────────┐            ┌──────────▼──────────┐
│  Blazor Server   │            │    MAUI Hybrid      │
│   (Phase 1)      │            │    (Phase 2)        │
│                  │            │                     │
│ - Server hosting │            │ - Native hosting    │
│ - Browser notif  │            │ - Native notif      │
│ - Server storage │            │ - Device storage    │
└──────────────────┘            └─────────────────────┘
```

---

## 15. Design Patterns Catalog

### 15.1 Repository Pattern
**Purpose**: Abstract data access logic
**Location**: Infrastructure layer
**Example**: `TaskRepository`, `CategoryRepository`

### 15.2 Unit of Work Pattern
**Purpose**: Manage transactions across multiple repositories
**Location**: Infrastructure layer
**Example**: `UnitOfWork` coordinating `SaveChangesAsync()`

### 15.3 Factory Pattern
**Purpose**: Encapsulate object creation logic
**Location**: Domain entities
**Example**: `TaskItem.Create()` static factory method

### 15.4 Observer Pattern (Event Aggregator)
**Purpose**: Loosely coupled communication between components
**Location**: Application/Presentation
**Example**: `EventAggregator` publishing `TaskCreatedEvent`

### 15.5 Strategy Pattern
**Purpose**: Platform-specific implementations
**Location**: Infrastructure services
**Example**: `INotificationService` with `BrowserNotificationService` vs `MauiNotificationService`

### 15.6 Result Pattern
**Purpose**: Explicit success/failure handling without exceptions
**Location**: Application services
**Example**: `Result<TaskDto>` returned from service methods

### 15.7 State Container Pattern
**Purpose**: Manage shared UI state
**Location**: Presentation layer
**Example**: `TaskStateService` managing task list state

### 15.8 CQRS Lite
**Purpose**: Separate read and write operations
**Location**: Application layer (Commands vs Queries)
**Example**: `CreateTaskCommand` vs `GetTaskQuery` (via service methods)

---

## 17. Best Practices & Guidelines

### 17.1 Code Organization

✅ **DO:**
- Keep domain entities rich with behavior
- Use interfaces for all cross-layer dependencies
- Follow consistent naming conventions
- Group related files in folders

❌ **DON'T:**
- Put business logic in Blazor components
- Reference Infrastructure from Domain
- Use static classes for services
- Mix concerns in a single class

### 17.2 Component Best Practices

✅ **DO:**
- Keep components focused (single responsibility)
- Use parameters for input, EventCallback for output
- Dispose of subscriptions in `IDisposable.Dispose()`
- Use `@key` directive for list items
- Call `StateHasChanged()` after async state changes

❌ **DON'T:**
- Call services directly from presentational components
- Forget to unsubscribe from events
- Perform heavy operations in `OnInitialized`
- Mutate parameter values

### 17.3 State Management Best Practices

✅ **DO:**
- Centralize state in state services
- Use immutable state updates where possible
- Publish events for cross-component communication
- Keep state serializable

❌ **DON'T:**
- Share mutable state directly between components
- Put UI-specific logic in state services
- Forget to notify state changes

### 17.4 Dependency Injection Best Practices

✅ **DO:**
- Use constructor injection
- Inject interfaces, not implementations
- Choose appropriate lifetimes
- Register services in extension methods

❌ **DON'T:**
- Use service locator pattern
- Create services with `new` keyword
- Inject DbContext as Singleton

### 17.5 Testing Best Practices

✅ **DO:**
- Follow Arrange-Act-Assert pattern
- Use meaningful test names
- Test behavior, not implementation
- Mock external dependencies
- Use FluentAssertions for readable assertions

❌ **DON'T:**
- Test framework code (EF, Blazor runtime)
- Write tests that depend on test order
- Mock everything (test real objects when simple)
- Ignore failing tests

### 17.6 Performance Best Practices

✅ **DO:**
- Use `@key` for list rendering
- Implement `ShouldRender()` for expensive components
- Use virtualization for long lists
- Index database queries appropriately
- Use `AsNoTracking()` for read-only queries

❌ **DON'T:**
- Render thousands of items without virtualization
- Perform synchronous I/O in UI thread
- Load entire database into memory
- Create N+1 query problems

---

## Summary

This technical design provides a **clean, testable, and MAUI-ready architecture** for the TaskScheduler application:

### Key Architectural Decisions:

1. **Clean Architecture** with 4 distinct layers
2. **Custom service-based state management** with event aggregator
3. **Repository + Unit of Work** for data access
4. **Platform abstraction interfaces** for MAUI migration
5. **Smart/Presentational component** separation
6. **Comprehensive testing** with xUnit + bUnit
7. **Domain-driven design** with rich entities

### Migration Benefits:

- **90%+ code reuse** when moving to MAUI
- **Zero changes** to business logic
- **Minimal refactoring** of UI components
- **Platform services** swapped via DI

### Development Workflow:

```
1. Start with Blazor Server (rapid prototyping)
2. Build features incrementally
3. Test each layer independently
4. When ready: Create MAUI project
5. Move shared components to .Shared project
6. Replace platform services
7. Test on all platforms
```

This architecture ensures the codebase is **maintainable, testable, and ready for cross-platform deployment** from day one.

---

**Next Steps:**

Would you like me to:
1. Create the actual project structure with all folders and initial files?
2. Implement starter code for key classes (entities, services, components)?
3. Set up the database with Entity Framework migrations?
4. Create sample unit tests and component tests?
5. Generate a development roadmap/sprint plan?
