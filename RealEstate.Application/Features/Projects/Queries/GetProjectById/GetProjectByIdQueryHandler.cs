using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Projects.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Services;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITranslationService _translationService;
    private readonly ILanguageContext _languageContext;

    public GetProjectByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ITranslationService translationService, ILanguageContext languageContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _translationService = translationService;
        _languageContext = languageContext;
    }

    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = new ProjectDto();

        if (_languageContext.Language.ToLower() == _languageContext.DefaultLanguage)
        {
             project = await _unitOfWork.Repository<Project>()
            .Query()
            .AsNoTracking()
            .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (project == null) throw new RealEstate.Application.Exceptions.NotFoundException("Project", request.Id);
            return project;

        }

        var translations = await _translationService.GetTranslationsAsync(
            TranslatableEntity.Project,
            new[] { project.Id },
            _languageContext.Language,
            cancellationToken);

        if (translations.TryGetValue((project.Id, "Name"), out var translatedName))
        {
            project.Name = translatedName;
        }

        if (translations.TryGetValue((project.Id, "Description"), out var translatedDescription))
        {
            project.Description = translatedDescription;
        }

        return project;
    }
}
