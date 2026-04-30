using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Commands.UpdateAdmin;
using RealEstate.Application.Features.Auth.Models;
using RealEstate.Application.Features.Auth.Queries.GetAdmins;

namespace RealEstate.API.Controllers;

[Authorize(Roles = "Admin")]
public class AdminsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<AdminListItemDto>>> GetAdmins([FromQuery] GetAdminsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateAdminRequest request)
    {
        var result = await Mediator.Send(new UpdateAdminCommand(request));
        if (result.Succeeded) return Ok(result);
        return BadRequest(result);
    }
}
