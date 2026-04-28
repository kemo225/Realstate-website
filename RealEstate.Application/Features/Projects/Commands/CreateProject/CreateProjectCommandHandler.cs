using MediatR;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Repository<Location>().ExistsAsync(l => l.Id == request.LocationId))
            throw new NotFoundException("Location Is Not Found");
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description,
            DeveloperName = request.DeveloperName,
            LocationId = request.LocationId
        };

        await _unitOfWork.Repository<Project>().AddAsync(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}

