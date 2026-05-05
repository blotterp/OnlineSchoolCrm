using Microsoft.EntityFrameworkCore;
using OnlineSchoolCrm.Application.Abstractions.Data;
using OnlineSchoolCrm.Domain.Crm;
using OnlineSchoolCrm.Persistence.Database;

namespace OnlineSchoolCrm.Persistence.Repositories;

public sealed class LeadRepository : ILeadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LeadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Lead lead)
    {
        _dbContext.Leads.Add(lead);
    }

    public Task<Lead?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Leads
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<IReadOnlyCollection<Lead>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Leads
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
