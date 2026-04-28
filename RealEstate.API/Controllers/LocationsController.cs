using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.Locations.Commands.CreateLocation;
using RealEstate.Application.Features.Locations.Commands.DeleteLocation;
using RealEstate.Application.Features.Locations.Commands.UpdateLocation;
using RealEstate.Application.Features.Locations.Queries.GetLocationById;
using RealEstate.Application.Features.Locations.Queries.GetLocations;
using RealEstate.Application.Features.Locations.Models;

namespace RealEstate.API.Controllers;

[Authorize]
public class LocationsController : BaseApiController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<LocationDto>>> Get([FromQuery] GetLocationsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<LocationDto>> GetById(int id)
    {
        var result = await Mediator.Send(new GetLocationByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Create(CreateLocationCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Update(int id, UpdateLocationCommand command)
    {
        if (id != command.Id) return BadRequest();
        var result = await Mediator.Send(command);
        if (result == 0) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        return Ok(await Mediator.Send(new DeleteLocationCommand(id)));
    }
}
