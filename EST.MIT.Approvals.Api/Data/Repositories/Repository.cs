using Approvals.Api.Data.Entities;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

namespace Approvals.Api.Data.Repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    private IList<T> _entities;

    protected Repository()
    {
        this._entities = new List<T>();
    }

    protected void Initialise(IList<T> entities)
    {
        this._entities = entities;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Task.Run(() => this._entities);
    }

    public async Task HardDeleteAsync(int id)
    {
        await Task.Run(() =>
        {
            if (this._entities.Any())
            {
                this._entities = this._entities.Where(x => x.Id != id).ToList();
            }
        });
    }

    public async Task SoftDeleteAsync(int id)
    {
        await Task.Run(() =>
        {
            var entity = this._entities.FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return;
            }

            entity.IsDeleted = true;
        });
    }

    public async Task<T?> GetAsync(int id)
    {
        return await Task.Run(() =>
        {
            if (!this._entities.Any())
            {
                return null;
            }

            return this._entities.FirstOrDefault(x => x.Id == id);

        });
    }
    
    public async Task<int> InsertRangeAsync(IEnumerable<T> list)
    {
        throw new NotImplementedException();
    }

    public async Task<int> InsertAsync(T t)
    {
        throw new NotImplementedException();
    }
    

    public async Task UpdateAsync(T t)
    {
        throw new NotImplementedException();
    }
}
