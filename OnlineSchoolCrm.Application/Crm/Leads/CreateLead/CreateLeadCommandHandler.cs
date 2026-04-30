using MediatR;
using OnlineSchoolCrm.Application.Abstractions.Data;
using OnlineSchoolCrm.Domain.Crm;
using OnlineSchoolCrm.Domain.Tenant;

namespace OnlineSchoolCrm.Application.Crm.Leads.CreateLead;
public sealed class CreateLeadCommandHandler 
    : IRequestHandler<CreateLeadCommand, Guid>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ILeadRepository _leadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLeadCommandHandler(
        ITenantRepository tenantRepository,
        ILeadRepository leadRepository,
        IUnitOfWork unitOfWork)
    {
        _tenantRepository = tenantRepository;
        _leadRepository = leadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateLeadCommand request,
        CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetActiveTenantAsync(
            cancellationToken);

        if (tenant is null)
        {
            tenant = new Tenant("Main School");
            _tenantRepository.Add(tenant);
        }

        var lead = new Lead(
            tenant.Id,
            request.ParentName,
            request.Phone,
            request.Email,
            request.ChildName,
            request.ChildAge,
            request.CourseInterest);

        _leadRepository.Add(lead);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return lead.Id;
    }
}
