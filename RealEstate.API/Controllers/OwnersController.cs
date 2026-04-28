using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.Owners.Commands.CreateOwner;
using RealEstate.Application.Features.Owners.Commands.UpdateOwner;
using RealEstate.Application.Features.Owners.Commands.DeleteOwner;
using RealEstate.Application.Features.Owners.Queries.GetOwners;
using RealEstate.Application.Features.Owners.Queries.GetOwnerById;
using RealEstate.Application.Common.Models;

namespace RealEstate.API.Controllers;

public class OwnersController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<OwnerDto>>> GetOwners([FromQuery] GetOwnersQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OwnerDto>> GetOwnerById(int id)
    {
        var result = await Mediator.Send(new GetOwnerByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<int>>> CreateOwner(CreateOwnerCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result<int>>> UpdateOwner(int id, UpdateOwnerCommand command)
    {
        if (id != command.Id) return BadRequest();
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<bool>>> DeleteOwner(int id)
    {
        return await Mediator.Send(new DeleteOwnerCommand(id));
    }
}
