using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Facilities.Commands.UpdateFacility;

public class UpdateFacilityCommand : IRequest<int>
{
    public int Id { get; set; }
    public TranslationInputDto Name { get; set; } = new();
}

