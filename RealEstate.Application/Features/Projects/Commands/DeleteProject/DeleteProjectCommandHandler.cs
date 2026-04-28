using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>().Query().Include(p=>p.Properties).FirstOrDefaultAsync(r=>r.Id==request.Id);
        
        if (project == null) throw new RealEstate.Application.Exceptions.NotFoundException("Project", request.Id);
        if (project.Properties != null) throw new RealEstate.Application.Exceptions.ValidtationException($"Cannot Remove {project.Name} Is Related With Another Units");


        _unitOfWork.Repository<Project>().Delete(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}

