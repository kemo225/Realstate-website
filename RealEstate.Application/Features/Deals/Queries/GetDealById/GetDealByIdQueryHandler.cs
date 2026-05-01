using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Features.Deals.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Deals.Queries.GetDealById;

public class GetDealByIdQueryHandler : IRequestHandler<GetDealByIdQuery, DealDetailsDto>
{
    private readonly IApplicationDbContext _context;

    public GetDealByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DealDetailsDto> Handle(GetDealByIdQuery request, CancellationToken cancellationToken)
    {
        var deal = await (from item in _context.Deals.AsNoTracking()
                          join buyer in _context.Users.AsNoTracking()
                              on item.CreatedById equals buyer.Id into buyerJoin
                          from buyer in buyerJoin.DefaultIfEmpty()
                          join updater in _context.Users.AsNoTracking()
                              on item.UpdatedById equals updater.Id into updaterJoin
                          from updater in updaterJoin.DefaultIfEmpty()
                          where item.Id == request.Id
                          select new DealDetailsDto
                          {
                              Id = item.Id,
                              DealDate = item.DealDate,
                              Unit = item.PaymentPlan == null ? new DealUnitBasicDto() : new DealUnitBasicDto
                              {
                                  UnitId = item.PaymentPlan.UnitId,
                                  UnitName = item.PaymentPlan.Unit != null ? item.PaymentPlan.Unit.Name : string.Empty,
                                  Price = item.PaymentPlan.Unit != null ? item.PaymentPlan.Unit.Price : 0,
                                  Area = item.PaymentPlan.Unit != null ? item.PaymentPlan.Unit.Area : 0,
                                  IsActive = item.PaymentPlan.Unit != null && item.PaymentPlan.Unit.IsActive,
                                  ProjectId = item.PaymentPlan.Unit != null ? item.PaymentPlan.Unit.ProjectId : 0,
                                  ProjectName = item.PaymentPlan.Unit != null && item.PaymentPlan.Unit.Project != null
                                      ? item.PaymentPlan.Unit.Project.Name
                                      : string.Empty
                              },
                              UnitDetails = item.PaymentPlan == null ? new DealUnitDetailsDto() : new DealUnitDetailsDto
                              {
                                  UnitDetailId = item.PaymentPlan.Id,
                                  CommissionRate = item.PaymentPlan.CommissionRate,
                                  InstallmentMothes = item.PaymentPlan.InstallmentMothes,
                                  InstallmentDownPayment = item.PaymentPlan.InstallmentDownPayment,
                                  PaymentType = item.PaymentPlan.PaymentType.ToString(),
                                  Status = item.PaymentPlan.Status.ToString()
                              },
                              Buyer = new DealBuyerDto
                              {
                                  FullName = item.ClientName,
                                  Phone = item.Phone,
                                  Email = item.Email,
                                  DealDate = item.DealDate
                              },
                              CreatedBy = buyer != null
                                  ? (!string.IsNullOrWhiteSpace(buyer.UserName) ? buyer.UserName : buyer.Email)
                                  : null,
                              CreatedAt = item.CreatedAt,
                              UpdatedBy = updater != null
                                  ? (!string.IsNullOrWhiteSpace(updater.UserName) ? updater.UserName : updater.Email)
                                  : null,
                              UpdatedAt = item.UpdatedAt
                          }).FirstOrDefaultAsync(cancellationToken);

        if (deal == null) throw new RealEstate.Application.Exceptions.NotFoundException("Deal", request.Id);

        return deal;
    }
}
