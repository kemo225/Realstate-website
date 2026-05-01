using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Mappings;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.UnitSoldouts.Models;

namespace RealEstate.Application.Features.UnitSoldouts.Queries.GetUnitSoldouts;

public class GetUnitSoldoutsQueryHandler : IRequestHandler<GetUnitSoldoutsQuery, PaginatedList<UnitSoldoutDto>>
{
    private readonly IApplicationDbContext _context;

    public GetUnitSoldoutsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<UnitSoldoutDto>> Handle(GetUnitSoldoutsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.UnitSoldouts
            .AsNoTracking()
            .AsQueryable();

        // Filtering
        if (!string.IsNullOrEmpty(request.SoldType))
        {
            query = query.Where(s => s.SoldType == request.SoldType);
        }

        // Search by Unit Name
        if (!string.IsNullOrEmpty(request.UnitName))
        {
            query = query.Where(s => s.Unit.Name.Contains(request.UnitName));
        }

        // Sorting by SoldoutDate desc
        query = query.OrderByDescending(s => s.SoldoutDate);

        // Selection Projection
        var projectedQuery = query.Select(s => new UnitSoldoutDto
        {
            Id = s.Id,
            UnitId = s.UnitId,
            UnitName = s.Unit.Name,
            ProjectName = s.Unit.Project != null ? s.Unit.Project.Name : null,
            City = s.Unit.Project != null && s.Unit.Project.Location != null ? s.Unit.Project.Location.City : null,
            Country = s.Unit.Project != null && s.Unit.Project.Location != null ? s.Unit.Project.Location.Country : null,
            UnitImages = s.Unit.Images.Select(i => i.ImageUrl).ToList(),
            SoldoutDate = s.SoldoutDate,
            SoldType = s.SoldType,
            // Notes excluded from list as per general requirements (id, unitId, unitName, projectName, city, country, unitImages, soldoutDate, soldType, createdBy, createdAt)
            CreatedBy = s.CreatedByUser != null ? s.CreatedByUser.UserName : null,
            CreatedAt = s.CreatedAt
        });

        return await projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
