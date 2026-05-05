using OnlineSchoolCrm.Domain.Crm;

namespace OnlineSchoolCrm.Application.Crm.Leads.GetLead;

public sealed record LeadResponse(
    Guid Id,
    string ParentName,
    string Phone,
    string? Email,
    string? ChildName,
    int? ChildAge,
    string? CourseInterest,
    LeadStatus Status,
    DateTimeOffset CreatedAt);