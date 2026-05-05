using MediatR;
using OnlineSchoolCrm.Application.Abstractions.Data;
using OnlineSchoolCrm.Application.Crm.Leads.GetLead;

namespace OnlineSchoolCrm.Application.Crm.Leads.GetLeads;

public sealed class GetLeadsQueryHandler
    : IRequestHandler<GetLeadsQuery, IReadOnlyCollection<LeadResponse>>
{
    private readonly ILeadRepository _leadRepository;

    public GetLeadsQueryHandler(ILeadRepository leadRepository)
    {
        _leadRepository = leadRepository;
    }

    public async Task<IReadOnlyCollection<LeadResponse>> Handle(
        GetLeadsQuery request,
        CancellationToken cancellationToken)
    {
        var leads = await _leadRepository.GetAllAsync(cancellationToken);

        return leads
            .Select(lead => new LeadResponse(
                lead.Id,
                lead.ParentName,
                lead.Phone,
                lead.Email,
                lead.ChildName,
                lead.ChildAge,
                lead.CourseInterest,
                lead.Status,
                lead.CreatedAt))
            .ToList();
    }
}