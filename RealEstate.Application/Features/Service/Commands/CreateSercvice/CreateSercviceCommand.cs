using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Service.Commands.CreateSercvice;

public class CreateSercviceCommand : IRequest<int>
{
    public TranslationInputDto Name { get; set; } = new();
}

