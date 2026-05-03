using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<int>
{
    public int Id { get; set; }
    public TranslationInputDto Name { get; set; } = new();
    public TranslationInputDto Description { get; set; } = new();
    public int? DeveloperId { get; set; }
    public int? LocationId { get; set; }
}
