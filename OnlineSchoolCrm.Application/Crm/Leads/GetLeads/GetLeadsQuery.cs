using MediatR;
using OnlineSchoolCrm.Application.Crm.Leads.GetLead;

namespace OnlineSchoolCrm.Application.Crm.Leads.GetLeads;

public sealed record GetLeadsQuery : IRequest<IReadOnlyCollection<LeadResponse>>;