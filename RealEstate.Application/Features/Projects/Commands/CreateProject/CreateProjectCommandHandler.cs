using System.Collections.Generic;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITranslationService _translationService;

    public CreateProjectCommandHandler(IUnitOfWork unitOfWork, ITranslationService translationService)
    {
        _unitOfWork = unitOfWork;
        _translationService = translationService;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Repository<Location>().ExistsAsync(l => l.Id == request.LocationId))
            throw new NotFoundException("Location Is Not Found");
        if (request.DeveloperId.HasValue && !await _unitOfWork.Repository<Developer>().ExistsAsync(l => l.Id == request.DeveloperId.Value))
            throw new NotFoundException("Developer Is Not Found");
            
        var project = new Project
        {
            Name = request.Name.En,
            Description = request.Description.En,
            DeveloperId = request.DeveloperId,
            LocationId = request.LocationId
        };

        await _unitOfWork.Repository<Project>().AddAsync(project);
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

