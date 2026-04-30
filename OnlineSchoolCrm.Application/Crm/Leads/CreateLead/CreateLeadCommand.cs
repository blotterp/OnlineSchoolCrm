using MediatR;

namespace OnlineSchoolCrm.Application.Crm.Leads.CreateLead;

public sealed record CreateLeadCommand(
    string ParentName,
    string Phone,
    string? Email,
    string? ChildName,
    int? ChildAge,
    string? CourseInterest
    ) : IRequest<Guid>;