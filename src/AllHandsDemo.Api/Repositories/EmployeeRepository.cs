using AllHandsDemo.Api.Data;
using AllHandsDemo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AllHandsDemo.Api.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Employee?> GetByUserNameAsync(string userName)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.UserName == userName);
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<bool> IsUserNameUniqueAsync(string userName, int? excludeId = null)
    {
        var query = _dbSet.Where(e => e.UserName == userName);
        
        if (excludeId.HasValue)
        {
            query = query.Where(e => e.Id != excludeId.Value);
        }
        
        return !await query.AnyAsync();
    }

    public async Task<bool> IsEmailUniqueAsync(string email, int? excludeId = null)
    {
        var query = _dbSet.Where(e => e.Email == email);
        
        if (excludeId.HasValue)
        {
            query = query.Where(e => e.Id != excludeId.Value);
        }
        
        return !await query.AnyAsync();
    }
}
