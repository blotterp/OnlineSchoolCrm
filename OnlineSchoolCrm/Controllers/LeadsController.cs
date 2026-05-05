using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolCrm.Application.Crm.Leads.CreateLead;

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

        public IActionResult GetById(Guid id)
        {
            return Ok(new { Id = id });
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
