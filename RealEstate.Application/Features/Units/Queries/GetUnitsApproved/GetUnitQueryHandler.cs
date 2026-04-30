using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Mappings;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Properties.Queries.GetProperties;

public class GetUnitQueryHandler : IRequestHandler<GetUnitQuery, PaginatedList<UnitDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUnitQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedList<UnitDto>> Handle(GetUnitQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<RealEstate.Domain.Entities.Unit>()
            .Query()
            .Where(u => u.IsActive)
            .AsNoTracking();

        // Price filters
        if (request.MinPrice.HasValue)
            query = query.Where(u => u.Price >= request.MinPrice.Value);

        if (request.MaxPrice.HasValue)
            query = query.Where(u => u.Price <= request.MaxPrice.Value);

        // Project filter
        if (request.ProjectId.HasValue)
            query = query.Where(u => u.ProjectId == request.ProjectId.Value);

        // Projection with audit fields + null-safe navigation
        var projectedQuery = query.Select(u => new UnitDto
        {
            Id = u.Id,
            Name = u.Name,
            Description = u.Description,
            Price = u.Price,
            PropertyType = u.PropertyType.ToString(),
            IsFeatured = u.IsFeatured,
            IsActive = u.IsActive,
            ProjectName = u.Project != null ? u.Project.Name : null,
            LocationName = u.Project != null && u.Project.Location != null ? u.Project.Location.City : null,

            PaymentPlans = u.PaymentPlans
                .Where(p => p.Status == PropertyStatus.Approved)
                .Select(p => new PaymentPlanDtoRead
                {
                    PlanStatus = p.Status != null ? p.Status.ToString() : null,
                    InstallmentDownPayment = p.InstallmentDownPayment,
                    InstallmentMothes = p.InstallmentMothes,
                    PaymentType = p.PaymentType != null ? p.PaymentType.ToString() : null
                }).ToList(),

            ImageUrls = u.Images.Select(i => i.ImageUrl).ToList(),

            Facilities = u.PropertyFacilities
                .Where(pf => pf.Facility != null)
                .Select(pf => pf.Facility!.Name)
                .ToList(),

            Services = u.UnitServices
                .Where(s => s.Service != null)
                .Select(s => s.Service!.Name)
                .ToList(),

            // Audit fields
            CreatedBy = u.CreatedByUser != null ? u.CreatedByUser.UserName : null,
            CreatedAt = u.CreatedAt,
            UpdatedBy = u.UpdatedByUser != null ? u.UpdatedByUser.UserName : null,
            UpdatedAt = u.UpdatedAt
        }).OrderByDescending(u => u.IsFeatured);

        // Pagination
        return await projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
