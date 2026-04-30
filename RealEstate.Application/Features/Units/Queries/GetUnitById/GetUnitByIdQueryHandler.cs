using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Properties.Queries.GetPropertyById;

public class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQuery, UnitDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUnitByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UnitDto?> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Repository<RealEstate.Domain.Entities.Unit>()
            .Query()
            .AsNoTracking()
            .Where(u => u.Id == request.Id)
            .Select(u => new UnitDto
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

                ImageUrls = u.Images
                    .Select(i => i.ImageUrl)
                    .ToList(),

                Facilities = u.PropertyFacilities
                    .Where(pf => pf.Facility != null)
                    .Select(pf => pf.Facility!.Name)
                    .ToList(),

                Services = u.UnitServices
                    .Where(s => s.Service != null)
                    .Select(s => s.Service!.Name)
                    .ToList(),

                // Audit fields — join AspNetUsers via navigation properties
                CreatedBy = u.CreatedByUser != null ? u.CreatedByUser.UserName : null,
                CreatedAt = u.CreatedAt,
                UpdatedBy = u.UpdatedByUser != null ? u.UpdatedByUser.UserName : null,
                UpdatedAt = u.UpdatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null)
            throw new NotFoundException("Unit", request.Id);

        return result;
    }
}
