using OnlineSchoolCrm.Domain.Crm;

namespace OnlineSchoolCrm.Application.Abstractions.Data;

public interface ILeadRepository
{
    void Add(Lead lead);
}