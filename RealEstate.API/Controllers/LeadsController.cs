using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RealEstate.Application.Features.Leads.Commands.CreateLead;
using RealEstate.Application.Features.Leads.Commands.DeleteLead;
using RealEstate.Application.Features.Leads.Models;
using RealEstate.Application.Features.Leads.Queries.GetLeads;
using RealEstate.Application.Features.Leads.Queries.GetLeadById;
using RealEstate.Application.Common.Models;

namespace RealEstate.API.Controllers;

public class LeadsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<LeadDto>>> GetLeads([FromQuery] GetLeadsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeadDto>> GetLeadById(int id)
    {
        var result = await Mediator.Send(new GetLeadByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<int>>> CreateLead(CreateLeadCommand command)
    {
        return await Mediator.Send(command);
    }



    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Result>> DeleteLead(int id)
    {
        return await Mediator.Send(new DeleteLeadCommand(id));
    }

}
