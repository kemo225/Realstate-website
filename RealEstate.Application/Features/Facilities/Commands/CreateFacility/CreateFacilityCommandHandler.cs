using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Facilities.Commands.CreateFacility;

public class CreateFacilityCommandHandler : IRequestHandler<CreateFacilityCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITranslationService _translationService;

    public CreateFacilityCommandHandler(
        IUnitOfWork unitOfWork,
        ITranslationService translationService)
    {
        _unitOfWork = unitOfWork;
        _translationService = translationService;
    }

    public async Task<int> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
    {
        // 1. Persist the entity — English value becomes the canonical Name field
        var facility = new Facility
        {
            Name = request.Name.En
        };

        await _unitOfWork.Repository<Facility>().AddAsync(facility);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 2. Persist translations in a single batch insert
        await _translationService.SaveTranslationsAsync(
            TranslatableEntity.Facility,
            facility.Id,
            new Dictionary<string, TranslationInputDto>
            {
                ["Name"] = request.Name
            },
            cancellationToken);

        return facility.Id;
    }
}
