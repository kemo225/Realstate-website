using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Properties.Commands.DeleteProperty;

public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUnitCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        var property = await _unitOfWork.Repository<Domain.Entities.Unit>().GetByIdAsync(request.Id);
        
        if (property == null) throw new RealEstate.Application.Exceptions.NotFoundException("Property", request.Id);

        property.IsActive=false;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}

