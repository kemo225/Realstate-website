using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RealEstate.Application.Features.UnitSoldouts.Models;
using RealEstate.Application.Features.UnitSoldouts.Queries.GetUnitSoldouts;
using RealEstate.Application.Features.UnitSoldouts.Queries.GetUnitSoldoutById;
using RealEstate.Application.Common.Models;

namespace RealEstate.API.Controllers;

[Authorize]
[Route("api/unit-soldout")]
public class UnitSoldoutController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<UnitSoldoutDto>>> GetUnitSoldouts([FromQuery] GetUnitSoldoutsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitSoldoutDto>> GetUnitSoldoutById(int id)
    {
        return Ok(await Mediator.Send(new GetUnitSoldoutByIdQuery(id)));
    }
}
