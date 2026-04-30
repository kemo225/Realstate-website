using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Projects.Commands.AddPropertyForProject;
using RealEstate.Application.Features.Projects.Commands.CreateProject;
using RealEstate.Application.Features.Projects.Commands.DeleteProject;
using RealEstate.Application.Features.Projects.Commands.UpdateProject;
using RealEstate.Application.Features.Projects.Commands.UploadProjectImages;
using RealEstate.Application.Features.Projects.Models;
using RealEstate.Application.Features.Projects.Queries.GetProjectById;
using RealEstate.Application.Features.Projects.Queries.GetProjects;
using RealEstate.Application.Features.Properties.Commands.DeleteProperty;
using RealEstate.Application.Features.Properties.Commands.DeleteUnitImages;
using RealEstate.Application.Features.Units.Commands.UpdateUnit;
using RealEstate.Application.Features.Properties.Commands.UploadPropertyImages;
using RealEstate.Application.Features.Properties.Models;

namespace RealEstate.API.Controllers;

[Authorize]
public class ProjectsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PaginatedList<ProjectDto>>>> GetAll([FromQuery] GetProjectsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(new ApiResponse<PaginatedList<ProjectDto>> { Success = true, Data = result });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProjectDto>>> GetById(int id)
    {
        var result = await Mediator.Send(new GetProjectByIdQuery(id));
        return Ok(new ApiResponse<ProjectDto> { Success = true, Data = result });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Create(CreateProjectCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Update(int id, UpdateProjectCommand command)
    {
        if (id != command.Id) return BadRequest();
        var result = await Mediator.Send(command);
        if (result == 0) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        return Ok(await Mediator.Send(new DeleteProjectCommand(id)));
    }

    [HttpPost("{id}/images")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<List<string>>>> UploadImages(int id, IFormFileCollection files)
    {
        var result = await Mediator.Send(new UploadProjectImagesCommand(id, files));
        return Ok(new ApiResponse<List<string>> { Success = result.Succeeded, Data = result.Data, Message = result.Succeeded ? "Images uploaded successfully." : result.Errors.FirstOrDefault() });
    }
    [HttpPost("AddUnitProject")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> AddUnitForProject(AddPropertiesToProjectCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    [HttpPut("UpdateUnit")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> UpdateUnit(UpdateUnitCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("DeleteUnit/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> DeleteUnit(int id)
    {
        var command = new DeleteUnitCommand(id);
        return Ok(await Mediator.Send(command));
    }
    [HttpPost("{id}/uploadUnit/images")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<List<string>>>> uploadUnitimages(int id, IFormFileCollection files)
    {
        var result = await Mediator.Send(new UploadPropertyImagesCommand(id, files));
        return Ok(new ApiResponse<List<string>> { Success = result.Succeeded, Data = result.Data, Message = result.Succeeded ? "Images uploaded successfully." : result.Errors.FirstOrDefault() });
    }
    [HttpDelete("{id}/deleteproject/images")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteProjectImages(int id, string url)
    {
        var result = await Mediator.Send(new DeleteProjectImagesCommand(id, url));
        return Ok(new ApiResponse<bool> { Success = result, Data = result, Message = "Image deleted successfully." });
    }
    [HttpDelete("{id}/deleteUnit/images")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteUnitImages(int id, string url)
    {
        var result = await Mediator.Send(new DeleteUnitImagesCommand(id, url));
        return Ok(new ApiResponse<bool> { Success = result, Data = result, Message = "Image deleted successfully." });
    }

}
