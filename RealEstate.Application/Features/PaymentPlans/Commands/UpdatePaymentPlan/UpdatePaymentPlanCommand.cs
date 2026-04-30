using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.PaymentPlans.Commands.UpdatePaymentPlan;

public record UpdatePaymentPlanCommand(
    int paymentPlanId,
     string? PaymentType,
     PropertyStatus? Status,
    decimal InstallmentDownPayment,
    int InstallmentYears
) : IRequest<bool>;

public class UpdatePaymentPlanCommandHandler : IRequestHandler<UpdatePaymentPlanCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentPlanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdatePaymentPlanCommand request, CancellationToken cancellationToken)
    {
        var paymentPlan = await _unitOfWork.Repository<PaymentPlan>().Query()
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.Id == request.paymentPlanId, cancellationToken);

        if (paymentPlan == null)
            throw new NotFoundException("Payment Plan", request.paymentPlanId);

        if (paymentPlan.Unit == null || !paymentPlan.Unit.IsActive)
            throw new ValidatationException("Unit is already sold. Cannot update the payment plan.");

        paymentPlan.InstallmentDownPayment = request.InstallmentDownPayment;
        paymentPlan.InstallmentMothes = request.InstallmentYears;
        paymentPlan.PaymentType = request.PaymentType.ToLower() == "cash" ? PaymentType.Cash : PaymentType.Installment;
        paymentPlan.Status = request.Status;
        
        _unitOfWork.Repository<PaymentPlan>().Update(paymentPlan);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
