using MediatR;
using OnlineSchoolCrm.Application.Abstractions.Data;
using OnlineSchoolCrm.Application.Crm;

namespace OnlineSchoolCrm.Application.Crm.Leads.GetLead;

public sealed class GetLeadByIdQueryHandler : IRequestHandler<GetLeadByIdQuery, LeadResponse?>
{
    private readonly ILeadRepository _leadRepository;

    public GetLeadByIdQueryHandler(ILeadRepository leadRepository) 
    {
        _leadRepository = leadRepository;
    }

    public async Task<LeadResponse?> Handle(
        GetLeadByIdQuery request,
        CancellationToken cancellationToken)
    {
        var lead = await _leadRepository.GetByIdAsync(request.Id, cancellationToken);
        if (lead is null)
            return null;

        return new LeadResponse(
            lead.Id,
            lead.ParentName,
            lead.Phone,
            lead.Email,
            lead.ChildName,
            lead.ChildAge,
            lead.CourseInterest,
            lead.Status,
            lead.CreatedAt
            );
    }

}