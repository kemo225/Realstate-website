using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Projects.Commands.UploadProjectImages;

public class UploadProjectImagesCommandHandler : IRequestHandler<UploadProjectImagesCommand, Result<List<string>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public UploadProjectImagesCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<Result<List<string>>> Handle(UploadProjectImagesCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>().GetByIdAsync(request.ProjectId);
        if (project == null) throw new RealEstate.Application.Exceptions.NotFoundException("Project", request.ProjectId);

        var uploadedUrls = new List<string>();

        foreach (var file in request.Files)
        {
            var url = await _imageService.UploadAsync(file, "projects");
            if (!string.IsNullOrEmpty(url))
            {
                uploadedUrls.Add(url);
                project.Images.Add(new ProjectImage
                {
                    ImageUrl = url
                });
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<List<string>>.Success(uploadedUrls);
    }
}
