
# Student Management System

A console-based Student Management System developed using VB.NET and SQLite.

## Features

- Add new student records
- View all student records
- Search students using Student ID
- Update student information
- Delete student records
- Prevent duplicate Student IDs
- Validate user inputs
- Permanently store records using SQLite

## Technologies Used

- VB.NET
- .NET 10 SDK
- SQLite
- Microsoft.Data.Sqlite
- Visual Studio Code

## Project Structure

StudentManagementSystem/
│
├── Program.vb
├── Student.vb
├── Database.vb
├── StudentManagementSystem.vbproj
└── README.md

## How to Run

1. Install the .NET SDK.
2. Clone or download this repository.
3. Open the project folder in VS Code.
4. Open the terminal.
5. Restore dependencies:

   dotnet restore

6. Run the application:

   dotnet run

## Database

The application automatically creates the SQLite database
and Students table when it starts.

## Operations

The application supports Create, Read, Update and Delete
(CRUD) operations for student records.

## Developer

Developed as a VB.NET mini project.
