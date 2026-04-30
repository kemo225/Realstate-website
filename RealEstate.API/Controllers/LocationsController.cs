using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.Locations.Commands.CreateLocation;
using RealEstate.Application.Features.Locations.Commands.DeleteLocation;
using RealEstate.Application.Features.Locations.Commands.UpdateLocation;
using RealEstate.Application.Features.Locations.Queries.GetLocationById;
using RealEstate.Application.Features.Locations.Queries.GetLocations;
using RealEstate.Application.Features.Locations.Models;

using RealEstate.Application.Common.Models;

namespace RealEstate.API.Controllers;

[Authorize]
public class LocationsController : BaseApiController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<PaginatedList<LocationDto>>>> GetAll([FromQuery] GetLocationsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(new ApiResponse<PaginatedList<LocationDto>> { Success = true, Data = result });
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<LocationDto>>> GetById(int id)
    {
        var result = await Mediator.Send(new GetLocationByIdQuery(id));
        return Ok(new ApiResponse<LocationDto> { Success = true, Data = result });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Create(CreateLocationCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Update( UpdateLocationCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        return Ok(await Mediator.Send(new DeleteLocationCommand(id)));
    }
}
