using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.PaymentPlans.Commands.DeletePaymentPlan;

public record DeletePaymentPlanCommand(int Id) : IRequest<bool>;

public class DeletePaymentPlanCommandHandler : IRequestHandler<DeletePaymentPlanCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePaymentPlanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePaymentPlanCommand request, CancellationToken cancellationToken)
    {
        var paymentPlan = await _unitOfWork.Repository<PaymentPlan>().Query()
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (paymentPlan == null)
            throw new NotFoundException("Payment Plan", request.Id);

        if (!paymentPlan.Unit.IsActive)
            throw new ValidatationException("Unit is already sold. Cannot delete the payment plan.");

        _unitOfWork.Repository<PaymentPlan>().Delete(paymentPlan);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
