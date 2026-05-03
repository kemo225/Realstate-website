using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Features.Facilities.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Facilities.Queries.GetFacilityById
{
    internal class GetFaciltyByQueryId : IRequestHandler<GetFacilityByIdQuery, FacilityDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITranslationService _translationService;
        private readonly ILanguageContext _languageContext;

        public GetFaciltyByQueryId(IUnitOfWork unitOfWork, IMapper mapper, ITranslationService translationService, ILanguageContext languageContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _translationService = translationService;
            _languageContext = languageContext;
        }

        public async Task<FacilityDto> Handle(GetFacilityByIdQuery request, CancellationToken cancellationToken)
        {
            var facility = await _unitOfWork.Repository<Domain.Entities.Facility>().Query()
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
                
            if (facility == null)
            {
                throw new Exception("Facility not found");
            }

            var translations = await _translationService.GetTranslationsAsync(
                TranslatableEntity.Facility,
                new[] { facility.Id },
                _languageContext.Language,
                cancellationToken);

            var dto = _mapper.Map<FacilityDto>(facility);

            if (translations.TryGetValue((facility.Id, "Name"), out var translatedName))
            {
                dto.Name = translatedName;
            }

            return dto;
        }
    }
}
