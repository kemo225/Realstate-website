using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Locations.Commands.DeleteLocation;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLocationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<Location>().GetByIdAsync(request.Id);
        
        if (location == null) 
            throw new RealEstate.Application.Exceptions.NotFoundException("Location", request.Id);
        if (await _unitOfWork.Repository<Project>().ExistsAsync(p=>p.LocationId==location.Id))
            throw new RealEstate.Application.Exceptions.ValidtationException("Location Is Related With Project or More");

        _unitOfWork.Repository<Location>().Delete(location);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}

