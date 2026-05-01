using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Mappings;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Deals.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Deals.Queries.GetDeals;

public class GetDealsQueryHandler : IRequestHandler<GetDealsQuery, PaginatedList<DealDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDealsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<DealDto>> Handle(GetDealsQuery request, CancellationToken cancellationToken)
    {
        var query = from deal in _context.Deals.AsNoTracking()
                    join buyer in _context.Users.AsNoTracking()
                        on deal.CreatedById equals buyer.Id into buyerJoin
                    from buyer in buyerJoin.DefaultIfEmpty()
                    join updater in _context.Users.AsNoTracking()
                        on deal.UpdatedById equals updater.Id into updaterJoin
                    from updater in updaterJoin.DefaultIfEmpty()
                    select new DealQueryRow
                    {
                        Deal = deal,
                        Buyer = buyer,
                        Updater = updater
                    };

        if (request.UnitId.HasValue)
        {
            query = query.Where(d => d.Deal.PaymentPlan != null && d.Deal.PaymentPlan.UnitId == request.UnitId.Value);
        }

        if (request.ProjectId.HasValue)
        {
            query = query.Where(d => d.Deal.PaymentPlan != null &&
                                     d.Deal.PaymentPlan.Unit != null &&
                                     d.Deal.PaymentPlan.Unit.ProjectId == request.ProjectId.Value);
        }

   



        query = ApplySorting(query, request.SortBy, request.SortDirection);

        var projectedQuery = query.Select(d => new DealDto
        {
            Id = d.Deal.Id,
            DealType=d.Deal.DealType,
            DealDate = d.Deal.DealDate,
            Unit = d.Deal.PaymentPlan == null ? new DealUnitBasicDto() : new DealUnitBasicDto
            {
                UnitId = d.Deal.PaymentPlan.UnitId,
                UnitName = d.Deal.PaymentPlan.Unit != null ? d.Deal.PaymentPlan.Unit.Name : string.Empty,
                Price = d.Deal.PaymentPlan.Unit != null ? d.Deal.PaymentPlan.Unit.Price : 0,
                Area = d.Deal.PaymentPlan.Unit != null ? d.Deal.PaymentPlan.Unit.Area : 0,
                IsActive = d.Deal.PaymentPlan.Unit != null && d.Deal.PaymentPlan.Unit.IsActive,
                ProjectId = d.Deal.PaymentPlan.Unit != null ? d.Deal.PaymentPlan.Unit.ProjectId : 0,
                ProjectName = d.Deal.PaymentPlan.Unit != null && d.Deal.PaymentPlan.Unit.Project != null
                    ? d.Deal.PaymentPlan.Unit.Project.Name
                    : string.Empty
            },
            UnitDetails = d.Deal.PaymentPlan == null ? new DealUnitDetailsDto() : new DealUnitDetailsDto
            {
                UnitDetailId = d.Deal.PaymentPlan.Id,
                CommissionRate = d.Deal.PaymentPlan.CommissionRate,
                InstallmentMothes = d.Deal.PaymentPlan.InstallmentMothes,
                InstallmentDownPayment = d.Deal.PaymentPlan.InstallmentDownPayment,
                PaymentType = d.Deal.PaymentPlan.PaymentType.ToString(),
                Status = d.Deal.PaymentPlan.Status.ToString()
            },
            Buyer = new DealBuyerDto
            {
                FullName = d.Deal.ClientName,
                Phone = d.Deal.Phone,
                Email = d.Deal.Email,
                DealDate = d.Deal.DealDate,
            },
            CreatedBy = d.Buyer != null
                ? (!string.IsNullOrWhiteSpace(d.Buyer.UserName) ? d.Buyer.UserName : d.Buyer.Email)
                : null,
            CreatedAt = d.Deal.CreatedAt,
            UpdatedBy = d.Updater != null
                ? (!string.IsNullOrWhiteSpace(d.Updater.UserName) ? d.Updater.UserName : d.Updater.Email)
                : null,
            UpdatedAt = d.Deal.UpdatedAt
        });

        return await projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private static IQueryable<DealQueryRow> ApplySorting(IQueryable<DealQueryRow> query, string? sortBy, string? sortDirection)
    {
        var isDescending = !string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);
        var key = sortBy?.Trim().ToLowerInvariant();

        return key switch
        {
            "createdat" => isDescending ? query.OrderByDescending(x => x.Deal.CreatedAt) : query.OrderBy(x => x.Deal.CreatedAt),
            "unitname" => isDescending
                ? query.OrderByDescending(x => x.Deal.PaymentPlan != null && x.Deal.PaymentPlan.Unit != null ? x.Deal.PaymentPlan.Unit.Name : string.Empty)
                : query.OrderBy(x => x.Deal.PaymentPlan != null && x.Deal.PaymentPlan.Unit != null ? x.Deal.PaymentPlan.Unit.Name : string.Empty),
            "price" => isDescending
                ? query.OrderByDescending(x => x.Deal.PaymentPlan != null && x.Deal.PaymentPlan.Unit != null ? x.Deal.PaymentPlan.Unit.Price : 0)
                : query.OrderBy(x => x.Deal.PaymentPlan != null && x.Deal.PaymentPlan.Unit != null ? x.Deal.PaymentPlan.Unit.Price : 0),
            _ => isDescending ? query.OrderByDescending(x => x.Deal.DealDate) : query.OrderBy(x => x.Deal.DealDate)
        };
    }

    private sealed class DealQueryRow
    {
        public Deal Deal { get; init; } = null!;
        public ApplicationUser? Buyer { get; init; }
        public ApplicationUser? Updater { get; init; }
    }
}
