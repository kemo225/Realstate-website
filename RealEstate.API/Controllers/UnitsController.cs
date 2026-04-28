using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Properties.Commands.DeleteProperty;
using RealEstate.Application.Features.Properties.Commands.UpdateProperty;
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
        public async Task<ActionResult<IEnumerable<PropertyDto>>> Get([FromQuery] GetPropertiesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
       

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PropertyDto>> GetById(int id)
        {
            var result = await Mediator.Send(new GetPropertyByIdQuery(id));
            return Ok(result);
        }
  
       
    }
}
