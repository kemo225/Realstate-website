using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Properties.Commands.UploadPropertyImages;

public class UploadPropertyImagesCommandHandler : IRequestHandler<UploadPropertyImagesCommand, Result<List<string>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public UploadPropertyImagesCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<Result<List<string>>> Handle(UploadPropertyImagesCommand request, CancellationToken cancellationToken)
    {
        var property = await _unitOfWork.Repository<Domain.Entities.Unit>().GetByIdAsync(request.PropertyId);
        if (property == null) throw new RealEstate.Application.Exceptions.NotFoundException("Property", request.PropertyId);

        var uploadedUrls = new List<string>();

        foreach (var file in request.Files)
        {
            var url = await _imageService.UploadAsync(file, "units");
            if (!string.IsNullOrEmpty(url))
            {
                uploadedUrls.Add(url);
                property.Images.Add(new UnitImage
                {
                    ImageUrl = url,
                    IsPrimary = property.Images.Count == 0,
                    SortOrder = property.Images.Count
                });
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<List<string>>.Success(uploadedUrls);
    }
}
