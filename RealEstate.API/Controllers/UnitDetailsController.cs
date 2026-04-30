//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using RealEstate.Application.Common.Models;

//using System.Threading.Tasks;

//namespace RealEstate.API.Controllers;

//[Authorize]
//public class UnitDetailsController : BaseApiController
//{
//    [HttpGet]
//    public async Task<ActionResult<ApiResponse<PaginatedList<UnitDetailDto>>>> GetAll([FromQuery] GetAllUnitDetailsQuery query)
//    {
//        var result = await Mediator.Send(query);
//        return Ok(new ApiResponse<PaginatedList<UnitDetailDto>> { Success = true, Data = result });
//    }

//    [HttpGet("{id}")]
//    public async Task<ActionResult<ApiResponse<UnitDetailDto>>> GetById(int id)
//    {
//        var result = await Mediator.Send(new GetUnitDetailByIdQuery(id));
//        return Ok(new ApiResponse<UnitDetailDto> { Success = true, Data = result });
//    }
//}
