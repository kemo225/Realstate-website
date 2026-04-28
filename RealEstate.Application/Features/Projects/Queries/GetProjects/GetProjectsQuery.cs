using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Projects.Models;

namespace RealEstate.Application.Features.Projects.Queries.GetProjects;

public class GetProjectsQuery : IRequest<PaginatedList<ProjectDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
