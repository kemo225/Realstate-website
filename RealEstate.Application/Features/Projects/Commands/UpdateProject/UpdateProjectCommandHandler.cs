using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>().GetByIdAsync(request.Id);
        
        if (project == null) throw new RealEstate.Application.Exceptions.NotFoundException("Project", request.Id);

        project.Name = request.Name;
        project.Description = request.Description;
        project.DeveloperId = request.DeveloperId;

        _unitOfWork.Repository<Project>().Update(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}

