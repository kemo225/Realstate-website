using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.PaymentPlans.Commands.CreatePaymentPlan;
using RealEstate.Application.Features.PaymentPlans.Commands.UpdatePaymentPlan;
using RealEstate.Application.Features.PaymentPlans.Commands.DeletePaymentPlan;
using RealEstate.Application.Features.PaymentPlans.Queries.GetPaymentPlansByUnitId;
using RealEstate.Application.Features.PaymentPlans.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.API.Controllers;

[Authorize]
[Route("api/payment-plans")]
public class PaymentPlansController : BaseApiController
{
    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<Result<int>>> Create(CreatePaymentPlanCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(Result<int>.Success(result));
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<Result<bool>>> Update(UpdatePaymentPlanCommand command)
    {
        
        var result = await Mediator.Send(command);
        return Ok(Result<bool>.Success(result));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<Result<bool>>> Delete(int id)
    {
        var result = await Mediator.Send(new DeletePaymentPlanCommand(id));
        return Ok(Result<bool>.Success(result));
    }

    [HttpGet("unit/{unitId}")]
    [AllowAnonymous]
    public async Task<ActionResult<Result<IEnumerable<UnitPaymentPlanDto>>>> GetByUnitId(int unitId)
    {
        var result = await Mediator.Send(new GetPaymentPlansByUnitIdQuery(unitId));
        return Ok(Result<IEnumerable<UnitPaymentPlanDto>>.Success(result));
    }
}
