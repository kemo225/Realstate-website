using MediatR;
using RealEstate.Application.Features.Projects.Models;

namespace RealEstate.Application.Features.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery(int Id) : IRequest<ProjectDto?>;
