using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Locations.Commands.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLocationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = new Location
        {
            City = request.City,
            District = request.District,
            Street = request.Street,
            Country = request.Country ?? string.Empty,
            Latitude = request.Latitude ,
            Longitude = request.Longitude
        };

        await _unitOfWork.Repository<Location>().AddAsync(location);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return location.Id;
    }
}
