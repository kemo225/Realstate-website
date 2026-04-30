using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Developers.Commands.CreateDeveloper;
using RealEstate.Application.Features.Developers.Commands.DeleteDeveloper;
using RealEstate.Application.Features.Developers.Commands.UpdateDeveloper;
using RealEstate.Application.Features.Developers.Commands.UploadGallery;
using RealEstate.Application.Features.Developers.Commands.UploadLogo;
using RealEstate.Application.Features.Developers.Commands.DeleteLogo;
using RealEstate.Application.Features.Developers.Commands.DeleteGalleryImage;
using RealEstate.Application.Features.Developers.Models;
using RealEstate.Application.Features.Developers.Queries.GetDeveloperById;
using RealEstate.Application.Features.Developers.Queries.GetDevelopers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.API.Controllers;

[Authorize]
public class DevelopersController : BaseApiController
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<int>>> Create(CreateDeveloperCommand command)
    {
        var id = await Mediator.Send(command);
        return Ok(new ApiResponse<int> { Success = true, Data = id, Message = "Developer created successfully." });
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<MediatR.Unit>>> Update( UpdateDeveloperCommand command)
    {
       

        await Mediator.Send(command);
        return Ok(new ApiResponse<MediatR.Unit> { Success = true, Message = "Developer updated successfully." });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<MediatR.Unit>>> Delete(int id)
    {
        await Mediator.Send(new DeleteDeveloperCommand(id));
        return Ok(new ApiResponse<MediatR.Unit> { Success = true, Message = "Developer deleted successfully." });
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PaginatedList<DeveloperDto>>>> GetAll([FromQuery] GetDevelopersQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(new ApiResponse<PaginatedList<DeveloperDto>> { Success = true, Data = result });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<DeveloperDto>>> GetById(int id)
    {
        var result = await Mediator.Send(new GetDeveloperByIdQuery(id));
        return Ok(new ApiResponse<DeveloperDto> { Success = true, Data = result });
    }

    [HttpPost("{id}/logo")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<string>>> UploadLogo(int id, IFormFile file)
    {
        var result = await Mediator.Send(new UploadDeveloperLogoCommand(id, file));
        return Ok(new ApiResponse<string> { Success = true, Data = result, Message = "Logo uploaded successfully." });
    }

    [HttpPost("{id}/gallery")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<List<string>>>> UploadGallery(int id, List<IFormFile> files)
    {
        var result = await Mediator.Send(new UploadDeveloperGalleryCommand(id, files));
        return Ok(new ApiResponse<List<string>> { Success = true, Data = result, Message = "Gallery images uploaded successfully." });
    }

    [HttpDelete("{id}/logo")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<MediatR.Unit>>> DeleteLogo(int id)
    {
        await Mediator.Send(new DeleteDeveloperLogoCommand(id));
        return Ok(new ApiResponse<MediatR.Unit> { Success = true, Message = "Logo deleted successfully." });
    }

    [HttpDelete("gallery/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<MediatR.Unit>>> DeleteGalleryImage(int id)
    {
        await Mediator.Send(new DeleteDeveloperGalleryImageCommand(id));
        return Ok(new ApiResponse<MediatR.Unit> { Success = true, Message = "Gallery image deleted successfully." });
    }
}
