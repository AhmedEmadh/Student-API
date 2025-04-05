# Student-API

Student-API is an **ASP.NET Core Web API** designed to manage student information. The API utilizes **stored procedures** for database operations and **DTOs (Data Transfer Objects)** for efficient data handling, ensuring separation of concerns and maintaining a clean architecture.

## Features

- **Student Management**: Create, update, retrieve, and delete student records.

- **DTOs**: **DTOs** are used to transfer data between layers, promoting better data management, security, and separation of concerns.

- **Stored Procedures**: The API uses **stored procedures** for all CRUD operations, ensuring optimal performance and security.

- **ASP.NET Core Web API**: Built using **ASP.NET Core Web API** for scalability, performance, and cross-platform support.

## Technologies Used

- **ASP.NET Core Web API**: The framework for building the API, ensuring high performance, scalability, and cross-platform support.

- **Entity Framework Core**: ORM for database interactions, executing database operations using **stored procedures**.

- **DTO (Data Transfer Object)**: A pattern used to ensure secure, efficient data transfer between layers while maintaining data integrity.

- **SQL Server**: Relational database used for storing and processing student data via **stored procedures**.

## Getting Started

Follow these steps to set up the **Student-API** project locally:

### Prerequisites

Ensure the following are installed:

- [.NET Core SDK](https://dotnet.microsoft.com/download)

- SQL Server (local or remote) for managing database operations and running the stored procedures

- Visual Studio or your preferred IDE for editing and running the project

### Installation

1. **Clone the Repository**:
   
   Clone the repository to your local machine:
   
   `git clone https://github.com/AhmedEmadh/Student-API.git`

2. **Navigate to the Project Directory**:
   
   Change to the project folder:
   
   `cd Student-API`

3. **Restore Dependencies**:
   
   Restore all required NuGet packages:
   
   `dotnet restore`

4. **Set Up the Database**:
   
   - Ensure your SQL Server instance is running.
   
   - Import the **stored procedures** from the `Database/StoredProcedures` folder (or equivalent) into your SQL Server instance.
   
   - These stored procedures are used to interact with the database for CRUD operations.

5. Build the Solution:
   Build the project to ensure all dependencies are properly configured:
   `dotnet build`

6. **Run the Application**:
   
   Start the API using the following command:
   
   `dotnet run`
   
   The API will now be available at `https://localhost:5001` (or the configured port).

## Project Structure

The project follows a layered architecture, separating concerns for better maintainability and clarity:

- **StudentAPIServer**: Contains the API controllers and handles HTTP requests.

- **StudentAPIBusinessLayer**: Implements business logic, interacting with stored procedures and utilizing DTOs.

- **StudentAPIDataAccessLayer**: Handles database operations through **stored procedures**.

- **DTOs**: Data Transfer Objects used to decouple the database and API response models.

## Example DTO

```csharp
public class StudentDTO
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Grade { get; set; }
}
```