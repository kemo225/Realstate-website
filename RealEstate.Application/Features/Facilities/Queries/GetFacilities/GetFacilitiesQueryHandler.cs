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

namespace RealEstate.Application.Features.Facilities.Queries.GetFacilities;

public class GetFacilitiesQueryHandler : IRequestHandler<GetFacilitiesQuery, IEnumerable<FacilityDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITranslationService _translationService;
    private readonly ILanguageContext _languageContext;

    public GetFacilitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ITranslationService translationService, ILanguageContext languageContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _translationService = translationService;
        _languageContext = languageContext;
    }

    public async Task<IEnumerable<FacilityDto>> Handle(GetFacilitiesQuery request, CancellationToken cancellationToken)
    {
        var facilities = await _unitOfWork.Repository<Facility>().Query()
            .ProjectTo<FacilityDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (!facilities.Any()) return facilities;

        var facilityIds = facilities.Select(f => f.Id).ToList();

        var translations = await _translationService.GetTranslationsAsync(
            TranslatableEntity.Facility,
            facilityIds,
            _languageContext.Language,
            cancellationToken);

        foreach (var facility in facilities)
        {
            if (translations.TryGetValue((facility.Id, "Name"), out var translatedName))
            {
                facility.Name = translatedName;
            }
        }

        return facilities;
    }
}
