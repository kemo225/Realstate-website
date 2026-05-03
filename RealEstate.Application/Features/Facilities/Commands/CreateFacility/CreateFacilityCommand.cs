using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Facilities.Commands.CreateFacility;

public class CreateFacilityCommand : IRequest<int>
{
    /// <summary>Multilingual name. English (En) is required.</summary>
    public TranslationInputDto Name { get; set; } = new();
}

