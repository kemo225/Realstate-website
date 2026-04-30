using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Developers.Commands.UploadLogo;

public record UploadDeveloperLogoCommand(int Id, IFormFile File) : IRequest<string>;

public class UploadDeveloperLogoCommandHandler : IRequestHandler<UploadDeveloperLogoCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public UploadDeveloperLogoCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<string> Handle(UploadDeveloperLogoCommand request, CancellationToken cancellationToken)
    {
        var developer = await _unitOfWork.Repository<Developer>().GetByIdAsync(request.Id);
        if (developer == null)
            throw new NotFoundException("Developer", request.Id);

        // Delete old logo if exists
        if (!string.IsNullOrEmpty(developer.LogoImage))
        {
            await _imageService.DeleteAsync(developer.LogoImage);
        }

        var relativePath = await _imageService.UploadAsync(request.File, "developers");
        developer.LogoImage = relativePath;

        _unitOfWork.Repository<Developer>().Update(developer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return relativePath;
    }
}
