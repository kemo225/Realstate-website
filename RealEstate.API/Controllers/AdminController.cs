using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.Auth.Commands.UpdateAdmin;
using RealEstate.Application.Features.Auth.Models;

namespace RealEstate.API.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : BaseApiController
{
    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateAdminRequest request)
    {
        var result = await Mediator.Send(new UpdateAdminCommand(request));
        if (result.Succeeded) return Ok(result);
        return BadRequest(result);
    }
}
