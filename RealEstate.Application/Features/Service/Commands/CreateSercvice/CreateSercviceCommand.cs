using MediatR;

namespace RealEstate.Application.Features.Facilities.Commands.CreateFacility;

public class CreateSercviceCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
}

