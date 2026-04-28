using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RealEstate.Application.Features.Deals.Commands.CreateDeal;
using RealEstate.Application.Features.Deals.Models;
using RealEstate.Application.Features.Deals.Queries.GetDeals;
using RealEstate.Application.Features.Deals.Queries.GetDealById;
using RealEstate.Application.Common.Models;

namespace RealEstate.API.Controllers;

[Authorize]
public class DealsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<DealDto>>> GetDeals([FromQuery] GetDealsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("property/{propertyId}")]
    public async Task<ActionResult<PaginatedList<DealDto>>> GetDealsByProperty(
        int propertyId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await Mediator.Send(new GetDealsQuery(
            PropertyId: propertyId,
            PageNumber: pageNumber,
            PageSize: pageSize)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DealDetailsDto>> GetDealById(int id)
    {
        return Ok(await Mediator.Send(new GetDealByIdQuery(id)));
    }

    [HttpPost]
    public async Task<ActionResult<Result<int>>> CreateDeal(CreateDealCommand command)
    {
        return Ok(await Mediator.Send(command));
    }



}
