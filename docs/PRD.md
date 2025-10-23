# Product Requirements Document (PRD)

## TaskScheduler - Neurodiverse-Friendly Task Management Application

**Version:** 1.0
**Date:** October 23, 2025
**Status:** Draft - MVP Planning
**Author:** Product Team

---

## Table of Contents

1. [Executive Summary](#1-executive-summary)
2. [Vision & Goals](#2-vision--goals)
3. [User Personas & Problem Statement](#3-user-personas--problem-statement)
4. [Success Metrics](#4-success-metrics)
5. [Feature Requirements](#5-feature-requirements)
6. [User Stories & Use Cases](#6-user-stories--use-cases)
7. [Technical Architecture](#7-technical-architecture)
8. [UI/UX Requirements](#8-uiux-requirements)
9. [Non-Functional Requirements](#9-non-functional-requirements)
10. [Release Phases & Timeline](#10-release-phases--timeline)
11. [Dependencies & Risks](#11-dependencies--risks)
12. [Open Questions](#12-open-questions)

---

## 1. Executive Summary

### 1.1 Product Overview

TaskScheduler is a cross-platform task management application specifically designed to support neurodiverse teenagers in managing their daily responsibilities, including homework, chores, music practice, and routines. The application prioritizes cognitive accessibility, visual clarity, and executive function support.

### 1.2 Target Platform

- **Primary Platform:** Blazor Server (web-based MVP)
- **Future Platforms:** .NET MAUI hybrid app for Windows, macOS, and Android

### 1.3 Key Differentiators

1. **Neuro-inclusive design** optimized for ADHD, autism, and executive function challenges
2. **Microsoft Teams for Education integration** for automatic homework import
3. **Built-in Pomodoro timer** with 20-minute default focus sessions
4. **School preparation system** with packing reminders and visual checklists
5. **Automatic task breakdown** into manageable, focus-appropriate chunks

### 1.4 Primary User

Neurodiverse teenagers (ages 13-18) with executive function challenges, specifically requiring support for:
- Time blindness
- Task initiation difficulties
- Working memory limitations
- Focus maintenance (20-minute concentration span)
- Transition challenges
- Organization and planning

---

## 2. Vision & Goals

### 2.1 Product Vision

To create an accessible, empowering task management system that reduces anxiety, builds independence, and supports neurodiverse students in successfully managing their academic and personal responsibilities without overwhelming them.

### 2.2 Product Goals

#### Primary Goals
1. **Reduce forgotten homework and materials** by 80%+ through integrated reminders and packing systems
2. **Improve task completion rates** through appropriate task breakdown and Pomodoro support
3. **Decrease anxiety** around school preparation and time management
4. **Build independence** through visual scaffolding that can be gradually reduced
5. **Support executive function** without adding cognitive load

#### Secondary Goals
1. Enable parent/guardian monitoring without being intrusive
2. Integrate seamlessly with existing school systems (Microsoft Teams)
3. Provide cross-platform access for home and mobile use
4. Create a sustainable, maintainable codebase for long-term support

---

## 3. User Personas & Problem Statement

### 3.1 Primary Persona: Alex (The Student)

**Demographics:**
- Age: 14-16 years old
- Neurodiverse (ADHD/Autism spectrum/Executive function challenges)
- Uses Microsoft Teams for school
- Concentration span: ~20 minutes
- Memory challenges: Working memory, prospective memory

**Current Pain Points:**
- Forgets homework assignments and due dates
- Loses track of what materials to bring to school
- Gets overwhelmed by large tasks
- Struggles to initiate tasks
- Experiences time blindness
- Difficulty transitioning between activities
- Forgets to pack completed homework
- Anxiety about forgetting important items (PE kit, instrument, etc.)
- Morning routines take too long or are forgotten
- Bedtime routines are inconsistent

**Needs:**
- Clear visual representation of tasks and time
- Automatic breakdown of large tasks into smaller chunks
- Reminders at the right time (not too early, not too late)
- Simple, non-overwhelming interface
- Integration with school homework system
- Help remembering what to pack each day

### 3.2 Secondary Persona: Parent/Guardian

**Demographics:**
- Parent of neurodiverse teenager
- Limited time for task oversight
- Wants to support independence, not create dependency

**Needs:**
- Visibility into homework completion
- Assurance that important tasks aren't forgotten
- Ability to help without micromanaging
- Reduce daily conflicts about homework and preparation

### 3.3 Problem Statement

> Neurodiverse teenagers struggle to manage the complex web of homework assignments, school preparation, and daily routines due to executive function challenges. Existing task management tools are not designed for their cognitive needs, leading to forgotten assignments, missing materials, incomplete tasks, and significant anxiety for both students and parents. There is a need for a specialized tool that integrates with educational platforms, provides appropriate scaffolding, and supports focus limitations while building independence.

---

## 4. Success Metrics

### 4.1 Primary Metrics (MVP)

| Metric | Target | Measurement Method |
|--------|--------|-------------------|
| Task Completion Rate | >75% | Completed tasks / Total tasks |
| Homework Submission Rate | >90% | From parent/teacher feedback |
| Forgotten Items Per Week | <2 | User-reported or parent-reported |
| Daily Active Usage | >80% of school days | Login/interaction tracking |
| Pomodoro Sessions Completed | Average 4+ per day | Session completion logs |
| User Satisfaction | >4/5 stars | User/parent survey |

### 4.2 Secondary Metrics

| Metric | Target | Measurement Method |
|--------|--------|-------------------|
| Morning Routine Completion | >85% | Routine checklist completion |
| Bedtime Routine Completion | >80% | Routine checklist completion |
| Average Task Breakdown Used | >60% of tasks | Task with sub-tasks / Total tasks |
| Notification Response Rate | >70% | Action taken within 15 min of notification |
| Teams Sync Success Rate | >95% | Successful syncs / Total sync attempts |

### 4.3 Engagement Metrics

- Average session duration
- Tasks created per week
- Categories customized
- Routines completed
- Feature usage distribution

---

## 5. Feature Requirements

### 5.1 Phase 1: MVP Features (Must Have)

#### 5.1.1 Task Management Core

**Priority:** P0 (Critical)

**Requirements:**
- **FR-1.1:** Users can create tasks with the following properties:
  - Title (required, max 200 characters)
  - Description (optional, max 1000 characters)
  - Category (selected from user-defined list)
  - Due date and time (optional)
  - Estimated duration (in minutes, default suggestions: 20, 40, 60)
  - Priority level (High, Medium, Low with visual indicators)
  - Visual icon (auto-selected based on category, customizable)

- **FR-1.2:** Users can edit any task property at any time

- **FR-1.3:** Users can delete tasks with confirmation dialog

- **FR-1.4:** Users can mark tasks as complete with satisfying visual feedback

- **FR-1.5:** Completed tasks are moved to a separate "Completed" view but remain accessible

**Acceptance Criteria:**
- Task creation form loads in <1 second
- All fields save correctly to local database
- Validation errors are clearly displayed
- Task appears in all relevant views immediately after creation

---

#### 5.1.2 Recurring Tasks

**Priority:** P0 (Critical)

**Requirements:**
- **FR-2.1:** Users can set tasks to recur on the following patterns:
  - Daily (every X days)
  - Weekly (specific days of week: Mon, Tue, Wed, etc.)
  - Monthly (specific day of month)
  - Custom (advanced pattern builder)

- **FR-2.2:** Recurring tasks automatically generate new instances based on pattern

- **FR-2.3:** Users can edit individual instances without affecting the series

- **FR-2.4:** Users can edit the entire recurring series

- **FR-2.5:** Users can delete individual instances or the entire series

**Acceptance Criteria:**
- Recurring tasks generate correctly for next 30 days
- Editing one instance doesn't break recurrence
- Clear indication in UI that task is recurring
- Option to "complete all future instances" for ended activities

---

#### 5.1.3 Sub-Tasks & Checklists

**Priority:** P0 (Critical)

**Requirements:**
- **FR-3.1:** Users can add unlimited sub-tasks to any main task

- **FR-3.2:** Each sub-task has:
  - Title (required)
  - Completion checkbox
  - Optional estimated duration
  - Optional order/sequence number

- **FR-3.3:** Main task shows progress indicator based on sub-task completion (e.g., "3/5 completed")

- **FR-3.4:** Sub-tasks can be reordered via drag-and-drop or arrows

- **FR-3.5:** Completing all sub-tasks prompts to complete main task

- **FR-3.6:** Sub-tasks display in checklist format within task detail view

**Acceptance Criteria:**
- Progress bar updates immediately when sub-task checked
- Sub-tasks can be added/removed without data loss
- Clear visual hierarchy (main task vs. sub-tasks)
- Percentage completion shown on task cards

---

#### 5.1.4 Configurable Task Categories

**Priority:** P0 (Critical)

**Requirements:**
- **FR-4.1:** System includes default categories:
  - Homework (with subject sub-categories)
  - Chores
  - Music Practice
  - Morning Routine
  - Bedtime Routine
  - School Preparation
  - Other

- **FR-4.2:** Users can create custom categories with:
  - Category name (required, max 50 characters)
  - Icon selection (from built-in library of 50+ icons)
  - Color selection (from preset palette or custom hex)
  - Default reminder settings (optional)
  - Sort order

- **FR-4.3:** Users can edit existing categories (including defaults)

- **FR-4.4:** Users can delete custom categories (with task reassignment prompt)

- **FR-4.5:** Categories can be reordered in settings

- **FR-4.6:** Homework category supports subject sub-categories:
  - Math, English, Science, History, Art, Music, PE, Languages, etc.
  - Custom subjects can be added

**Acceptance Criteria:**
- Category changes reflect immediately across all views
- Deleting a category prompts user to reassign tasks
- Icon library loads quickly (<500ms)
- Color picker is accessible and clear
- At least one category must exist at all times

---

#### 5.1.5 Pomodoro Timer System

**Priority:** P0 (Critical)

**Requirements:**
- **FR-5.1:** Built-in Pomodoro timer with configurable durations:
  - Default work session: 20 minutes
  - Default break: 5 minutes
  - User can adjust in increments of 5 minutes (10-60 min work, 3-15 min break)

- **FR-5.2:** Timer displays:
  - Large, easy-to-read countdown
  - Circular progress ring
  - Current session type (Work/Break)
  - Session number (e.g., "Session 2 of 4")
  - Task name being worked on

- **FR-5.3:** Timer controls:
  - Start/Pause button
  - Skip session button
  - Stop timer button (with confirmation)
  - Extend session by 5 minutes (max 2 extensions)

- **FR-5.4:** Timer notifications:
  - Audio alert when session ends (customizable sound)
  - Visual alert (full-screen if app is open)
  - System notification if app is in background
  - Optional vibration (mobile)

- **FR-5.5:** After work session ends:
  - Automatic transition to break screen
  - Break activity suggestions (stretch, water, snack)
  - "Skip break" option (with warning)

- **FR-5.6:** After break ends:
  - "Ready to continue?" prompt
  - Option to start next session or stop
  - Auto-advance to next sub-task if applicable

- **FR-5.7:** Timer persists across app navigation (small widget in corner)

- **FR-5.8:** Timer state saves if app is closed (resume on reopen)

**Acceptance Criteria:**
- Timer is accurate within 1 second
- Notifications trigger reliably
- Timer UI is visible and clear from across the room
- Break suggestions are relevant and helpful
- Timer integrates seamlessly with task system

---

#### 5.1.6 Automatic Task Breakdown

**Priority:** P0 (Critical)

**Requirements:**
- **FR-6.1:** When creating/editing a task with estimated duration >20 minutes:
  - System suggests breaking task into 20-minute chunks
  - Shows preview: "This will take approximately X Pomodoro sessions"
  - Offers to auto-create sub-tasks for each session

- **FR-6.2:** Auto-generated sub-tasks are named:
  - "[Task Name] - Part 1 of X"
  - "[Task Name] - Part 2 of X"
  - etc.

- **FR-6.3:** User can:
  - Accept auto-breakdown
  - Decline and create manual sub-tasks
  - Adjust number of sessions
  - Customize sub-task names

- **FR-6.4:** When starting a multi-session task:
  - Timer automatically sets to work duration
  - Displays "Part X of Y" in timer view
  - Auto-advances to next part after break

- **FR-6.5:** Progress tracking shows:
  - Which session currently on
  - How many sessions remaining
  - Total time remaining
  - Percentage complete

**Acceptance Criteria:**
- Suggestion appears immediately when duration exceeds threshold
- Auto-generated sub-tasks are logical and well-named
- User can easily customize the breakdown
- Progress is clear and motivating

---

#### 5.1.7 Calendar Views

**Priority:** P0 (Critical)

**Requirements:**

##### Daily Calendar View
- **FR-7.1:** Display single day with:
  - Hourly timeline (6 AM - 11 PM default, configurable)
  - Current time indicator (moving line)
  - Tasks displayed in time blocks
  - Morning/evening routine sections
  - "What to pack" section (if next day)
  - Homework due today highlighted

##### Weekly Calendar View
- **FR-7.2:** Display 7-day week with:
  - Column per day (Sunday-Saturday or Monday-Sunday)
  - Time blocks for scheduled tasks
  - Due date badges
  - Color-coded by category
  - Scroll to current day on load
  - Week navigation (prev/next buttons)

##### Monthly Calendar View
- **FR-7.3:** Display full month with:
  - Standard calendar grid (weeks as rows)
  - Multiple tasks per day (stacked or count badge)
  - Homework due dates prominently marked
  - School events overlay (from Teams calendar)
  - Color dots for categories
  - Expandable day view (click day to see details)
  - Month navigation (prev/next buttons)
  - "Today" button to jump to current date

##### Shared Requirements
- **FR-7.4:** All calendar views:
  - Highlight current day
  - Show completion status (checkmarks on completed tasks)
  - Filter by category (toggle on/off)
  - Search/find tasks
  - Quick-add task button
  - Sync status indicator (for Teams integration)

- **FR-7.5:** Tapping a task opens detail view with:
  - Full task information
  - Sub-tasks checklist
  - Start Pomodoro button
  - Edit/delete options
  - Mark complete button

**Acceptance Criteria:**
- Calendar views load in <2 seconds
- All tasks display in correct date/time slots
- Navigation is smooth and intuitive
- Color coding is consistent across views
- View preference is saved

---

#### 5.1.8 Microsoft Teams for Education Integration

**Priority:** P0 (Critical)

**Requirements:**

##### Authentication
- **FR-8.1:** Users can sign in with Microsoft school account (OAuth 2.0)

- **FR-8.2:** App requests minimum required permissions:
  - `EduAssignments.Read` (read assignments)
  - `EduRoster.Read` (read classes)
  - `Calendars.Read` (read school calendar)

- **FR-8.3:** Token management:
  - Secure storage of access/refresh tokens
  - Automatic token refresh
  - Clear error messages if authentication fails
  - "Sign out" option to disconnect account

##### Assignment Sync
- **FR-8.4:** Import all assignments from Teams with:
  - Assignment title
  - Subject/class name
  - Due date and time
  - Description/instructions
  - Point value (optional display)
  - Attached resources (links to files)
  - Status (assigned, submitted, graded)

- **FR-8.5:** Sync frequency:
  - Manual "Sync Now" button
  - Automatic sync every 30 minutes (configurable: 15min/30min/1hr/manual only)
  - Sync on app launch
  - Background sync (when app is open)

- **FR-8.6:** Sync behavior:
  - Create new tasks for new assignments
  - Update due dates if changed in Teams
  - Update status if submitted/graded in Teams
  - Don't duplicate existing assignments
  - Show "Last synced: X minutes ago" indicator

- **FR-8.7:** Assignment task properties:
  - Automatically assigned to Homework category
  - Subject auto-detected from class name
  - Due date from assignment
  - Links to open in Teams (browser or app)
  - Badge/tag: "From Teams"
  - Cannot delete (can only hide/complete)

##### School Calendar Integration
- **FR-8.8:** Import school calendar events:
  - Class schedule (if available)
  - School holidays
  - Half-term breaks
  - Special events (assemblies, field trips)
  - Exam dates

- **FR-8.9:** Calendar overlay:
  - School events shown in different color/style
  - Toggle to show/hide school calendar
  - Don't create tasks from school events (display only)
  - Hover/tap to see event details

##### Error Handling & Offline
- **FR-8.10:** Handle sync errors gracefully:
  - Show error message if sync fails
  - Retry mechanism (3 attempts)
  - Work offline if Teams is unavailable
  - Queue sync for when connection restored

- **FR-8.11:** Conflict resolution:
  - If user edited a Teams assignment task, preserve edits on sync
  - Warn if due date changed in Teams
  - Allow user to choose which data to keep

**Acceptance Criteria:**
- Authentication flow completes in <30 seconds
- Sync imports 100% of accessible assignments
- No duplicate assignments created
- Sync status is always visible
- Error messages are clear and actionable
- App remains functional when offline

---

#### 5.1.9 School Preparation & Packing System

**Priority:** P0 (Critical)

**Requirements:**

##### Pack List Categories
- **FR-9.1:** System includes default pack item categories:
  - Sports Kit (PE clothes, trainers, water bottle)
  - PE Bag (separate from sports kit if needed)
  - Art Set (paints, brushes, apron, portfolio)
  - Musical Instrument (specific instrument name)
  - Homework (linked to specific assignments)
  - Textbooks (by subject)
  - Special Equipment (calculator, compass, protractor, etc.)
  - Lunch/Snack
  - Other (custom items)

- **FR-9.2:** Users can create custom pack items with:
  - Item name
  - Category
  - Icon
  - Linked to specific days (e.g., "PE Kit every Tuesday")
  - Linked to specific calendar events (e.g., "Instrument for Music lesson")

##### Recurring Pack Items
- **FR-9.3:** Set items that recur on specific days:
  - "Every Monday: Sports Kit"
  - "Every Tuesday and Thursday: PE Bag"
  - "Every Wednesday: Art Set"
  - "Every Friday: Musical Instrument"

- **FR-9.4:** Visual weekly pattern display in settings:
  - Grid showing each day of week
  - Icons for items needed that day
  - Easy toggle on/off for each item/day

##### Homework Packing Integration
- **FR-9.5:** Completed homework automatically added to pack list:
  - Shows on the day it's due
  - Linked to original task
  - Displays subject and assignment name
  - Removed from pack list after due date

- **FR-9.6:** Incomplete homework shows warning:
  - "Not done yet - pack when finished"
  - Stays on pack list even if not complete (to avoid forgetting)

##### Packing Checklist Display
- **FR-9.7:** Pack list displays as visual checklist:
  - Large checkboxes (easy to tap)
  - Item icons and names
  - Grouped by category
  - Sort by: category, priority, or custom order

- **FR-9.8:** Checklist views:
  - "Tonight" - items to pack for tomorrow
  - "Tomorrow" - what should be packed already
  - "Morning" - final check before leaving

##### Packing Reminders
- **FR-9.9:** Evening reminder (default 7 PM, configurable):
  - Notification: "Check what to pack for tomorrow"
  - Opens pack list for next school day
  - Shows all items needed
  - Allows checking off as packed

- **FR-9.10:** Morning reminder (default 7:30 AM, configurable):
  - Notification: "Final packing check"
  - Shows items not yet checked
  - Quick review before leaving
  - "I have everything" button

- **FR-9.11:** Subject-specific reminders (1 hour before):
  - "Don't forget PE kit today" (on PE days)
  - "Remember your Art set" (on Art days)
  - Customizable lead time

##### Calendar Integration
- **FR-9.12:** Pack items shown on calendar:
  - Icons for items needed that day
  - In daily/weekly/monthly views
  - "Pack" section in daily view
  - Badge count on calendar days with items

- **FR-9.13:** Homework due dates on calendar show:
  - Assignment name
  - Subject
  - "Pack homework" reminder
  - Link to original task

**Acceptance Criteria:**
- Pack list is always accessible in <2 taps
- Reminders trigger at correct times
- Items reset daily (unchecked for new day)
- Homework items sync correctly with due dates
- No items are ever missed or forgotten

---

#### 5.1.10 Morning & Bedtime Routines

**Priority:** P0 (Critical)

**Requirements:**

##### Pre-built Routine Templates
- **FR-10.1:** Morning Routine default template:
  - Wake up alarm acknowledgment
  - Breakfast
  - Get dressed
  - Brush teeth
  - Pack bag (links to pack list)
  - Check homework is packed
  - Check items to bring (sports kit, etc.)
  - Leave by [time] reminder

- **FR-10.2:** Bedtime Routine default template:
  - Homework check (all complete?)
  - Pack bag for tomorrow (links to pack list)
  - Shower/bath
  - Brush teeth
  - Set clothes out for morning
  - Set alarm
  - Lights out by [time]

- **FR-10.3:** After-school Routine template:
  - Snack
  - Change clothes
  - Review homework assignments
  - Schedule homework time
  - Practice (music/sports)

##### Custom Routine Builder
- **FR-10.4:** Users can create custom routines with:
  - Routine name
  - Time to start (optional)
  - List of steps (ordered)
  - Estimated time per step
  - Icons for each step
  - Recurring schedule (which days)

- **FR-10.5:** Each routine step has:
  - Step name
  - Optional description
  - Estimated duration
  - Checkbox for completion
  - Optional link to task or pack list
  - Optional timer (for time-limited steps)

- **FR-10.6:** Routine step types:
  - Simple checkbox (yes/no)
  - Timer step (brush teeth for 2 min)
  - Link to pack list (opens packing checklist)
  - Link to task (e.g., "check homework")
  - Sub-routine (routine within routine)

##### Routine Execution
- **FR-10.7:** Starting a routine:
  - Shows full checklist of steps
  - Large "Start Routine" button
  - Estimated total time displayed
  - Current time and target end time

- **FR-10.8:** During routine:
  - Highlight current step
  - Show progress bar (X of Y steps)
  - Checkbox for each completed step
  - Optional timer for time-sensitive steps
  - "Next" button to advance
  - Auto-advance option (configurable)

- **FR-10.9:** Routine completion:
  - Satisfying completion animation
  - "Routine completed in X minutes" summary
  - Compare to estimated time
  - Streak tracking (days in a row)

##### Routine Reminders
- **FR-10.10:** Routine start reminders:
  - Morning routine: configurable time (default 7 AM)
  - Bedtime routine: configurable time (default 8:30 PM)
  - Custom routine: scheduled time or manual start

- **FR-10.11:** Routine step reminders:
  - If stuck on one step for too long, gentle prompt
  - "Don't forget to pack your bag" at bedtime
  - "Check you have everything" before leaving

##### Routine Analytics
- **FR-10.12:** Track routine performance:
  - Completion rate (%)
  - Average time to complete
  - Which steps take longest
  - Streaks (consecutive days)
  - Morning routine on-time rate

**Acceptance Criteria:**
- Routines are intuitive and easy to follow
- Each step is clearly visible and actionable
- Progress is motivating and encouraging
- Reminders are timely and helpful
- Customization is simple and flexible

---

#### 5.1.11 Time Management & Transitions

**Priority:** P0 (Critical)

**Requirements:**

##### Time Awareness
- **FR-11.1:** Visual time indicators:
  - Large clock showing current time
  - Time-of-day visual (morning sun, afternoon, evening moon)
  - Countdown to next task/event
  - "You have X minutes left" for current session

- **FR-11.2:** Time tracking:
  - Estimated duration for each task
  - Actual time spent tracking (via Pomodoro)
  - Comparison: estimated vs. actual
  - Historical average for similar tasks

##### Transition Support
- **FR-11.3:** Before next task/event:
  - 10-minute warning notification
  - 5-minute warning notification
  - 1-minute final warning
  - Visual countdown on screen

- **FR-11.4:** Transition screen between tasks:
  - "Great work on [previous task]!"
  - "Up next: [next task]"
  - Preview of next task details
  - Estimated duration
  - "Take a moment to prepare" message
  - Optional quick break (2-3 min)

- **FR-11.5:** Morning departure countdown:
  - "Leave by 8:00 AM" reminder
  - Countdown: "15 minutes until leaving time"
  - Final check prompt: "Do you have everything?"
  - Links to pack list for verification

##### "What's Next" Display
- **FR-11.6:** Prominent "What's Next" widget on home screen:
  - Current task (if in progress)
  - Next 3 upcoming tasks with times
  - Visual timeline
  - Easy access to start next task

**Acceptance Criteria:**
- Time indicators are always visible and accurate
- Transition warnings are timely and not annoying
- "What's Next" always shows relevant information
- Departure countdown is prominent in morning

---

#### 5.1.12 Notifications & Reminders

**Priority:** P0 (Critical)

**Requirements:**

##### Notification Types
- **FR-12.1:** Task reminders:
  - X minutes before due time (configurable per task)
  - Day before due date (for homework)
  - Custom reminder times

- **FR-12.2:** Routine reminders:
  - Morning routine start
  - Bedtime routine start
  - Custom routine triggers

- **FR-12.3:** Packing reminders:
  - Evening: "Pack for tomorrow"
  - Morning: "Final packing check"
  - Subject-specific: "Don't forget PE kit"

- **FR-12.4:** Timer notifications:
  - Work session complete
  - Break complete
  - Pomodoro cycle complete

- **FR-12.5:** Sync notifications:
  - New homework assignments from Teams
  - Updated due dates
  - Sync errors (if any)

##### Notification Customization
- **FR-12.6:** Global settings:
  - Enable/disable all notifications
  - Quiet hours (no notifications during sleep)
  - Weekend mode (reduced reminders)

- **FR-12.7:** Per-category settings:
  - Custom notification sound
  - Vibration on/off
  - Visual only mode (low-sensory)
  - Lead time (how far in advance)

- **FR-12.8:** Notification delivery:
  - Sound (customizable from library)
  - Vibration (mobile)
  - Visual alert (app open)
  - System notification (app closed/background)
  - Badge count on app icon

##### Snooze & Dismiss
- **FR-12.9:** Snooze options:
  - 5 minutes
  - 10 minutes
  - 15 minutes
  - Maximum 2 snoozes per notification (then forced to dismiss or action)

- **FR-12.10:** Smart snoozing:
  - Can't snooze past task due time
  - Warning if snoozing too close to deadline
  - Suggest better action ("Start task now")

##### Low-Sensory Mode
- **FR-12.11:** Sensory-friendly notifications:
  - No sudden loud sounds (gentle chimes)
  - No bright flashing
  - Subtle vibrations only
  - Visual-only option
  - Gradual volume increase (for timers)

**Acceptance Criteria:**
- Notifications trigger reliably and on time
- Customization is easy and comprehensive
- No overwhelming or anxiety-inducing alerts
- Low-sensory mode actually reduces sensory load
- Snooze limits prevent procrastination

---

#### 5.1.13 Neuro-Inclusive UI Design

**Priority:** P0 (Critical)

**Requirements:**

##### Visual Design Principles
- **FR-13.1:** Minimal clutter:
  - Maximum 3 primary actions visible per screen
  - Generous whitespace
  - No unnecessary decorative elements
  - Hide advanced features behind "More" menus

- **FR-13.2:** High contrast:
  - WCAG AAA contrast ratios (7:1 minimum)
  - Clear separation between elements
  - Borders and dividers where needed
  - No reliance on color alone for information

- **FR-13.3:** Clear typography:
  - Minimum 16px base font size
  - Sans-serif font (default: system font)
  - OpenDyslexic font option
  - Generous line height (1.5-1.6)
  - Left-aligned text (no justified)
  - Short line lengths (<75 characters)

- **FR-13.4:** Consistent layout:
  - Same navigation structure on all screens
  - Predictable button placement
  - Consistent icon usage
  - Same interaction patterns throughout

##### Interactive Elements
- **FR-13.5:** Large touch targets:
  - Minimum 44×44 pixels (mobile)
  - Generous spacing between tappable elements
  - Visual feedback on all interactions
  - No tiny icons or links

- **FR-13.6:** Clear focus states:
  - Obvious keyboard navigation indicators
  - Tab order makes logical sense
  - Skip links for navigation
  - All actions keyboard-accessible

##### Animation & Motion
- **FR-13.7:** Reduced motion option:
  - Disable all non-essential animations
  - Instant transitions instead of sliding
  - Static progress indicators (no spinning)
  - Respect system "reduce motion" preference

- **FR-13.8:** Essential animations only:
  - Gentle fade in/out
  - Smooth scrolling (can be disabled)
  - Completion celebrations (can be disabled)
  - No auto-playing videos or GIFs

##### Color & Themes
- **FR-13.9:** Theme options:
  - Light mode (default)
  - Dark mode
  - High contrast mode
  - Custom theme builder (advanced)

- **FR-13.10:** Color customization:
  - Custom accent color
  - Category colors editable
  - Color blind safe palette option
  - Preview before applying

##### Sensory Considerations
- **FR-13.11:** Sensory settings:
  - Disable sounds globally
  - Disable vibrations globally
  - Reduce visual complexity (icon-only mode)
  - Muted color palette option

##### Text Scaling
- **FR-13.12:** Adjustable text size:
  - Small, Medium (default), Large, Extra Large
  - Respect system text size settings
  - Reflows content properly at all sizes
  - No cut-off text or horizontal scrolling

##### Focus Mode
- **FR-13.13:** Distraction-free focus mode:
  - Hide all non-essential UI
  - Show only current task and timer
  - Minimal color/visual noise
  - Toggle on/off easily
  - Auto-enable during Pomodoro sessions (optional)

**Acceptance Criteria:**
- UI passes WCAG 2.1 AAA standards
- All features accessible via keyboard
- Screen reader compatible (future requirement)
- Reduced motion mode eliminates all unnecessary animation
- Text remains readable at all supported sizes
- Focus mode removes 80%+ of visual elements

---

#### 5.1.14 Progress Tracking & Motivation

**Priority:** P0 (Critical)

**Requirements:**

##### Task Completion Feedback
- **FR-14.1:** Completion interaction:
  - Large, satisfying checkbox
  - Optional completion sound (customizable)
  - Optional completion animation (confetti, checkmark bounce)
  - Haptic feedback (mobile)
  - Immediate visual state change

- **FR-14.2:** Completion summary:
  - "Great job!" encouragement message
  - Show time taken vs. estimated
  - Update streak counters
  - Option to add notes about task

##### Streaks & Consistency
- **FR-14.3:** Completion streaks:
  - Daily task completion streak
  - Homework on-time streak
  - Morning routine streak
  - Bedtime routine streak
  - Zero forgotten items streak

- **FR-14.4:** Streak display:
  - Prominent on home screen
  - Flame/fire emoji for active streaks
  - "X days in a row!" message
  - Encouraging message when at risk of breaking

##### Progress Visualization
- **FR-14.5:** Daily progress:
  - Circular progress ring (tasks completed today)
  - Percentage: "75% of today's tasks done"
  - Visual breakdown by category
  - Comparison to yesterday

- **FR-14.6:** Weekly progress:
  - Bar chart showing completion by day
  - Weekly completion percentage
  - Homework submission rate
  - Pomodoro sessions completed

##### Achievements (Simple, Non-Overwhelming)
- **FR-14.7:** Simple milestone achievements:
  - "First week complete"
  - "10 Pomodoros completed"
  - "Perfect morning routine week"
  - "Zero forgotten items - 5 days"

- **FR-14.8:** Achievement display:
  - Subtle notification when earned
  - Optional celebration (can disable)
  - Achievement list in profile
  - No pressure to collect all (anti-anxiety)

##### History & Logs
- **FR-14.9:** Task history:
  - Completed tasks list (last 30 days)
  - Filter by category, date range
  - Pomodoro session logs
  - Time tracking data

- **FR-14.10:** Exportable reports (for parents/teachers):
  - Weekly summary PDF
  - Completion rates by category
  - Time spent on homework
  - Forgotten items log

**Acceptance Criteria:**
- Completion feedback is immediate and satisfying
- Streaks are motivating without being anxiety-inducing
- Progress visualizations are clear and accurate
- Achievements feel earned and meaningful
- History data is accurate and useful

---

#### 5.1.15 Data Management & Storage

**Priority:** P0 (Critical)

**Requirements:**

##### Local Storage
- **FR-15.1:** SQLite database for all app data:
  - Tasks and sub-tasks
  - Categories
  - Routines
  - Pack lists
  - Pomodoro session logs
  - User settings
  - Teams sync cache

- **FR-15.2:** Data persistence:
  - All changes save immediately
  - Auto-save every 30 seconds (for in-progress edits)
  - No data loss on app crash
  - Transaction support for data integrity

##### Microsoft Account Integration
- **FR-15.3:** OAuth 2.0 authentication:
  - Microsoft Authentication Library (MSAL)
  - Secure token storage (encrypted)
  - Automatic token refresh
  - Token expiry handling

- **FR-15.4:** Account management:
  - Sign in with school account
  - Sign out (clears tokens, keeps local data)
  - Reconnect if authentication expires
  - Multiple account support (future: switch between siblings)

##### Data Privacy
- **FR-15.5:** Privacy principles:
  - All data stored locally by default
  - No data sent to third-party servers
  - Teams data accessed via Microsoft Graph (official API)
  - No analytics tracking without explicit consent
  - No advertising, ever

- **FR-15.6:** Data deletion:
  - Clear all data option (with confirmation)
  - Delete Teams sync data only (keep local tasks)
  - Export data before deletion (optional)

##### Backup & Restore (Future Phase 2)
- **FR-15.7:** Manual backup:
  - Export all data to JSON file
  - Import from backup file
  - Merge or replace options

**Acceptance Criteria:**
- Database operations are fast (<100ms for reads)
- No data loss under any circumstances
- Authentication is secure and follows Microsoft best practices
- User data is never sent anywhere except Teams (via Graph API)
- Backup/restore works reliably

---

### 5.2 Phase 2: Enhanced Features (Should Have)

#### 5.2.1 Parent/Guardian Portal

**Priority:** P1 (High)

**Requirements:**
- **FR-16.1:** Parent view mode (separate login or access code)
- **FR-16.2:** Dashboard showing:
  - Homework completion status
  - Upcoming due dates
  - Forgotten items log
  - Routine completion rates
  - Pomodoro session summary
- **FR-16.3:** Ability to add tasks (appears in student view)
- **FR-16.4:** Notification if homework overdue
- **FR-16.5:** Weekly email summary (optional)
- **FR-16.6:** Privacy controls (student can hide certain categories)

---

#### 5.2.2 Smart Scheduling & AI Assistance

**Priority:** P1 (High)

**Requirements:**
- **FR-17.1:** Suggest optimal homework times based on:
  - Historical completion patterns
  - Due date urgency
  - Estimated duration
  - Available free time
- **FR-17.2:** Auto-schedule homework into free time blocks
- **FR-17.3:** Predict how many Pomodoro sessions needed
- **FR-17.4:** Break large assignments into milestones:
  - Research paper → research, outline, draft, revise, final
  - Math homework → odd problems, even problems
- **FR-17.5:** "You have 3 days - suggested plan: 2 sessions today, 2 tomorrow, 1 final review"

---

#### 5.2.3 Advanced Homework Management

**Priority:** P1 (High)

**Requirements:**
- **FR-18.1:** Homework complexity estimation (simple/moderate/complex)
- **FR-18.2:** Multi-day project scheduling
- **FR-18.3:** Automatic milestone creation for projects
- **FR-18.4:** Integration with grading (from Teams)
- **FR-18.5:** Completion percentage tracking per assignment
- **FR-18.6:** "Submit homework" reminder (link to Teams)

---

#### 5.2.4 Analytics & Insights

**Priority:** P1 (High)

**Requirements:**
- **FR-19.1:** Best times for homework analysis (by subject)
- **FR-19.2:** Average Pomodoros per task type
- **FR-19.3:** Forgetting patterns (what items/days)
- **FR-19.4:** Productivity heat map (by day/time)
- **FR-19.5:** Subject-specific focus duration
- **FR-19.6:** Break effectiveness tracking
- **FR-19.7:** Exportable reports (PDF/CSV)

---

#### 5.2.5 Enhanced Packing System

**Priority:** P2 (Medium)

**Requirements:**
- **FR-20.1:** Photo attachments for complex items
- **FR-20.2:** "Where items are stored" photo guide
- **FR-20.3:** Barcode/QR scanning for item tracking
- **FR-20.4:** Shared packing lists (with siblings/friends)
- **FR-20.5:** Weather-based suggestions (coat, umbrella)
- **FR-20.6:** Sports kit contents checklist (socks, shirt, shorts, etc.)

---

#### 5.2.6 Cloud Sync & Multi-Device

**Priority:** P2 (Medium)

**Requirements:**
- **FR-21.1:** Cloud backup to OneDrive/iCloud
- **FR-21.2:** Sync across devices (Mac, Windows, Android)
- **FR-21.3:** Offline-first architecture (works without internet)
- **FR-21.4:** Conflict resolution (if edited on multiple devices)
- **FR-21.5:** Sync status indicator
- **FR-21.6:** Manual sync trigger

---

#### 5.2.7 Additional Integrations

**Priority:** P2 (Medium)

**Requirements:**
- **FR-22.1:** Google Classroom integration (alternative to Teams)
- **FR-22.2:** Export to Apple/Google Calendar
- **FR-22.3:** Smart home integrations:
  - Alexa/Google Assistant announcements
  - Smart display integration
  - Philips Hue for focus mode lighting
- **FR-22.4:** Wearable notifications (Apple Watch, etc.)

---

### 5.3 Phase 3: Future Enhancements

#### 5.3.1 Social & Collaboration

**Priority:** P3 (Low)

**Requirements:**
- **FR-23.1:** Study buddy coordination
- **FR-23.2:** Group project task splitting
- **FR-23.3:** Shared homework reminders
- **FR-23.4:** Teacher communication portal (if school permits)

---

#### 5.3.2 Advanced Focus Tools

**Priority:** P3 (Low)

**Requirements:**
- **FR-24.1:** Focus music/ambient sounds library
- **FR-24.2:** Website/app blocking during Pomodoros (if technically feasible)
- **FR-24.3:** Distraction logging ("I got distracted by...")
- **FR-24.4:** Environmental suggestions (lighting, noise level)

---

#### 5.3.3 Gamification (Optional)

**Priority:** P3 (Low)

**Requirements:**
- **FR-25.1:** Point system for task completion
- **FR-25.2:** Unlock custom themes/icons/avatars
- **FR-25.3:** Virtual pet that thrives on productivity
- **FR-25.4:** Friendly competition with classmates (opt-in)

**Note:** Gamification is optional and should be implemented carefully to avoid creating anxiety or pressure. Must be fully disable-able.

---

## 6. User Stories & Use Cases

### 6.1 Primary User Stories (Student - Alex)

#### US-1: Homework Management
**As a student,**
**I want to** automatically see all my homework assignments from Teams in my task list,
**So that** I don't forget any assignments and know what's due when.

**Acceptance Criteria:**
- All Teams assignments appear in app within 30 minutes of being posted
- Assignments show correct due dates and subject
- I can click to see full assignment details
- I can break down homework into smaller chunks

---

#### US-2: Focus Support
**As a student who can only concentrate for 20 minutes,**
**I want to** break my homework into 20-minute sessions with breaks,
**So that** I can actually complete my work without getting overwhelmed.

**Acceptance Criteria:**
- App suggests breaking tasks into 20-minute chunks
- Timer runs for 20 minutes then forces a break
- I can see how many more sessions I have left
- Break timer helps me rest properly

---

#### US-3: Packing Reminders
**As a student who often forgets things,**
**I want to** see what I need to pack each day and get reminders,
**So that** I never show up to school without my PE kit or homework again.

**Acceptance Criteria:**
- Evening reminder shows what to pack for tomorrow
- Morning reminder confirms I have everything
- PE kit shows up automatically on PE days
- Homework I need to hand in appears on pack list

---

#### US-4: Morning Routine
**As a student who struggles with mornings,**
**I want to** follow a simple checklist to get ready,
**So that** I remember everything and leave on time.

**Acceptance Criteria:**
- Morning routine starts at my wake-up time
- Each step is clearly shown with checkbox
- I get a warning when I need to leave soon
- Final check reminds me to grab my packed bag

---

#### US-5: Visual Schedule
**As a visual learner,**
**I want to** see my day laid out in a calendar with colors and icons,
**So that** I can understand at a glance what I need to do.

**Acceptance Criteria:**
- Calendar shows all tasks with due dates
- Each category has a different color
- I can see what's happening today, this week, and this month
- Icons make it easy to recognize task types

---

### 6.2 Secondary User Stories (Parent)

#### US-6: Homework Monitoring
**As a parent,**
**I want to** see which homework assignments are due and whether they're completed,
**So that** I can support my child without having to constantly ask.

**Acceptance Criteria:**
- Parent view shows all homework with status
- I can see what's overdue or coming up
- I get notified if something is forgotten
- Weekly summary email (optional)

---

#### US-7: Independence Building
**As a parent,**
**I want to** gradually reduce scaffolding as my child builds skills,
**So that** they develop independence over time.

**Acceptance Criteria:**
- I can adjust reminder frequency
- I can see completion trends over time
- I can enable/disable features as needed
- System suggests when to reduce support

---

### 6.3 Key Use Cases

#### UC-1: Monday Morning - School Preparation

**Scenario:** It's Monday morning, and Alex needs to get ready for school, including bringing his PE kit.

**Flow:**
1. 7:00 AM - Morning routine reminder triggers
2. Alex opens app and sees morning checklist
3. Checklist includes "Pack bag" step
4. Tapping "Pack bag" opens pack list showing:
   - PE Kit (highlighted - it's Monday)
   - Homework: Math worksheet (due today)
   - Textbook: History
   - Lunch
5. Alex checks off items as he packs
6. 7:45 AM - "Leave in 15 minutes" countdown appears
7. 7:55 AM - Final check: "Do you have everything?"
8. Alex confirms and leaves for school

**Expected Outcome:** Alex arrives at school with PE kit and homework, on time.

---

#### UC-2: After School - Homework Assignment Received

**Scenario:** Alex's teacher posts a new homework assignment in Teams during school hours.

**Flow:**
1. 2:30 PM - Teacher posts "Math: Complete Chapter 5 exercises" due Wednesday
2. 3:00 PM - App syncs with Teams (auto-sync every 30 min)
3. New task appears in homework category:
   - Title: "Math: Complete Chapter 5 exercises"
   - Due: Wednesday 9:00 AM
   - Estimated: 40 minutes (auto-suggested)
4. App suggests: "This will take 2 Pomodoro sessions"
5. Alex accepts auto-breakdown:
   - Sub-task 1: "Math homework - Part 1 of 2"
   - Sub-task 2: "Math homework - Part 2 of 2"
6. App suggests scheduling: "Do session 1 today at 4 PM, session 2 tomorrow at 4 PM"
7. Homework also appears on Wednesday's pack list: "Pack: Math homework"

**Expected Outcome:** Alex knows about the homework immediately, has a plan to complete it, and won't forget to bring it.

---

#### UC-3: Evening - Homework Session with Pomodoro

**Scenario:** Alex sits down to do his Math homework (Part 1 of 2).

**Flow:**
1. 4:00 PM - "Time for homework" reminder triggers
2. Alex opens task: "Math homework - Part 1 of 2"
3. Taps "Start Pomodoro"
4. Timer screen shows:
   - Large countdown: 20:00
   - Circular progress ring
   - Task name: "Math homework - Part 1"
   - "Session 1 of 2"
5. Alex works for 20 minutes
6. Timer ends - sound chime + notification
7. Break screen appears:
   - "Great work! Take a 5-minute break"
   - Suggestions: "Stretch, get water, walk around"
   - Break timer: 5:00
8. After break: "Ready to continue or stop for now?"
9. Alex selects "Stop for now"
10. Sub-task 1 is marked complete
11. App shows: "Part 2 scheduled for tomorrow at 4 PM"

**Expected Outcome:** Alex completes half the homework without getting overwhelmed, takes a proper break, and knows when to do the rest.

---

#### UC-4: Sunday Evening - Planning the Week

**Scenario:** Alex wants to see what's coming up this week.

**Flow:**
1. Opens weekly calendar view
2. Sees:
   - **Monday:** PE (pack sports kit), Math homework due
   - **Tuesday:** Music lesson (pack instrument)
   - **Wednesday:** History test (study sessions scheduled Sat/Sun/Mon)
   - **Thursday:** PE (pack sports kit)
   - **Friday:** Art class (pack art set), English essay due
3. Also sees color-coded tasks:
   - Green: Homework
   - Blue: Chores
   - Purple: Music practice
   - Orange: Routines
4. Can tap any day to see details
5. Can tap any task to start working on it early

**Expected Outcome:** Alex has a clear visual understanding of the week ahead and can plan accordingly.

---

#### UC-5: Bedtime Routine - Preparation for Tomorrow

**Scenario:** It's Sunday night, and Alex needs to prepare for Monday.

**Flow:**
1. 8:30 PM - Bedtime routine reminder triggers
2. Alex opens bedtime routine checklist
3. Steps include:
   - ✅ Homework check (all done? Yes - Math completed earlier)
   - ⬜ Pack bag for tomorrow
   - ⬜ Shower
   - ⬜ Brush teeth
   - ⬜ Set alarm
4. Taps "Pack bag for tomorrow"
5. Pack list opens showing Monday's items:
   - ⬜ PE Kit (it's Monday - PE day)
   - ⬜ Math homework (completed, ready to pack)
   - ⬜ Textbooks: History, English
   - ⬜ Lunch
6. Alex checks off items as he packs
7. Returns to bedtime routine and continues
8. Completes all steps
9. "Bedtime routine complete! Streak: 5 days 🔥"

**Expected Outcome:** Alex goes to bed knowing everything is ready for tomorrow, reducing morning stress.

---

## 7. Technical Architecture

### 7.1 Platform Strategy

#### Phase 1: Blazor Server MVP
**Purpose:** Rapid prototyping and validation

**Characteristics:**
- Web-based application
- Hosted on local server or Azure
- Accessible via browser on any device
- Real-time UI updates via SignalR
- Minimal client-side code
- Server-side rendering

**Trade-offs:**
- ✅ Faster development
- ✅ Easier debugging
- ✅ Shared C# codebase
- ❌ Requires internet connection
- ❌ Server hosting required
- ❌ Less native platform integration

---

#### Phase 2: .NET MAUI Hybrid App
**Purpose:** Cross-platform native apps with shared UI

**Characteristics:**
- BlazorWebView component in MAUI
- Shared Razor components from Phase 1
- Native platform capabilities (notifications, file system, etc.)
- Offline-first architecture
- Platform-specific features via MAUI APIs

**Platforms:**
- Windows Desktop (Windows 10/11)
- macOS Desktop (macOS 11+)
- Android Mobile (Android 8.0+)
- iOS Mobile (future, if needed)

**Trade-offs:**
- ✅ Code reuse from Phase 1 (80%+ shared)
- ✅ Native performance and integration
- ✅ Offline support
- ✅ Platform-specific features
- ❌ More complex build process
- ❌ Platform-specific testing required

---

### 7.2 Technology Stack

#### Frontend
- **Blazor (Razor Components):** UI framework
- **MudBlazor or Radzen:** Component library (evaluation needed)
- **CSS/SCSS:** Styling
- **JavaScript Interop:** For platform-specific features

#### Backend
- **.NET 8 (LTS):** Runtime and framework
- **ASP.NET Core:** Web hosting (Blazor Server)
- **MAUI:** Cross-platform framework (Phase 2)

#### Data Layer
- **SQLite:** Local database (via Entity Framework Core)
- **Entity Framework Core 8:** ORM
- **Repository Pattern:** Data access abstraction

#### Authentication & APIs
- **Microsoft Authentication Library (MSAL):** OAuth 2.0 for Teams
- **Microsoft Graph SDK for .NET:** Teams and Calendar APIs
- **System.Text.Json:** JSON serialization

#### Services & Background Tasks
- **IHostedService:** Background sync service
- **System.Timers:** Pomodoro timer
- **Platform-specific notification APIs:** Local notifications

#### Testing
- **xUnit:** Unit testing framework
- **bUnit:** Blazor component testing
- **Moq:** Mocking framework
- **FluentAssertions:** Assertion library

#### DevOps & Tooling
- **Git:** Version control
- **GitHub:** Repository hosting
- **Visual Studio 2022:** IDE
- **Azure DevOps or GitHub Actions:** CI/CD (future)

---

### 7.3 Architecture Layers

```
┌─────────────────────────────────────────────────────────────┐
│                     Presentation Layer                       │
│                  (Blazor Razor Components)                   │
│  - Pages (Calendar, Tasks, Routines, Settings)               │
│  - Components (TaskCard, Timer, PackList, etc.)              │
│  - Layouts (MainLayout, FocusLayout)                         │
└─────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────┐
│                      Services Layer                          │
│                   (Business Logic)                           │
│  - TaskService (CRUD, completion, breakdown)                 │
│  - CategoryService (manage categories)                       │
│  - PomodoroService (timer logic, session tracking)           │
│  - RoutineService (routine execution, tracking)              │
│  - PackingListService (pack items, reminders)                │
│  - NotificationService (schedule, trigger notifications)     │
│  - TeamsIntegrationService (sync assignments, calendar)      │
│  - AnalyticsService (progress tracking, insights)            │
└─────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────┐
│                       Data Layer                             │
│                  (Repository Pattern)                        │
│  - ITaskRepository / TaskRepository                          │
│  - ICategoryRepository / CategoryRepository                  │
│  - IRoutineRepository / RoutineRepository                    │
│  - IPackItemRepository / PackItemRepository                  │
│  - IPomodoroSessionRepository / PomodoroSessionRepository    │
│  - Database Context (Entity Framework Core)                  │
└─────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────┐
│                    Persistence Layer                         │
│                      (SQLite)                                │
│  - Local database file: taskscheduler.db                     │
│  - Tables: Tasks, Categories, Routines, PackItems, etc.      │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                  External Services                           │
│  - Microsoft Graph API (Teams assignments, calendar)         │
│  - Microsoft Authentication (OAuth 2.0)                      │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                Platform Services (MAUI)                      │
│  - Local Notifications (platform-specific)                   │
│  - File System Access                                        │
│  - Secure Storage (tokens)                                   │
│  - Background Tasks                                          │
└─────────────────────────────────────────────────────────────┘
```

---

### 7.4 Data Models

#### Task
```csharp
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public DateTime? DueDate { get; set; }
    public int? EstimatedDuration { get; set; } // minutes
    public Priority Priority { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedDate { get; set; }
    public bool IsRecurring { get; set; }
    public RecurrencePattern RecurrencePattern { get; set; }
    public int? ParentTaskId { get; set; } // for sub-tasks
    public List<TaskItem> SubTasks { get; set; }
    public bool IsFromTeams { get; set; }
    public string TeamsAssignmentId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}
```

#### Category
```csharp
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IconName { get; set; }
    public string ColorHex { get; set; }
    public int SortOrder { get; set; }
    public bool IsDefault { get; set; }
    public NotificationSettings DefaultNotificationSettings { get; set; }
}
```

#### Routine
```csharp
public class Routine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeSpan? StartTime { get; set; }
    public List<RoutineStep> Steps { get; set; }
    public RecurrencePattern Schedule { get; set; }
    public int EstimatedDuration { get; set; } // minutes
}

public class RoutineStep
{
    public int Id { get; set; }
    public int RoutineId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int SortOrder { get; set; }
    public int? EstimatedDuration { get; set; }
    public string IconName { get; set; }
    public RoutineStepType Type { get; set; } // Checkbox, Timer, Link
    public string LinkReference { get; set; } // Task ID or Pack List
}
```

#### PackItem
```csharp
public class PackItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public PackItemCategory Category { get; set; }
    public string IconName { get; set; }
    public bool IsRecurring { get; set; }
    public List<DayOfWeek> RecurringDays { get; set; }
    public int? LinkedTaskId { get; set; } // for homework
    public DateTime? SpecificDate { get; set; } // for one-time items
}
```

#### PomodoroSession
```csharp
public class PomodoroSession
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int WorkDuration { get; set; } // minutes
    public int BreakDuration { get; set; } // minutes
    public bool WasCompleted { get; set; }
    public bool WasInterrupted { get; set; }
}
```

---

### 7.5 Microsoft Graph API Integration

#### Authentication Flow
```
1. User clicks "Connect to Teams"
2. App initiates OAuth 2.0 flow (MSAL)
3. User signs in with school Microsoft account
4. Microsoft prompts for permissions:
   - EduAssignments.Read
   - EduRoster.Read
   - Calendars.Read
5. User grants permissions
6. App receives access token and refresh token
7. Tokens stored securely (encrypted)
8. App can now call Graph API
```

#### Sync Flow
```
Every 30 minutes (or manual trigger):
1. Check if access token is valid (refresh if needed)
2. Call Graph API: GET /education/me/assignments
3. Parse response (list of assignments)
4. For each assignment:
   a. Check if already exists in local DB (by Teams ID)
   b. If new: Create new Task with IsFromTeams = true
   c. If exists: Update due date if changed
   d. Map assignment to appropriate category (by class name)
5. Store last sync timestamp
6. Update UI with new tasks
7. Send notification if new assignments found
```

#### API Endpoints Used

##### Get User's Assignments
```
GET /education/me/assignments
```
Returns all assignments for the authenticated student.

##### Get Assignment Details
```
GET /education/me/assignments/{assignmentId}
```
Returns details for a specific assignment.

##### Get User's Classes
```
GET /education/me/classes
```
Returns list of classes (for category mapping).

##### Get Calendar Events
```
GET /me/calendar/events?$filter=start/dateTime ge '{startDate}'
```
Returns school calendar events.

#### Rate Limiting & Error Handling
- **Rate Limits:** Microsoft Graph has throttling limits
- **Retry Logic:** Exponential backoff on 429 errors
- **Offline Handling:** Queue sync requests if offline
- **Error Display:** Clear user-facing error messages

---

### 7.6 Offline-First Architecture

**Principle:** App must work fully offline except for Teams sync.

#### Strategy
1. **Local-first data:** All user data in SQLite
2. **Sync when connected:** Background sync for Teams data
3. **Graceful degradation:** Show "Teams sync unavailable" when offline
4. **Queue operations:** Queue sync requests when offline, process when online
5. **Conflict resolution:** Last-write-wins for simple conflicts

#### Offline Features
- ✅ Create/edit/delete tasks
- ✅ Start Pomodoro timers
- ✅ Complete routines
- ✅ View all data
- ✅ Receive local notifications
- ❌ Sync with Teams (requires internet)
- ❌ Import new assignments

---

### 7.7 Notification Architecture

#### Platform-Specific Implementation

##### Blazor Server (Phase 1)
- **Browser notifications** (Web Notifications API)
- **In-app alerts** (modal/toast components)
- **Limitations:** No system notifications when browser closed

##### MAUI (Phase 2)
- **Windows:** Windows Notification System (WinUI)
- **macOS:** User Notifications Framework
- **Android:** Android Notification Channels
- **Features:**
  - Background notifications
  - Action buttons (Snooze, Start Task)
  - Custom sounds
  - Badge counts

#### Notification Scheduling
```csharp
// Example: Schedule homework reminder
NotificationService.Schedule(
    title: "Math homework due tomorrow",
    body: "Don't forget to pack your completed homework",
    scheduledTime: DateTime.Today.AddDays(1).AddHours(7), // 7 AM tomorrow
    category: "homework_reminder",
    actions: ["Start Task", "Snooze"]
);
```

---

## 8. UI/UX Requirements

### 8.1 Design System

#### Color Palette

**Light Mode:**
- **Primary:** #3B82F6 (Blue - focus, actions)
- **Success:** #10B981 (Green - completed tasks)
- **Warning:** #F59E0B (Orange - due soon)
- **Error:** #EF4444 (Red - overdue)
- **Background:** #FFFFFF
- **Surface:** #F9FAFB
- **Text Primary:** #111827
- **Text Secondary:** #6B7280

**Dark Mode:**
- **Primary:** #60A5FA
- **Success:** #34D399
- **Warning:** #FBBF24
- **Error:** #F87171
- **Background:** #111827
- **Surface:** #1F2937
- **Text Primary:** #F9FAFB
- **Text Secondary:** #D1D5DB

**High Contrast Mode:**
- WCAG AAA compliant (7:1 contrast ratio minimum)

#### Typography

**Font Family:**
- **Default:** System font stack (SF Pro on macOS, Segoe UI on Windows, Roboto on Android)
- **Dyslexia-friendly option:** OpenDyslexic

**Font Sizes:**
- **Heading 1:** 32px (2rem)
- **Heading 2:** 24px (1.5rem)
- **Heading 3:** 20px (1.25rem)
- **Body:** 16px (1rem) - base size
- **Small:** 14px (0.875rem)
- **Tiny:** 12px (0.75rem)

**Font Weights:**
- **Regular:** 400
- **Medium:** 500
- **Semibold:** 600
- **Bold:** 700

**Line Height:** 1.5-1.6 for body text

#### Spacing Scale
- **xs:** 4px
- **sm:** 8px
- **md:** 16px
- **lg:** 24px
- **xl:** 32px
- **2xl:** 48px

#### Border Radius
- **Small:** 4px (buttons, inputs)
- **Medium:** 8px (cards)
- **Large:** 12px (modals)
- **Full:** 9999px (circular elements)

---

### 8.2 Key Screen Layouts

#### Home Screen / Dashboard
**Components:**
1. **Top Bar**
   - App name/logo (left)
   - Current date and time
   - Sync status indicator
   - Settings icon (right)

2. **"What's Next" Section**
   - Current task (if in progress)
   - Next 3 upcoming tasks with times
   - Visual timeline
   - "Start" buttons

3. **Daily Progress Ring**
   - Circular progress indicator
   - Percentage completed
   - "X of Y tasks done"

4. **Streaks Display**
   - Fire emoji + "5 days" for active streaks
   - Encouraging message

5. **Quick Actions**
   - Start morning routine
   - Start bedtime routine
   - View pack list
   - Add task (floating action button)

6. **Bottom Navigation** (Mobile)
   - Home
   - Calendar
   - Tasks
   - Routines
   - More

---

#### Calendar Screen
**Components:**
1. **View Switcher**
   - Tabs: Day / Week / Month
   - Selected view highlighted

2. **Navigation**
   - Previous/Next buttons
   - "Today" button
   - Date range display

3. **Calendar Display**
   - Varies by view (day/week/month)
   - Color-coded tasks
   - Due date badges
   - Completion checkmarks

4. **Filters**
   - Toggle categories on/off
   - Show/hide school calendar
   - Show/hide completed tasks

5. **Search Bar**
   - Find tasks by keyword

---

#### Task Detail Screen
**Components:**
1. **Header**
   - Back button
   - Task title (editable)
   - Priority indicator
   - Category badge

2. **Main Content**
   - Description (editable)
   - Due date/time picker
   - Estimated duration slider
   - Category selector
   - Recurring settings (if applicable)

3. **Sub-tasks Section**
   - List of sub-tasks with checkboxes
   - Add sub-task button
   - Reorder controls
   - Progress bar

4. **Actions**
   - Start Pomodoro (large button)
   - Mark complete
   - Edit
   - Delete

5. **Metadata**
   - Created date
   - Last modified
   - From Teams (if applicable)
   - Link to Teams assignment

---

#### Pomodoro Timer Screen
**Components:**
1. **Timer Display**
   - Large countdown (e.g., "19:42")
   - Circular progress ring (animated)
   - Session type ("Work" or "Break")
   - Session number ("Session 1 of 3")

2. **Task Info**
   - Task name
   - Which sub-task (if applicable)

3. **Controls**
   - Pause/Resume button (large, center)
   - Stop button (with confirmation)
   - Extend +5 min button (max 2x)

4. **Focus Mode Toggle**
   - Hide all distractions
   - Show only timer

5. **Break Screen** (after work session)
   - "Great job! Take a break"
   - Break timer
   - Suggested activities
   - Skip break button (with warning)

---

#### Routine Screen
**Components:**
1. **Routine Selector**
   - Dropdown or tabs: Morning / Bedtime / Custom

2. **Routine Checklist**
   - Large checkboxes
   - Step names with icons
   - Estimated time per step
   - Current step highlighted

3. **Progress Bar**
   - "Step 3 of 7"
   - Percentage complete

4. **Timer** (for timed steps)
   - Countdown for current step
   - "Next" button to advance

5. **Completion Screen**
   - "Routine complete!"
   - Time taken vs. estimated
   - Streak indicator
   - Encouraging message

---

#### Pack List Screen
**Components:**
1. **Date Selector**
   - "Today" / "Tomorrow" toggle
   - Or specific date picker

2. **Pack Items Checklist**
   - Grouped by category
   - Large checkboxes
   - Item names with icons
   - Special highlight for homework items

3. **Quick Actions**
   - "Check all" button
   - "Reset for tomorrow" button
   - Add custom item

4. **Link to Calendar**
   - "View tomorrow's schedule"

---

### 8.3 Accessibility Requirements

#### Keyboard Navigation
- All interactive elements reachable via Tab
- Logical tab order
- Visible focus indicators
- Skip links for main content
- Keyboard shortcuts for common actions:
  - `Ctrl/Cmd + N`: New task
  - `Ctrl/Cmd + T`: Start timer
  - `Ctrl/Cmd + K`: Open calendar
  - `Space`: Toggle checkbox/complete task

#### Screen Reader Support (Phase 2)
- Proper ARIA labels on all interactive elements
- Semantic HTML (headings, lists, buttons)
- Alt text for all icons
- Live region announcements for timer, notifications
- Form validation errors announced

#### Color Contrast
- WCAG AAA compliance (7:1 ratio)
- Color not sole indicator of information
- High contrast mode option

#### Text & Layout
- Text scales up to 200% without breaking layout
- Minimum 44×44px touch targets
- No horizontal scrolling at normal zoom
- Content reflows properly on all screen sizes

---

### 8.4 Responsive Design

#### Breakpoints
- **Mobile:** < 640px (phone portrait)
- **Tablet:** 640px - 1024px (phone landscape, tablet)
- **Desktop:** > 1024px

#### Layout Adaptations

**Mobile:**
- Bottom navigation bar
- Single column layout
- Full-screen modals
- Swipe gestures (mark complete, delete)

**Tablet:**
- Side navigation or bottom bar (user choice)
- Two-column layout where appropriate
- Split view (calendar + task detail)

**Desktop:**
- Sidebar navigation
- Multi-column layouts
- Hover states and tooltips
- Keyboard shortcuts prominently displayed

---

## 9. Non-Functional Requirements

### 9.1 Performance

**Requirements:**
- **Page Load Time:** < 2 seconds on 4G connection
- **Database Queries:** < 100ms for reads, < 500ms for writes
- **UI Responsiveness:** Interactions respond within 100ms
- **Sync Time:** Teams sync completes in < 30 seconds for 50 assignments
- **Timer Accuracy:** Pomodoro timer accurate within ±1 second
- **Offline Mode:** App remains usable offline indefinitely

**Targets:**
- Support up to 1000 tasks in database without performance degradation
- Handle 100+ calendar events smoothly
- Render monthly calendar with 200+ items in < 1 second

---

### 9.2 Security

**Requirements:**
- **Authentication Tokens:** Encrypted storage (MAUI Secure Storage or equivalent)
- **Local Database:** SQLite encryption (optional, Phase 2)
- **HTTPS Only:** All network communication over HTTPS
- **No Plain Text Secrets:** No API keys or secrets in code
- **Minimal Permissions:** Request only necessary Microsoft Graph permissions
- **Token Expiry:** Automatic refresh of expired tokens
- **Logout:** Secure logout clears all tokens

**Compliance:**
- GDPR principles (data minimization, right to delete)
- COPPA compliance (parental consent for users under 13, if applicable)

---

### 9.3 Reliability

**Requirements:**
- **Data Integrity:** No data loss under any circumstances
- **Crash Recovery:** App state preserved and restored after crash
- **Sync Reliability:** Retry failed syncs with exponential backoff
- **Database Backups:** Auto-backup before major operations (Phase 2)
- **Error Logging:** All errors logged locally for troubleshooting

**Targets:**
- 99.9% uptime for local functionality (offline features)
- 95% Teams sync success rate (accounting for network issues)

---

### 9.4 Usability

**Requirements:**
- **Learnability:** New users can complete basic tasks (create task, start timer) without tutorial
- **Efficiency:** Frequent tasks (mark complete, start timer) accessible in ≤ 2 taps
- **Error Prevention:** Confirmation dialogs for destructive actions (delete task, clear data)
- **Error Recovery:** Clear error messages with suggested actions
- **Consistency:** Same interaction patterns throughout app

**Targets:**
- 80% of users successfully create and complete a task in first session
- 90% of users understand Pomodoro timer without instructions
- < 5% error rate on task creation

---

### 9.5 Maintainability

**Requirements:**
- **Code Quality:** Follow C# coding standards and best practices
- **Architecture:** Clean separation of concerns (MVVM or similar)
- **Documentation:** XML comments on public APIs
- **Testing:** 80%+ code coverage for business logic
- **Version Control:** Git with meaningful commit messages
- **Dependency Management:** NuGet packages kept up-to-date

---

### 9.6 Scalability

**Phase 1 (MVP):**
- Single-user application
- Local data storage only
- No cloud infrastructure needed

**Phase 2 (Multi-device):**
- Cloud sync for single user across devices
- OneDrive or iCloud backend
- Conflict resolution for concurrent edits

**Phase 3 (Multi-user - Future):**
- Family accounts (multiple students, shared parent view)
- Teacher/school integrations
- Scalable backend (Azure, AWS, etc.)

---

### 9.7 Compatibility

**Operating Systems:**
- **Phase 1 (Blazor Server):**
  - Windows 10/11 (Edge, Chrome, Firefox)
  - macOS 11+ (Safari, Chrome, Firefox)
  - Android 8.0+ (Chrome)
  - iOS 13+ (Safari) - browser only

- **Phase 2 (MAUI):**
  - Windows 10 1809+ / Windows 11
  - macOS 11+ (Big Sur)
  - Android 8.0+ (API 26)
  - iOS 15+ (if needed in future)

**Screen Sizes:**
- Minimum: 375px width (iPhone SE)
- Maximum: 4K displays (3840px)
- Optimal: 1920×1080 (desktop), 390×844 (mobile)

---

## 10. Release Phases & Timeline

### Phase 1: MVP (Months 1-3)

**Goal:** Functioning prototype with core features for single user on Blazor Server.

#### Month 1: Foundation
**Weeks 1-2:**
- ✅ Project setup (.NET 8, Blazor Server)
- ✅ Database schema design
- ✅ Entity Framework Core setup
- ✅ Basic UI layout and navigation
- ✅ Task CRUD operations

**Weeks 3-4:**
- ✅ Categories system (configurable)
- ✅ Sub-tasks and checklists
- ✅ Recurring tasks
- ✅ Daily/weekly/monthly calendar views
- ✅ Basic styling (responsive, accessible)

**Milestone:** Tasks can be created, organized, and viewed.

---

#### Month 2: Core Features
**Weeks 5-6:**
- ✅ Pomodoro timer implementation
- ✅ Automatic task breakdown (20-min chunks)
- ✅ Timer notifications (browser)
- ✅ Progress tracking (completion, streaks)

**Weeks 7-8:**
- ✅ Microsoft Teams authentication (MSAL)
- ✅ Graph API integration (assignments)
- ✅ Assignment sync logic
- ✅ School calendar import
- ✅ Error handling for sync

**Milestone:** Homework syncs from Teams, timer works reliably.

---

#### Month 3: Preparation & Routines
**Weeks 9-10:**
- ✅ Pack list system (items, recurring)
- ✅ Homework packing integration
- ✅ Packing reminders (evening/morning)
- ✅ Routine templates (morning, bedtime)
- ✅ Routine execution and tracking

**Weeks 11-12:**
- ✅ Notification system (all types)
- ✅ UI polish and accessibility review
- ✅ Performance optimization
- ✅ Bug fixes
- ✅ User testing with target user (Alex)
- ✅ Feedback incorporation

**Milestone:** MVP complete and functional. Ready for real-world use.

---

### Phase 2: MAUI Migration & Enhancements (Months 4-6)

**Goal:** Native cross-platform apps with offline support and enhanced features.

#### Month 4: MAUI Setup
- Convert to .NET MAUI Hybrid app
- Platform-specific notification implementation
- Offline-first architecture
- Local storage optimization
- Background sync service

**Milestone:** App runs natively on Windows, macOS, Android.

---

#### Month 5: Enhanced Features
- Parent/guardian portal
- Smart scheduling and AI suggestions
- Advanced homework management
- Analytics and insights dashboard

**Milestone:** App supports parent monitoring and intelligent task management.

---

#### Month 6: Cloud & Multi-Device
- Cloud sync (OneDrive/iCloud)
- Multi-device support
- Enhanced packing system (photos, etc.)
- Additional integrations (Google Classroom option)
- Final polish and testing

**Milestone:** Phase 2 complete. App ready for wider rollout.

---

### Phase 3: Advanced Features (Months 7+)

**Future Enhancements (as needed):**
- Social/collaboration features
- Advanced focus tools
- Gamification (optional)
- Smart home integrations
- iOS version (if needed)
- Web portal for parents/teachers

---

## 11. Dependencies & Risks

### 11.1 Dependencies

#### Technical Dependencies
- **Microsoft Graph API:** Availability and reliability of Teams education endpoints
- **.NET 8:** Requires .NET 8 SDK and runtime
- **SQLite:** Cross-platform database support
- **Internet Connection:** Required for Teams sync only
- **Microsoft School Account:** User must have valid school account with Teams access

#### External Dependencies
- **Microsoft Azure AD:** For authentication
- **School IT Policies:** Some schools may block third-party app access to Teams
- **Microsoft Graph API Changes:** API updates may require code changes

---

### 11.2 Risks & Mitigation

#### Risk 1: Microsoft Teams API Access
**Risk:** School IT may block third-party app access to Teams/Graph API.

**Impact:** High - Core feature unavailable.

**Likelihood:** Medium.

**Mitigation:**
- Provide manual homework entry as fallback
- Document required permissions for school IT
- Offer to work with school IT for approval
- Build app to work fully without Teams (degraded mode)

---

#### Risk 2: User Adoption & Engagement
**Risk:** User (teenager) may not consistently use the app.

**Impact:** High - App doesn't solve the problem.

**Likelihood:** Medium.

**Mitigation:**
- Involve user in design process
- Make app genuinely helpful, not burdensome
- Start with minimal features, add gradually
- Parent reminders and oversight (Phase 2)
- Positive reinforcement, not punishment

---

#### Risk 3: Over-Engineering / Scope Creep
**Risk:** Trying to build too much too fast.

**Impact:** Medium - Delays MVP, burnout.

**Likelihood:** Medium.

**Mitigation:**
- Strict MVP scope (this PRD defines it)
- Phase-based approach
- User testing after each phase
- "Build, measure, learn" iterative approach

---

#### Risk 4: Platform Compatibility Issues
**Risk:** MAUI bugs or platform-specific issues.

**Impact:** Medium - Delays Phase 2.

**Likelihood:** Low-Medium.

**Mitigation:**
- Start with Blazor Server (proven technology)
- Test MAUI early in Phase 2
- Leverage community and Microsoft support
- Have fallback: continue Blazor Server if MAUI problematic

---

#### Risk 5: Performance with Large Data Sets
**Risk:** App slows down with hundreds of tasks/sessions.

**Impact:** Medium - Poor user experience.

**Likelihood:** Low.

**Mitigation:**
- Performance testing throughout development
- Database indexing
- Pagination for large lists
- Archive old data
- Lazy loading

---

#### Risk 6: Security Vulnerabilities
**Risk:** Token theft, data leaks, etc.

**Impact:** High - User data compromised.

**Likelihood:** Low.

**Mitigation:**
- Use proven libraries (MSAL, Secure Storage)
- Follow Microsoft security best practices
- Code review for security
- Penetration testing (Phase 2)
- Regular dependency updates

---

## 12. Open Questions

### 12.1 Design Decisions
1. **Component Library:** MudBlazor vs. Radzen vs. custom components?
   - **Recommendation:** Evaluate both, prioritize accessibility and customization.

2. **Timer Persistence:** Should Pomodoro timer continue running if app is closed?
   - **Recommendation:** Yes, with notification when complete.

3. **Homework Auto-Breakdown:** Opt-in or automatic with opt-out?
   - **Recommendation:** Automatic with clear "No thanks" option.

4. **Parent Portal Access:** Separate app, same app different login, or access code?
   - **Recommendation:** Same app, access code (simplest for Phase 2).

---

### 12.2 Technical Decisions
1. **Hosting (Phase 1):** Local server or Azure?
   - **Recommendation:** Local (Docker) for development, Azure for testing/demo.

2. **Database Encryption:** Necessary for MVP?
   - **Recommendation:** No for MVP, yes for Phase 2.

3. **Background Sync Frequency:** 15min, 30min, or 1hr?
   - **Recommendation:** 30min default, user configurable.

4. **Token Storage (Blazor Server):** Session-based or persisted?
   - **Recommendation:** Persisted (server-side encrypted), auto-refresh.

---

### 12.3 User Experience Questions
1. **Notification Sound:** Custom or system default?
   - **Recommendation:** System default initially, custom library in Phase 2.

2. **Break Enforcement:** Force breaks or allow skipping?
   - **Recommendation:** Strongly encourage (modal) but allow skip with warning.

3. **Streak Failure:** What happens if user forgets one day?
   - **Recommendation:** Gentle message, offer to keep trying, no punishment.

4. **Completion Celebrations:** How enthusiastic? Risk of being patronizing?
   - **Recommendation:** Subtle and customizable. User testing will guide this.

---

### 12.4 Integration Questions
1. **Google Classroom:** Support in MVP or Phase 2?
   - **Recommendation:** Phase 2. Focus on Teams for MVP.

2. **Calendar Export:** Should users be able to export to Google/Apple Calendar?
   - **Recommendation:** Phase 2 enhancement.

3. **Wearables (Apple Watch):** Worth supporting?
   - **Recommendation:** Phase 3, after Android/iOS apps stable.

---

## 13. Success Criteria for MVP

The MVP will be considered successful if:

1. ✅ **User (teenager) uses app daily** for at least 2 weeks
2. ✅ **Homework completion rate improves** (measurable via parent feedback)
3. ✅ **Forgotten items decrease** to < 1 per week
4. ✅ **Pomodoro timer is used** for at least 50% of homework sessions
5. ✅ **Teams sync works reliably** with > 90% success rate
6. ✅ **No critical bugs** that block core functionality
7. ✅ **User feedback is positive** (≥ 4/5 stars from user and parent)
8. ✅ **Morning/bedtime routines are followed** at least 70% of the time

If these criteria are met, proceed to Phase 2 (MAUI migration and enhancements).

---

## 14. Appendix

### 14.1 Glossary

- **Neurodiverse:** Individuals with cognitive differences such as ADHD, autism, dyslexia, etc.
- **Executive Function:** Mental skills for goal-directed behavior (planning, organization, time management, etc.)
- **Pomodoro Technique:** Time management method using 25-minute work sessions (adapted to 20 minutes for this app)
- **Time Blindness:** Difficulty accurately perceiving and tracking time
- **Prospective Memory:** Remembering to do something in the future
- **Scaffolding:** Support structures that can be gradually removed as skills develop
- **Microsoft Graph API:** Microsoft's unified API for accessing Microsoft 365 services
- **MSAL:** Microsoft Authentication Library for OAuth 2.0
- **Blazor:** Microsoft's framework for building web UIs with C# instead of JavaScript
- **MAUI:** .NET Multi-platform App UI, framework for cross-platform apps

---

### 14.2 References

**Research & Best Practices:**
- Tiimo App (visual planner for ADHD/autism)
- Neuro-inclusive design principles
- Visual schedules for autism and ADHD (research-backed)
- Microsoft Graph Education API documentation
- WCAG 2.1 AAA accessibility standards

**Technical Documentation:**
- Microsoft Graph API: https://learn.microsoft.com/en-us/graph/
- .NET MAUI: https://learn.microsoft.com/en-us/dotnet/maui/
- Blazor: https://learn.microsoft.com/en-us/aspnet/core/blazor/

---

### 14.3 Version History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0 | Oct 23, 2025 | Product Team | Initial PRD for MVP planning |

---

**END OF PRODUCT REQUIREMENTS DOCUMENT**
