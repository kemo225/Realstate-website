using MediatR;
using RealEstate.Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace RealEstate.Application.Features.Properties.Commands.UploadPropertyImages;

public record UploadPropertyImagesCommand(int PropertyId, IFormFileCollection Files) : IRequest<Result<List<string>>>;
