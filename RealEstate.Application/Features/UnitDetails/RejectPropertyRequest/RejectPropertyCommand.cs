using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Properties.Commands.RejectProperty;

public record RejectPropertyCommand(int Id) : IRequest<Result>;

public class RejectPropertyCommandHandler : IRequestHandler<RejectPropertyCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public RejectPropertyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RejectPropertyCommand request, CancellationToken cancellationToken)
    {
        var property = await _unitOfWork.Repository<UnitDetail>().GetByIdAsync(request.Id);

        if (property == null) 
            throw new NotFoundException("Property Reques not found");

        property.Status =PropertyStatus.Rejected;
        // We could store the reason in a new field if needed, but for now just update status
        
        _unitOfWork.Repository<UnitDetail>().Update(property);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
