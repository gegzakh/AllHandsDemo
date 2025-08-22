# AllHandsDemo - Employee Management API

A .NET Core 8 Web API microservice for managing employees with full CRUD operations, built using Repository pattern with Unit of Work, PostgreSQL database, and comprehensive testing.

## Features

- **CRUD Operations**: Complete Create, Read, Update, Delete operations for Employee entity
- **Repository Pattern**: Implements Repository pattern with Unit of Work for data access
- **PostgreSQL Integration**: Uses PostgreSQL as the primary database with Entity Framework Core
- **Auto-Migration**: Automatically applies database migrations on startup
- **Environment Configuration**: Separate configurations for Development, Staging, and Production
- **Swagger Documentation**: Interactive API documentation and testing interface
- **Comprehensive Testing**: Unit tests with Moq and integration tests with in-memory database
- **Input Validation**: Data annotations and business logic validation
- **Error Handling**: Proper HTTP status codes and error responses

## Project Structure

```
AllHandsDemo/
├── src/
│   └── AllHandsDemo.Api/           # Main API project
│       ├── Controllers/            # API controllers
│       ├── Data/                   # Database context
│       ├── DTOs/                   # Data Transfer Objects
│       ├── Models/                 # Entity models
│       ├── Repositories/           # Repository implementations
│       ├── Services/               # Business logic services
│       └── UnitOfWork/            # Unit of Work pattern
└── tests/
    └── AllHandsDemo.Tests/         # Test project
        ├── UnitTests/              # Unit tests
        └── IntegrationTests/       # Integration tests
```

## Employee Entity

The Employee entity includes the following fields:
- **Id**: Auto-generated primary key
- **FirstName**: Required, max 100 characters
- **LastName**: Required, max 100 characters
- **UserName**: Required, unique, max 50 characters
- **Email**: Required, unique, valid email format, max 255 characters
- **Age**: Required, range 18-120
- **CreatedAt**: Auto-generated timestamp
- **UpdatedAt**: Auto-updated timestamp

## Prerequisites

- .NET 8.0 SDK
- PostgreSQL Server
- Visual Studio 2022 or VS Code (optional)

## Setup Instructions

### 1. Database Setup

Ensure PostgreSQL is running with the following default credentials:
- **Host**: localhost
- **Port**: 5432
- **Username**: postgres
- **Password**: postgres

The application will create the following databases automatically:
- `AllHandsDemo_Dev` (Development)
- `AllHandsDemo_Staging` (Staging)
- `AllHandsDemo_Prod` (Production)

### 2. Build and Run

```bash
# Navigate to the solution directory
cd AllHandsDemo

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the API (Development environment)
cd src/AllHandsDemo.Api
dotnet run
```

### 3. Access the API

- **Swagger UI**: http://localhost:5000 (Development)
- **API Base URL**: http://localhost:5000/api

## API Endpoints

### Employees

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/employees` | Get all employees |
| GET | `/api/employees/{id}` | Get employee by ID |
| POST | `/api/employees` | Create new employee |
| PUT | `/api/employees/{id}` | Update existing employee |
| DELETE | `/api/employees/{id}` | Delete employee |

### Example Requests

#### Create Employee
```json
POST /api/employees
{
  "firstName": "John",
  "lastName": "Doe",
  "userName": "johndoe",
  "email": "john.doe@example.com",
  "age": 30
}
```

#### Update Employee
```json
PUT /api/employees/1
{
  "firstName": "John",
  "lastName": "Smith",
  "age": 31
}
```

## Environment Configuration

The application supports three environments with separate database configurations:

- **Development**: Enhanced logging, detailed error messages
- **Staging**: Production-like settings with moderate logging
- **Production**: Minimal logging, optimized for performance

Set the environment using:
```bash
export ASPNETCORE_ENVIRONMENT=Development  # Linux/Mac
set ASPNETCORE_ENVIRONMENT=Development     # Windows
```

## Testing

### Run Unit Tests
```bash
cd tests/AllHandsDemo.Tests
dotnet test --filter "Category=Unit"
```

### Run Integration Tests
```bash
cd tests/AllHandsDemo.Tests
dotnet test --filter "Category=Integration"
```

### Run All Tests
```bash
dotnet test
```

## Technologies Used

- **.NET 8.0**: Latest LTS version of .NET
- **ASP.NET Core Web API**: RESTful API framework
- **Entity Framework Core 8.0**: ORM for database operations
- **PostgreSQL**: Primary database
- **Npgsql**: PostgreSQL provider for EF Core
- **Swagger/OpenAPI**: API documentation
- **xUnit**: Testing framework
- **Moq**: Mocking framework for unit tests
- **Microsoft.AspNetCore.Mvc.Testing**: Integration testing

## Architecture Patterns

- **Repository Pattern**: Abstracts data access logic
- **Unit of Work**: Manages transactions and coordinates repositories
- **Dependency Injection**: Built-in ASP.NET Core DI container
- **DTO Pattern**: Separates internal models from API contracts
- **Service Layer**: Contains business logic and validation

## Error Handling

The API returns appropriate HTTP status codes:
- **200 OK**: Successful GET/PUT operations
- **201 Created**: Successful POST operations
- **204 No Content**: Successful DELETE operations
- **400 Bad Request**: Validation errors or business rule violations
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Unexpected server errors

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add/update tests
5. Ensure all tests pass
6. Submit a pull request

## License

This project is licensed under the MIT License.
