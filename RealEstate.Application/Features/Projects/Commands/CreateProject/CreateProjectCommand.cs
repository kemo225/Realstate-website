using MediatR;

namespace RealEstate.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? DeveloperId { get; set; }
    public int LocationId { get; set; }
}
