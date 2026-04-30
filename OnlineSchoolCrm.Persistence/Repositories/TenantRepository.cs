using Microsoft.EntityFrameworkCore;
using OnlineSchoolCrm.Application.Abstractions.Data;
using OnlineSchoolCrm.Domain.Tenant;
using OnlineSchoolCrm.Persistence.Database;

namespace OnlineSchoolCrm.Persistence.Repositories;

public sealed class TenantRepository : ITenantRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TenantRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Tenant?> GetActiveTenantAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Tenants
            .FirstOrDefaultAsync(x => x.IsActive, cancellationToken);
    }

    public void Add(Tenant tenant)
    {
            _dbContext.Tenants .Add(tenant); 
    }
}