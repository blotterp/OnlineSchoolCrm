using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolCrm.Application.Crm.Leads.CreateLead;
using OnlineSchoolCrm.Application.Crm.Leads.GetLead;
using OnlineSchoolCrm.Application.Crm.Leads.GetLeads;


namespace OnlineSchoolCrm.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public sealed class LeadsController : ControllerBase
    {
        private readonly ISender _sender;

        public LeadsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateLeadRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateLeadCommand(
            request.ParentName,
            request.Phone,
            request.Email,
            request.ChildName,
            request.ChildAge,
            request.CourseInterest
            );
            var leadId = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = leadId },
                new CreateLeadResponse(leadId));
        }

        [HttpGet("{id:guid}")]

        public async Task <IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var lead = await _sender.Send(new GetLeadByIdQuery(id, cancellationToken));

            if (lead is null)
                return NotFound();

            return Ok(lead);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var leads = await _sender.Send(new GetLeadsQuery(), cancellationToken);

            return Ok(leads);
        }



        public record CreateLeadRequest
        (
            string ParentName,
            string Phone,
            string? Email,
            string? ChildName,
            int? ChildAge,
            string? CourseInterest
        );

        public sealed record CreateLeadResponse(Guid id);
    }
}
