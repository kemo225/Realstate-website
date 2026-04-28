using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Facilities.Commands.CreateFacility;

public class CreateFacilityCommandHandler : IRequestHandler<CreateFacilityCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateFacilityCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
    {
        var facility = new Facility
        {
            Name = request.Name
        };

        await _unitOfWork.Repository<Facility>().AddAsync(facility);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return facility.Id;
    }
}
