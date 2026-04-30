using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Projects.Commands.UploadProjectImages;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Projects.Commands.UploadProjectImages;

public class DeleteProjectImagesCommandHandler : IRequestHandler<DeleteProjectImagesCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public DeleteProjectImagesCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<bool> Handle(DeleteProjectImagesCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>().Query()
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (project == null) throw new RealEstate.Application.Exceptions.NotFoundException("Project", request.ProjectId);

        var image = project.Images.FirstOrDefault(i => i.ImageUrl == request.Url);
        if (image == null)
        {
            throw new RealEstate.Application.Exceptions.NotFoundException("ProjectImage", request.Url);
        }

        await _imageService.DeleteAsync(request.Url);
        project.Images.Remove(image);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
