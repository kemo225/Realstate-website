using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Requests.Commands.ApproveRequest;

public class ApproveRequestCommandHandler : IRequestHandler<ApproveRequestCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public ApproveRequestCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(ApproveRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Request>().Query()
            .Include(r => r.Unit)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);


        if (entity == null)
        {
            throw new  Exceptions.NotFoundException("Request", request.Id);
        }

        if (entity.Status != RequestStatus.Pending)
        {
            throw new Exceptions.ValidatationException($"Request is already {entity.Status}");
        }

        // Ensure Unit exists and is still active (not sold) before approving
        if (entity.Unit == null || !entity.Unit.IsActive)
        {
            throw new Exceptions.BadRequestException("The associated unit has been sold and is no longer available for approval.");
        }
        foreach(var paymentplan in request.PaymentPlans)
        {
            entity.Unit.PaymentPlans.Add(new PaymentPlan()
            {
                CommissionRate=paymentplan.CommisionRate,
                InstallmentDownPayment=paymentplan.InstallmentDownPayment,
                InstallmentMothes=paymentplan.InstallmentMoths,
                PaymentType=paymentplan.PaymentType.ToLower()=="cash"?PaymentType.Cash: PaymentType.Installment,
                Status=PropertyStatus.Approved
            });

        }
        entity.Unit.IsActive = true;
        entity.Status = RequestStatus.Approved;
        entity.ApprovedById = _currentUserService.UserId;
        entity.ApprovedAt = DateTime.UtcNow;

        _unitOfWork.Repository<Request>().Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
