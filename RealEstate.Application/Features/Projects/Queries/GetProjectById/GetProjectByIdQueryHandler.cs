using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Features.Projects.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

using AutoMapper.QueryableExtensions;

namespace RealEstate.Application.Features.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>()
            .Query()
            .AsNoTracking()
            .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (project == null) throw new RealEstate.Application.Exceptions.NotFoundException("Project", request.Id);

        return project;
    }
}
