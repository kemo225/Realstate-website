using MediatR;
using RealEstate.Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace RealEstate.Application.Features.Properties.Commands.DeleteUnitImages;

public record DeleteUnitImagesCommand(int UnitId, string Url) : IRequest<bool>;
