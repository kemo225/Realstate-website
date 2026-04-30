using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Developers.Commands.DeleteGalleryImage;

public record DeleteDeveloperGalleryImageCommand(int Id) : IRequest;

public class DeleteDeveloperGalleryImageCommandHandler : IRequestHandler<DeleteDeveloperGalleryImageCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public DeleteDeveloperGalleryImageCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task Handle(DeleteDeveloperGalleryImageCommand request, CancellationToken cancellationToken)
    {
        var galleryImage = await _unitOfWork.Repository<DeveloperGallery>().GetByIdAsync(request.Id);
        if (galleryImage == null)
            throw new NotFoundException("DeveloperGallery", request.Id);

        if (!string.IsNullOrEmpty(galleryImage.ImageUrl))
        {
            await _imageService.DeleteAsync(galleryImage.ImageUrl);
        }

        _unitOfWork.Repository<DeveloperGallery>().Delete(galleryImage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
