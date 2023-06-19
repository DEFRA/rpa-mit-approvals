using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Api.Data.Repositories;

[ExcludeFromCodeCoverage]
public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ApprovalsContext Context;


    protected Repository(ApprovalsContext context)
    {
        this.Context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await this.Context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetAsync(int id)
    {
        return await this.Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
