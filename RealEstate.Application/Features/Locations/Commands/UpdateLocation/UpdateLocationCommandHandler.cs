using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Locations.Commands.UpdateLocation;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLocationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<Location>().GetByIdAsync(request.Id);
        
        if (location == null)
            throw new RealEstate.Application.Exceptions.NotFoundException("Location", request.Id);

        location.City = request.City;
        location.District = request.District;
        location.Street = request.Street;
        location.Country = request.Country ?? string.Empty;
        location.Latitude = request.Latitude;
        location.Longitude = request.Longitude;

        _unitOfWork.Repository<Location>().Update(location);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return location.Id;
    }
}
