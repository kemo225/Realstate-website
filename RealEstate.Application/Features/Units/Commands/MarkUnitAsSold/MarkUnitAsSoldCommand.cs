using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Units.Commands.MarkUnitAsSold;

public record MarkUnitAsSoldCommand(int Id,string Notes) : IRequest<bool>;

public class MarkUnitAsSoldCommandHandler : IRequestHandler<MarkUnitAsSoldCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public MarkUnitAsSoldCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(MarkUnitAsSoldCommand request, CancellationToken cancellationToken)
    {

        var unit = await _unitOfWork.Repository<Domain.Entities.Unit>()
    .Query().Include(u => u.PaymentPlans)
    .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

  
        if (unit == null)
            throw new NotFoundException("Unit", request.Id);

        if (!unit.IsActive)
            throw new ValidatationException("Unit is already sold");

    
        var UnitSoldOut = new UnitSoldout
        {
            UnitId = unit.Id,
            Notes = request.Notes
        };

        if (unit.SoldCount <= 0)
            UnitSoldOut.SoldType = "Sale";
        else
            UnitSoldOut.SoldType = "ReSale";
        unit.IsActive = false;
        unit.SoldCount++;

        foreach (var paymentPlan in unit.PaymentPlans)
        {
           if(paymentPlan.Status==PropertyStatus.Approved)
                _unitOfWork.Repository<Domain.Entities.PaymentPlan>().Delete(paymentPlan);
        }

      await  _unitOfWork.Repository<RealEstate.Domain.Entities.UnitSoldout>().AddAsync(UnitSoldOut);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
