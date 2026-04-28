using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.Facilities.Commands.CreateFacility;
using RealEstate.Application.Features.Facilities.Commands.DeleteFacility;
using RealEstate.Application.Features.Facilities.Commands.UpdateFacility;
using RealEstate.Application.Features.Facilities.Queries.GetFacilities;
using RealEstate.Application.Features.Facilities.Queries.GetFacilityById;
using RealEstate.Application.Features.Facilities.Models;

namespace RealEstate.API.Controllers;

[Authorize]
public class FacilitiesController : BaseApiController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<FacilityDto>>> Get()
    {
        return Ok(await Mediator.Send(new GetFacilitiesQuery()));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<FacilityDto>> GetById(int id)
    {
        var result = await Mediator.Send(new GetFacilityByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Create(CreateFacilityCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Update(UpdateFacilityCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        return Ok(await Mediator.Send(new DeleteFacilityCommand(id)));
    }
}
