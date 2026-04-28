using MediatR;
using RealEstate.Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace RealEstate.Application.Features.Projects.Commands.UploadProjectImages;

public record UploadProjectImagesCommand(int ProjectId, IFormFileCollection Files) : IRequest<Result<List<string>>>;
