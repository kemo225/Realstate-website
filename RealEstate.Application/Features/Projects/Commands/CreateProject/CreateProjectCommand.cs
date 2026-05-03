using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<int>
{
    public TranslationInputDto Name { get; set; } = new();
    public TranslationInputDto Description { get; set; } = new();
    public int? DeveloperId { get; set; }
    public int LocationId { get; set; }
}
