using MediatR;

namespace RealEstate.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? DeveloperName { get; set; }
    public int LocationId { get; set; }
}
