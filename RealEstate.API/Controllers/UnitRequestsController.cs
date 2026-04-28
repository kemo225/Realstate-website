using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Properties.Commands.ApproveProperty;
using RealEstate.Application.Features.Properties.Commands.CreateProperty;
using RealEstate.Application.Features.Properties.Commands.DeleteProperty;
using RealEstate.Application.Features.Properties.Commands.RejectProperty;
using RealEstate.Application.Features.Properties.Commands.UpdateProperty;
using RealEstate.Application.Features.Properties.Commands.UploadPropertyImages;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Application.Features.Properties.Queries.GetPropertyById;
using RealEstate.Application.Features.PropertyDetails.ApproveProperty;

namespace RealEstate.API.Controllers;

[Authorize]
public class UnitRequestsController : BaseApiController
{
   

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateRequestPropertPayCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("{id}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Result>> Approve(int id, ApprovePropertyCommand command)
    {
        if (id != command.Id) return BadRequest();
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("{id}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Result>> Reject(int id, RejectPropertyCommand command)
    {
        if (id != command.Id) return BadRequest();
        return Ok(await Mediator.Send(command));
    }


    
}
