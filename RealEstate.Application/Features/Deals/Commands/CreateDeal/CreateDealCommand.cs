using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Deals.Commands.CreateDeal;

public record CreateDealCommand(
    int UnitPlanId,
    string ?FullName,
    string ?Email,
    string ?Phone,
    string DealLocation
    ) : IRequest<Result<int>>;

public class CreateDealCommandHandler : IRequestHandler<CreateDealCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateDealCommand request, CancellationToken cancellationToken)
    {
        var paymentPlan = await _unitOfWork.Repository<PaymentPlan>()
        .Query()
        .Include(p => p.Unit)
        .FirstOrDefaultAsync(p => p.Id == request.UnitPlanId, cancellationToken);

        if (paymentPlan == null)
            throw new NotFoundException("Payment Plan", request.UnitPlanId);

        if (paymentPlan.Status == PropertyStatus.Sold)
            throw new ValidatationException("Already sold");

        var unit = await _unitOfWork.Repository<Domain.Entities.Unit>()
            .Query()
            .Include(u => u.PaymentPlans)
            .FirstOrDefaultAsync(u => u.Id == paymentPlan.UnitId, cancellationToken);
        if (unit is null)
            throw new ValidatationException("Not Found");
    
        var deal = new Deal
        {
            DealDate = DateTime.UtcNow,
            ClientName = request.FullName ,
            Phone = request.Phone ,
            Email = request.Email ,
            UnitPlanId = paymentPlan.Id
        };
        if (unit.SoldCount <= 0)
        {
            deal.DealType = "Sale";
        }
        else
        {
            deal.DealType = "ReSale";

        }

        paymentPlan.Status = PropertyStatus.Sold;

        if (paymentPlan.Unit != null)
        {
            paymentPlan.Unit.IsActive = false;
            paymentPlan.Unit.SoldCount++;
        }

        foreach (var plan in unit.PaymentPlans)
        {
            if (plan.Id != paymentPlan.Id && (plan.Status == PropertyStatus.Approved))
                _unitOfWork.Repository<PaymentPlan>().Delete(plan);
        }

        await _unitOfWork.Repository<Deal>().AddAsync(deal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<int>.Success(deal.Id);
    }
}

