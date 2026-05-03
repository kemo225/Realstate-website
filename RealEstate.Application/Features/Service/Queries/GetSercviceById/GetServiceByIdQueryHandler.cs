using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.Facilities.Models;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Service.Queries.GetSercviceById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetSercviceByIdQuery, SercviceDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITranslationService _translationService;
        private readonly ILanguageContext _languageContext;

        public GetServiceByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ITranslationService translationService, ILanguageContext languageContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _translationService = translationService;
            _languageContext = languageContext;
        }

        public async Task<SercviceDto> Handle(GetSercviceByIdQuery request, CancellationToken cancellationToken)
        {
            var service= new Domain.Entities.Service();
            if (_languageContext.Language.ToLower() == _languageContext.DefaultLanguage)
            {

                 service = await _unitOfWork.Repository<Domain.Entities.Service>().Query()
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

                if (service == null)
                {
                    throw new NotFoundException("Service not found");
                }
                _mapper.Map<SercviceDto>(service);
            }
            var translations = await _translationService.GetTranslationsAsync(
                TranslatableEntity.Service,
                new[] { service.Id },
                _languageContext.Language,
                cancellationToken);

            var dto = _mapper.Map<SercviceDto>(service);

            if (translations.TryGetValue((service.Id, "Name"), out var translatedName))
            {
                dto.Name = translatedName;
            }

            return dto;
        }
    }
}
