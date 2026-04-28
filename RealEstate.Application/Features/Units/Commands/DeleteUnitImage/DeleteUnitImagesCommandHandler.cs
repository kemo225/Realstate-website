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

public class DeleteUnitImagesCommandHandler : IRequestHandler<DeleteUnitImagesCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public DeleteUnitImagesCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<bool> Handle(DeleteUnitImagesCommand request, CancellationToken cancellationToken)
    {
        var unit = await _unitOfWork.Repository<Domain.Entities.Unit>().Query().Include(u=>u.Images).FirstOrDefaultAsync(u => u.Id == request.UnitId, cancellationToken);
        if (unit == null) throw new RealEstate.Application.Exceptions.NotFoundException("Unit", request.UnitId);
        if (!unit.Images.Any(i => i.ImageUrl == request.Url))
        {
            throw new RealEstate.Application.Exceptions.NotFoundException("UnitImage", request.Url);
        }


        if (! await _fileStorageService.DeleteFileAsync(request.Url))
         {
             throw new Exception($"Failed to delete file {request.Url}");
        }


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
