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

    [HttpGet("unit/{unitId:int}")]
    public async Task<ActionResult<PaginatedList<DealDto>>> GetDealsByUnit(
        int unitId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await Mediator.Send(new GetDealsQuery(
            UnitId: unitId,
            PageNumber: pageNumber,
            PageSize: pageSize)));
    }

  

    [HttpGet("latest")]
    public async Task<ActionResult<PaginatedList<DealDto>>> GetLatestDeals(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await Mediator.Send(new GetDealsQuery(
            SortBy: "dealDate",
            SortDirection: "desc",
            PageNumber: pageNumber,
            PageSize: pageSize)));
    }

    [HttpGet("project/{projectId:int}")]
    public async Task<ActionResult<PaginatedList<DealDto>>> GetDealsByProject(
        int projectId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await Mediator.Send(new GetDealsQuery(
            ProjectId: projectId,
            PageNumber: pageNumber,
            PageSize: pageSize)));
    }

    [HttpGet("completed")]
    public async Task<ActionResult<PaginatedList<DealDto>>> GetCompletedDeals(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await Mediator.Send(new GetDealsQuery(
            PageNumber: pageNumber,
            PageSize: pageSize)));
    }

    [HttpGet("sold-outside")]
    public async Task<ActionResult<PaginatedList<DealDto>>> GetDealsSoldOutside(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await Mediator.Send(new GetDealsQuery(
            DealLocation: RealEstate.Domain.Entities.enDealLocation.SoldOutside,
            PageNumber: pageNumber,
            PageSize: pageSize)));
    }

    [HttpGet("sold-inside")]
    public async Task<ActionResult<PaginatedList<DealDto>>> GetDealsSoldInside(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await Mediator.Send(new GetDealsQuery(
            DealLocation: RealEstate.Domain.Entities.enDealLocation.SoldInside,
            PageNumber: pageNumber,
            PageSize: pageSize)));
    }

    [HttpGet("property/{propertyId:int}")]
    public async Task<ActionResult<PaginatedList<DealDto>>> GetDealsByPropertyCompatibility(
        int propertyId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await Mediator.Send(new GetDealsQuery(
            UnitId: propertyId,
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
