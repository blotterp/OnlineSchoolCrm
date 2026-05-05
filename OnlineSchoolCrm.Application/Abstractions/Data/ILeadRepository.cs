using OnlineSchoolCrm.Domain.Crm;

namespace OnlineSchoolCrm.Application.Abstractions.Data;

public interface ILeadRepository
{
    void Add(Lead lead);

    Task<Lead?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Lead>> GetAllAsync(CancellationToken cancellationToken);
}