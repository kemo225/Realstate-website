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
    private readonly IFileStorageService _fileStorageService;

    public DeleteProjectImagesCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<bool> Handle(DeleteProjectImagesCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Domain.Entities.Project>().Query().Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);
        if (project == null) throw new RealEstate.Application.Exceptions.NotFoundException("Project", request.ProjectId);
        if (!project.Images.Any(i => i.ImageUrl == request.Url))
        {
            throw new RealEstate.Application.Exceptions.NotFoundException("ProjectImage", request.Url);
        }


        if (!await _fileStorageService.DeleteFileAsync(request.Url))
        {
            throw new Exception($"Failed to delete file {request.Url}");
        }


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
