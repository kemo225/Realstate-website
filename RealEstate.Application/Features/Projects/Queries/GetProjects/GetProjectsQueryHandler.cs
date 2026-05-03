using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Mappings;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Projects.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Application.Features.Projects.Queries.GetProjects;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, PaginatedList<ProjectDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITranslationService _translationService;
    private readonly ILanguageContext _languageContext;

    public GetProjectsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ITranslationService translationService, ILanguageContext languageContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _translationService = translationService;
        _languageContext = languageContext;
    }

    public async Task<PaginatedList<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var result=new PaginatedList<ProjectDto>() ;

        if ( _languageContext.Language.ToLower() == _languageContext.DefaultLanguage)
        {
             result = await _unitOfWork.Repository<Project>()
                .Query()
                .AsNoTracking()
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            return result ;
        }

        if (result.Items.Any())
        {
            var projectIds = result.Items.Select(p => p.Id).ToList();

            var translations = await _translationService.GetTranslationsAsync(
                TranslatableEntity.Project,
                projectIds,
                _languageContext.Language,
                cancellationToken);

            foreach (var project in result.Items)
            {
                if (translations.TryGetValue((project.Id, "Name"), out var translatedName))
                {
                    project.Name = translatedName;
                }

                if (translations.TryGetValue((project.Id, "Description"), out var translatedDescription))
                {
                    project.Description = translatedDescription;
                }
            }
        }

        return result;
    }
}
