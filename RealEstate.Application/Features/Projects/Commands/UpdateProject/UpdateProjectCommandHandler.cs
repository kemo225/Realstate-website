using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITranslationService _translationService;

    public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, ITranslationService translationService)
    {
        _unitOfWork = unitOfWork;
        _translationService = translationService;
    }

    public async Task<int> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>().GetByIdAsync(request.Id);
        if (project == null)
            throw new NotFoundException("Project", request.Id);

        project.Name = request.Name.En;
        project.Description = request.Description.En;
        project.DeveloperId = request.DeveloperId;
        project.LocationId = request.LocationId ?? project.LocationId;

        _unitOfWork.Repository<Project>().Update(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _translationService.SaveTranslationsAsync(
            TranslatableEntity.Project,
            project.Id,
            new Dictionary<string, TranslationInputDto>
            {
                ["Name"] = request.Name,
                ["Description"] = request.Description
            },
            cancellationToken);

        return project.Id;
    }
}
