using FluentValidation;

namespace OnlineSchoolCrm.Application.Crm.Leads.CreateLead;

public sealed class CreateLeadCommandValidator : AbstractValidator<CreateLeadCommand>
{
    public CreateLeadCommandValidator()
    { 
        RuleFor(x => x.ParentName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(320)
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ChildName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ChildAge)
            .InclusiveBetween(7, 18)
            .When(x => x.ChildAge.HasValue);

        RuleFor(x => x.CourseInterest)
            .MaximumLength(500);

    }
}