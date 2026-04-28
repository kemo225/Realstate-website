using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.Facilities.Commands.CreateFacility;
using RealEstate.Application.Features.Facilities.Commands.DeleteFacility;
using RealEstate.Application.Features.Facilities.Commands.UpdateFacility;
using RealEstate.Application.Features.Facilities.Models;
using RealEstate.Application.Features.Facilities.Queries.GetFacilities;
using RealEstate.Application.Features.Facilities.Queries.GetFacilityById;
using RealEstate.Application.Features.Service.Queries.GetSercviceById;
using RealEstate.Application.Features.Service.Queries.GetService;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SercviceDto>>> Get()
        {
            return Ok(await Mediator.Send(new GetServiceQuery()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<SercviceDto>> GetById(int id)
        {
            var result = await Mediator.Send(new GetSercviceByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> Create(CreateSercviceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> Update( UpdateSercviceCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteSercviceCommand(id)));
        }
    }
}
