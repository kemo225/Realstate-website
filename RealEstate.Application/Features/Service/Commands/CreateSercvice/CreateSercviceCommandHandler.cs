using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Service.Commands.CreateSercvice;

public class CreateSercviceCommandHandler : IRequestHandler<CreateSercviceCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITranslationService _translationService;

    public CreateSercviceCommandHandler(IUnitOfWork unitOfWork, ITranslationService translationService)
    {
        _unitOfWork = unitOfWork;
        _translationService = translationService;
    }

    public async Task<int> Handle(CreateSercviceCommand request, CancellationToken cancellationToken)
    {
        var Srvice = new Domain.Entities.Service
        {
            Name = request.Name.En
        };

        await _unitOfWork.Repository<Domain.Entities.Service>().AddAsync(Srvice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _translationService.SaveTranslationsAsync(
            TranslatableEntity.Service,
            Srvice.Id,
            new Dictionary<string, TranslationInputDto>
            {
                ["Name"] = request.Name
            },
            cancellationToken);

        return Srvice.Id;
    }
}
