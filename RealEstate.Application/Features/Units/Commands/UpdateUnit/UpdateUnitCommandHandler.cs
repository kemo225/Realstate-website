using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Units.Commands.UpdateUnit;

public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUnitCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
   
    public async Task<int> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        // Use minimal query with projection or just enough to get the unit and its details
        var unit = await _unitOfWork.Repository<RealEstate.Domain.Entities.Unit>()
            .Query()
            .FirstOrDefaultAsync(u => u.Id == request.Id , cancellationToken);

        if (unit == null)
            throw new NotFoundException("Unit", request.Id);
       if(!unit.IsActive)
            {
            throw new ValidatationException("Cannot update an Sold unit.");
        }

        // Check if name already exists in the same project (excluding current unit)
        var nameExists = await _unitOfWork.Repository<RealEstate.Domain.Entities.Unit>()
            .ExistsAsync(u => u.ProjectId == unit.ProjectId && u.Name == request.Name && u.Id != request.Id);

        if (nameExists)
            throw new ValidatationException($"A unit with name '{request.Name}' already exists in this project.");

        // Update basic unit fields
        unit.Name = request.Name;
        unit.Description = request.Description;
        unit.Price = request.Price;
        unit.PropertyType = request.PropertyType;

        // Update related UnitDetails
        unit.FloorName = request.FloorName;
        unit.NoKitchen = request.NoKitchen;
        unit.NoBedRoom = request.NoBedRoom;
        unit.NoBathRoom = request.NoBathRoom;
        unit.IsFeatured= request.IsFeatured;




        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return unit.Id;
    }
}

