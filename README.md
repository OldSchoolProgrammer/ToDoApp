# ASP.NET Core TodoCalendar

A comprehensive Task Management and Calendar application built with ASP.NET Core MVC 8.0. This application allows users to manage tasks, categorize them, and view them on an interactive calendar.

![Framework](https://img.shields.io/badge/.NET-8.0-purple)
![License](https://img.shields.io/badge/License-MIT-blue)

## Features

- **Task Management**:
    - Create, Edit, Delete, and View tasks.
    - Set due dates, priorities (High, Medium, Low), and completion status.
    - Filter tasks by Category, Priority, and Status.
- **Categorization**:
    - Organize tasks into custom categories (e.g., Work, Personal, Health).
    - Color-coded categories for easy visual identification.
- **Interactive Calendar**:
    - Powered by **FullCalendar v6**.
    - Visualizes tasks on a monthly/weekly view based on due dates.
    - Tasks are color-coded by category or priority.
- **Dashboard**:
    - Overview of upcoming and important tasks.

## Technology Stack

- **Framework**: .NET 8.0 (ASP.NET Core MVC)
- **Database**: SQLite
- **ORM**: Entity Framework Core
- **Frontend**:
    - Bootstrap 5
    - Bootstrap Icons
    - FullCalendar.js
    - jQuery & jQuery Validation

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Installation & Run

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/OldSchoolProgrammer/ToDoApp.git
    cd ToDoApp
    ```

2.  **Run the application**:
    ```bash
    cd TodoCalendar
    dotnet run
    ```

    The application will automatically create the SQLite database (`todoapp.db`) and seed it with initial data upon the first run.

3.  **Access the app**:
    Open your browser and navigate to the URL shown in the terminal (usually `http://localhost:5000` or `https://localhost:7001`).

## Project Structure

- **Controllers**: Handles business logic for Calendar, Categories, and Tasks.
- **Models**: Entity definitions (Category, TodoTask).
- **Views**: Razor views used for the UI.
- **wwwroot**: Static resources (CSS, JS, Libraries).

## License

This project is licensed under the MIT License.
