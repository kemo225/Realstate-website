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
    private readonly IFileStorageService _fileStorageService;

    public UploadProjectImagesCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<List<string>>> Handle(UploadProjectImagesCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>().GetByIdAsync(request.ProjectId);
        if (project == null) throw new RealEstate.Application.Exceptions.NotFoundException("Project", request.ProjectId);

        var uploadedUrls = new List<string>();

        foreach (var file in request.Files)
        {

            var url = await _fileStorageService.UploadFileAsync(file, "projects");
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
