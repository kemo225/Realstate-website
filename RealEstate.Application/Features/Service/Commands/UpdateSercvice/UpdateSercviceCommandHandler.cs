using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Service.Commands.UpdateSercvice;

public class UpdateSercviceCommandHandler : IRequestHandler<UpdateSercviceCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITranslationService _translationService;

    public UpdateSercviceCommandHandler(IUnitOfWork unitOfWork, ITranslationService translationService)
    {
        _unitOfWork = unitOfWork;
        _translationService = translationService;
    }

    public async Task<int> Handle(UpdateSercviceCommand request, CancellationToken cancellationToken)
    {
        var service = await _unitOfWork.Repository<Domain.Entities.Service>().GetByIdAsync(request.Id);
        if (service == null)
        {
            throw new System.Exception("Service not found");
        }

        service.Name = request.Name.En;
        
        _unitOfWork.Repository<Domain.Entities.Service>().Update(service);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _translationService.SaveTranslationsAsync(
            TranslatableEntity.Service,
            service.Id,
            new Dictionary<string, TranslationInputDto>
            {
                ["Name"] = request.Name
            },
            cancellationToken);

        return service.Id;
    }
}
