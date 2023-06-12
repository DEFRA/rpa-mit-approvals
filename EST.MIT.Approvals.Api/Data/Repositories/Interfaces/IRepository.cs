using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

public interface IRepository<T>
    where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task HardDeleteAsync(int id);

    Task SoftDeleteAsync(int id);

    Task<T?> GetAsync(int id);

    Task<int> InsertRangeAsync(IEnumerable<T> list);

    Task UpdateAsync(T t);

    Task<int> InsertAsync(T t);
}
