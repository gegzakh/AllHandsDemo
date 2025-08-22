using AllHandsDemo.Api.DTOs;
using AllHandsDemo.Api.Models;
using AllHandsDemo.Api.UnitOfWork;

namespace AllHandsDemo.Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();
        return employees.Select(MapToDto);
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        return employee != null ? MapToDto(employee) : null;
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
    {
        // Validate unique constraints
        if (!await _unitOfWork.Employees.IsUserNameUniqueAsync(createEmployeeDto.UserName))
        {
            throw new InvalidOperationException($"UserName '{createEmployeeDto.UserName}' is already taken.");
        }

        if (!await _unitOfWork.Employees.IsEmailUniqueAsync(createEmployeeDto.Email))
        {
            throw new InvalidOperationException($"Email '{createEmployeeDto.Email}' is already registered.");
        }

        var employee = new Employee
        {
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            UserName = createEmployeeDto.UserName,
            Email = createEmployeeDto.Email,
            Age = createEmployeeDto.Age,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(employee);
    }

    public async Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateEmployeeDto)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (employee == null)
        {
            return null;
        }

        // Validate unique constraints if values are being updated
        if (!string.IsNullOrEmpty(updateEmployeeDto.UserName) && 
            updateEmployeeDto.UserName != employee.UserName)
        {
            if (!await _unitOfWork.Employees.IsUserNameUniqueAsync(updateEmployeeDto.UserName, id))
            {
                throw new InvalidOperationException($"UserName '{updateEmployeeDto.UserName}' is already taken.");
            }
            employee.UserName = updateEmployeeDto.UserName;
        }

        if (!string.IsNullOrEmpty(updateEmployeeDto.Email) && 
            updateEmployeeDto.Email != employee.Email)
        {
            if (!await _unitOfWork.Employees.IsEmailUniqueAsync(updateEmployeeDto.Email, id))
            {
                throw new InvalidOperationException($"Email '{updateEmployeeDto.Email}' is already registered.");
            }
            employee.Email = updateEmployeeDto.Email;
        }

        // Update other fields if provided
        if (!string.IsNullOrEmpty(updateEmployeeDto.FirstName))
        {
            employee.FirstName = updateEmployeeDto.FirstName;
        }

        if (!string.IsNullOrEmpty(updateEmployeeDto.LastName))
        {
            employee.LastName = updateEmployeeDto.LastName;
        }

        if (updateEmployeeDto.Age.HasValue)
        {
            employee.Age = updateEmployeeDto.Age.Value;
        }

        employee.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Employees.Update(employee);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(employee);
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (employee == null)
        {
            return false;
        }

        _unitOfWork.Employees.Remove(employee);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private static EmployeeDto MapToDto(Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            UserName = employee.UserName,
            Email = employee.Email,
            Age = employee.Age,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt
        };
    }
}
