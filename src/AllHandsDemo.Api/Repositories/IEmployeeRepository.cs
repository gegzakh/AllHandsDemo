using AllHandsDemo.Api.Models;

namespace AllHandsDemo.Api.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetByUserNameAsync(string userName);
    Task<Employee?> GetByEmailAsync(string email);
    Task<bool> IsUserNameUniqueAsync(string userName, int? excludeId = null);
    Task<bool> IsEmailUniqueAsync(string email, int? excludeId = null);
}
