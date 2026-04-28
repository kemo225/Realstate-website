using MediatR;
using RealEstate.Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace RealEstate.Application.Features.Projects.Commands.UploadProjectImages;

public record DeleteProjectImagesCommand(int ProjectId, string Url) : IRequest<bool>;
