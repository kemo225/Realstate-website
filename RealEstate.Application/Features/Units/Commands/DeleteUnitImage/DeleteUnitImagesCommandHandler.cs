using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Properties.Commands.DeleteUnitImages;

public class DeleteUnitImagesCommandHandler : IRequestHandler<DeleteUnitImagesCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public DeleteUnitImagesCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<bool> Handle(DeleteUnitImagesCommand request, CancellationToken cancellationToken)
    {
        var unit = await _unitOfWork.Repository<Domain.Entities.Unit>().Query()
            .Include(u => u.Images)
            .FirstOrDefaultAsync(u => u.Id == request.UnitId, cancellationToken);

        if (unit == null) throw new RealEstate.Application.Exceptions.NotFoundException("Unit", request.UnitId);

        var image = unit.Images.FirstOrDefault(i => i.ImageUrl == request.Url);
        if (image == null)
        {
            throw new RealEstate.Application.Exceptions.NotFoundException("UnitImage", request.Url);
        }

        await _imageService.DeleteAsync(request.Url);
        unit.Images.Remove(image);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
