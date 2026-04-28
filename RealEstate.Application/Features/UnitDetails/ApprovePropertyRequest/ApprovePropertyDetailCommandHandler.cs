using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.PropertyDetails.ApproveProperty;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Properties.Commands.ApproveProperty;


public class ApprovePropertyDetailCommandHandler : IRequestHandler<ApprovePropertyCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public ApprovePropertyDetailCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(ApprovePropertyCommand request, CancellationToken cancellationToken)
    {
        var propertyRequest = await _unitOfWork.Repository<UnitDetail>().GetByIdAsync(request.Id);
        if (propertyRequest == null) 
            throw new NotFoundException("Property Request not found");
        var pproperty = await _unitOfWork.Repository<Domain.Entities.Unit>().GetByIdAsync(propertyRequest.UnitId);
        if (pproperty is null)
            throw new NotFoundException("Property Not found");


        propertyRequest.Status = PropertyStatus.Approved;
        propertyRequest.CommissionRate = request.CommissionRate ?? 0;
        propertyRequest.InstallmentYears = request.InstallmentYears ?? 0;
        propertyRequest.InstallmentDownPayment = request.InstallmentDownPayment ?? 0;
        propertyRequest.PaymentType = request.PaymentMethod;

        propertyRequest.ApprovedById = _currentUserService.UserId;
        propertyRequest.ApprovedAt = DateTime.UtcNow;

       
        pproperty.IsActive= true;


        _unitOfWork.Repository<UnitDetail>().Update(propertyRequest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

