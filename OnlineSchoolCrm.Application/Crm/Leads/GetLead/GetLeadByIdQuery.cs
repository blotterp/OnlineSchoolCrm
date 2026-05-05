using MediatR;


namespace OnlineSchoolCrm.Application.Crm.Leads.GetLead;

public sealed record GetLeadByIdQuery(Guid Id, CancellationToken cancellationToken) : IRequest<LeadResponse?>;
