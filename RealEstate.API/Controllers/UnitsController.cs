using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Properties.Commands.DeleteProperty;
using RealEstate.Application.Features.Units.Commands.UpdateUnit;
using RealEstate.Application.Features.Properties.Commands.UploadPropertyImages;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Application.Features.Properties.Queries.GetProperties;
using RealEstate.Application.Features.Properties.Queries.GetPropertyById;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UnitDto>>> Get([FromQuery] GetUnitQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
     

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UnitDto>> GetById(int id)
        {
            var result = await Mediator.Send(new GetUnitByIdQuery(id));
            return Ok(result);
        }
        [HttpPut("marksold")]
        [Authorize(Roles = "Admin,SuperAdmin")] // Assuming Admin role, adjust if needed
        public async Task<ActionResult<RealEstate.Application.Common.Models.Result<bool>>> MarkAsSold([FromQuery] int id, [FromQuery] string Notes)
        {
            var result = await Mediator.Send(new RealEstate.Application.Features.Units.Commands.MarkUnitAsSold.MarkUnitAsSoldCommand(id, Notes));
            return Ok(RealEstate.Application.Common.Models.Result<bool>.Success(result));
        }
    }
}
