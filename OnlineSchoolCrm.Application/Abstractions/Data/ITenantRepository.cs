using OnlineSchoolCrm.Domain.Tenant;

namespace OnlineSchoolCrm.Application.Abstractions.Data;

public interface ITenantRepository
{
    Task<Tenant> GetActiveTenantAsync(CancellationToken cancellationToken);
    void Add(Tenant tenant);
}
