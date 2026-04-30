using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Requests.Commands.ApproveRequest;
using RealEstate.Application.Features.Requests.Commands.CreateRequest;
using RealEstate.Application.Features.Requests.Commands.RejectRequest;
using RealEstate.Application.Features.Requests.Models;
using RealEstate.Application.Features.Requests.Queries.GetRequestById;
using RealEstate.Application.Features.Requests.Queries.GetRequests;
using System.Threading.Tasks;

namespace RealEstate.API.Controllers;

[Authorize]
public class RequestsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PaginatedList<RequestDto>>>> GetAll([FromQuery] GetRequestsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(new ApiResponse<PaginatedList<RequestDto>> { Success = true, Data = result });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<RequestDetailsDto>>> GetById(int id)
    {
        var result = await Mediator.Send(new GetRequestByIdQuery(id));
        return Ok(new ApiResponse<RequestDetailsDto> { Success = true, Data = result });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create(CreateRequestCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(new ApiResponse<int> { Success = true, Data = result, Message = "Request submitted successfully." });
    }

    [HttpPut("approve")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<bool>>> Approve(ApproveRequestCommand request)
    {
        var result = await Mediator.Send(request);
        return Ok(new ApiResponse<bool> { Success = true, Data = result, Message = "Request approved successfully." });
    }

    [HttpPut("{id}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<bool>>> Reject(int id)
    {
        var result = await Mediator.Send(new RejectRequestCommand(id));
        return Ok(new ApiResponse<bool> { Success = true, Data = result, Message = "Request rejected successfully." });
    }
}
