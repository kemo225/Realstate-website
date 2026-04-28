using MediatR;

namespace RealEstate.Application.Features.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(int Id) : IRequest<bool>;

