using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Properties.Models;

namespace RealEstate.Application.Features.Properties.Commands.CreateProperty;

public record CreateRequestPropertPayCommand(CreatePropertyRequest Request) : IRequest<Result<int>>;

