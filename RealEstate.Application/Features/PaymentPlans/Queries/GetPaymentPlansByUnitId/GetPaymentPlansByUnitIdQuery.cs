using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Application.Features.PaymentPlans.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.PaymentPlans.Queries.GetPaymentPlansByUnitId;

public record GetPaymentPlansByUnitIdQuery(int UnitId) : IRequest<IEnumerable<UnitPaymentPlanDto>>;

public class GetPaymentPlansByUnitIdQueryHandler : IRequestHandler<GetPaymentPlansByUnitIdQuery, IEnumerable<UnitPaymentPlanDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPaymentPlansByUnitIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UnitPaymentPlanDto>> Handle(GetPaymentPlansByUnitIdQuery request, CancellationToken cancellationToken)
    {
        var plans = await _unitOfWork.Repository<PaymentPlan>().Query()
            .AsNoTracking()
            .Where(p => p.UnitId == request.UnitId)
            .Include(p => p.CreatedByUser)
            .Include(p => p.UpdatedByUser)
            .Select(p => new UnitPaymentPlanDto
            {
                Id = p.Id,
                UnitId = p.UnitId,
                UnitName = p.Unit.Name,
                InstallmentDownPayment =Convert.ToDecimal(p.InstallmentDownPayment),
                PaymentType = p.PaymentType.ToString()!,
                InstallmentMonths = Convert.ToInt32(p.InstallmentMothes),
                UnitStatus =p.Status.ToString() ?? "",
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedByUser != null ? p.CreatedByUser.UserName : null,
                UpdatedAt = p.UpdatedAt,
                UpdatedBy = p.UpdatedByUser != null ? p.UpdatedByUser.UserName : null
            })
            .ToListAsync(cancellationToken);

        return plans;
    }
}
