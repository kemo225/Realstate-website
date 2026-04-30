using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Developers.Commands.DeleteLogo;

public record DeleteDeveloperLogoCommand(int Id) : IRequest;

public class DeleteDeveloperLogoCommandHandler : IRequestHandler<DeleteDeveloperLogoCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public DeleteDeveloperLogoCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task Handle(DeleteDeveloperLogoCommand request, CancellationToken cancellationToken)
    {
        var developer = await _unitOfWork.Repository<Developer>().GetByIdAsync(request.Id);
        if (developer == null)
            throw new NotFoundException("Developer", request.Id);

        if (!string.IsNullOrEmpty(developer.LogoImage))
        {
            await _imageService.DeleteAsync(developer.LogoImage);
            developer.LogoImage = null;

            _unitOfWork.Repository<Developer>().Update(developer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
