using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Features.Facilities.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Services;

namespace RealEstate.Application.Features.Service.Queries.GetService;

public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, IEnumerable<SercviceDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITranslationService _translationService;
    private readonly ILanguageContext _languageContext;

    public GetServiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ITranslationService translationService, ILanguageContext languageContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _translationService = translationService;
        _languageContext = languageContext;
    }

    public async Task<IEnumerable<SercviceDto>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
    {
        var services = new List<SercviceDto>();
        if (_languageContext.Language.ToLower() == _languageContext.DefaultLanguage)
        {
             services = await _unitOfWork.Repository<Domain.Entities.Service>().Query()
            .ProjectTo<SercviceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
            return services;
        }


        if (!services.Any()) return services;

        var serviceIds = services.Select(s => s.Id).ToList();

        var translations = await _translationService.GetTranslationsAsync(
            TranslatableEntity.Service,
            serviceIds,
            _languageContext.Language,
            cancellationToken);

        foreach (var service in services)
        {
            if (translations.TryGetValue((service.Id, "Name"), out var translatedName))
            {
                service.Name = translatedName;
            }
        }

        return services;
    }
}
