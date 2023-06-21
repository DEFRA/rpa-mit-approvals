using EST.MIT.Approvals.Data.Models;
using System.Linq.Expressions;
using System;

namespace EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

public interface IRepository<T>
    where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetAsync(int id);
}
