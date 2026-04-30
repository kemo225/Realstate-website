using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Developers.Commands.UploadGallery;

public record UploadDeveloperGalleryCommand(int Id, List<IFormFile> Files) : IRequest<List<string>>;

public class UploadDeveloperGalleryCommandHandler : IRequestHandler<UploadDeveloperGalleryCommand, List<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public UploadDeveloperGalleryCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<List<string>> Handle(UploadDeveloperGalleryCommand request, CancellationToken cancellationToken)
    {
        var developer = await _unitOfWork.Repository<Developer>().GetByIdAsync(request.Id);
        if (developer == null)
            throw new NotFoundException("Developer", request.Id);

        var relativePaths = new List<string>();

        foreach (var file in request.Files)
        {
            var relativePath = await _imageService.UploadAsync(file, "galleries");
            relativePaths.Add(relativePath);

            await _unitOfWork.Repository<DeveloperGallery>().AddAsync(new DeveloperGallery
            {
                DeveloperId = request.Id,
                ImageUrl = relativePath
            });
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return relativePaths;
    }
}
