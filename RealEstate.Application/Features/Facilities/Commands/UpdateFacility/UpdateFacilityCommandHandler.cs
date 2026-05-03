using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Facilities.Commands.UpdateFacility;

public class UpdateFacilityCommandHandler : IRequestHandler<UpdateFacilityCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITranslationService _translationService;

    public UpdateFacilityCommandHandler(IUnitOfWork unitOfWork, ITranslationService translationService)
    {
        _unitOfWork = unitOfWork;
        _translationService = translationService;
    }

    public async Task<int> Handle(UpdateFacilityCommand request, CancellationToken cancellationToken)
    {
        var facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(request.Id);
        
        if (facility == null)
        {
            throw new System.Exception("Facility not found");
        }

        facility.Name = request.Name.En;

        _unitOfWork.Repository<Facility>().Update(facility);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
