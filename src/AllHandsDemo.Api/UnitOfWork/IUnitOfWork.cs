using AllHandsDemo.Api.Repositories;

namespace AllHandsDemo.Api.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository Employees { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
