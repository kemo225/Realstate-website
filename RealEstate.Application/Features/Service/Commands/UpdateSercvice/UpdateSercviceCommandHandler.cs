using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Facilities.Commands.UpdateFacility;

public class UpdateSercviceCommandHandler : IRequestHandler<UpdateFacilityCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSercviceCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateFacilityCommand request, CancellationToken cancellationToken)
    {
        var facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(request.Id);
        
        if (facility == null)
            throw new RealEstate.Application.Exceptions.NotFoundException("Facility", request.Id);

        facility.Name = request.Name;

        _unitOfWork.Repository<Facility>().Update(facility);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return facility.Id;
    }
}
