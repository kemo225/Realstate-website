using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.PaymentPlans.Commands.CreatePaymentPlan;

//public decimal? CommissionRate { get; set; }
//public int? InstallmentYears { get; set; }
//public decimal? InstallmentDownPayment { get; set; }
//public PaymentType? PaymentType { get; set; }
//public PropertyStatus? Status { get; set; }
//[ForeignKey("Unit")]
//public int UnitId { get; set; }
public record CreatePaymentPlanCommand(
     string? PaymentType,
    int UnitId,
    decimal InstallmentDownPayment,
    int InstallmentYears
) : IRequest<int>;

public class CreatePaymentPlanCommandHandler : IRequestHandler<CreatePaymentPlanCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePaymentPlanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePaymentPlanCommand request, CancellationToken cancellationToken)
    {
        var unit = await _unitOfWork.Repository<RealEstate.Domain.Entities.Unit>().Query()
            .FirstOrDefaultAsync(u => u.Id == request.UnitId, cancellationToken);

        if (unit == null)
            throw new NotFoundException("Unit", request.UnitId);

        if (!unit.IsActive)
            throw new ValidatationException("Unit is already sold. Cannot add a payment plan.");

        var paymentPlan = new PaymentPlan
        {
            CommissionRate=0,
            UnitId = request.UnitId,
            InstallmentDownPayment = request.InstallmentDownPayment,
            InstallmentMothes = request.InstallmentYears,
            Status = PropertyStatus.Approved,
            PaymentType = request.PaymentType.ToLower() == "cash" ? PaymentType.Cash : PaymentType.Installment,

        };

        await _unitOfWork.Repository<PaymentPlan>().AddAsync(paymentPlan);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return paymentPlan.Id;
    }
}
