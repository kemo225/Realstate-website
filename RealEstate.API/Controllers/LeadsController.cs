using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RealEstate.Application.Features.Leads.Commands.CreateLead;
using RealEstate.Application.Features.Leads.Models;
using RealEstate.Application.Features.Leads.Queries.GetLeads;
using RealEstate.Application.Features.Leads.Queries.GetLeadById;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.API.Controllers;

public class LeadsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PaginatedList<LeadDto>>>> GetAll([FromQuery] GetLeadsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(new ApiResponse<PaginatedList<LeadDto>> { Success = true, Data = result });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<LeadDto>>> GetById(int id)
    {
        var result = await Mediator.Send(new GetLeadByIdQuery(id));
        return Ok(new ApiResponse<LeadDto> { Success = true, Data = result });
    }

    [HttpPost]
    public async Task<ActionResult<Result<int>>> CreateLead(CreateLeadCommand command)
    {
        return await Mediator.Send(command);
    }



    [HttpPut("cancel/lead/{LeadId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Result>> DeleteLead(int LeadId)
    {
        var result = await Mediator.Send(new UpdateLeadViewdCommand(LeadId, enStatusLead.cancelled));
        return Ok(result);
    }
    [HttpPut("view/lead/{LeadId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Result>> ViewdLead(int LeadId)
    {
        var result = await Mediator.Send(new UpdateLeadViewdCommand(LeadId, enStatusLead.Viewed));
        return Ok(result);
    }

}
