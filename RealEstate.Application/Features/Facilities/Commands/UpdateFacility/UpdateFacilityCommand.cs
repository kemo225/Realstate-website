using MediatR;

namespace RealEstate.Application.Features.Facilities.Commands.UpdateFacility;

public class UpdateFacilityCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

}

