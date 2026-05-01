using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.UnitSoldouts.Models;

namespace RealEstate.Application.Features.UnitSoldouts.Queries.GetUnitSoldoutById;

public class GetUnitSoldoutByIdQueryHandler : IRequestHandler<GetUnitSoldoutByIdQuery, UnitSoldoutDto>
{
    private readonly IApplicationDbContext _context;

    public GetUnitSoldoutByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UnitSoldoutDto> Handle(GetUnitSoldoutByIdQuery request, CancellationToken cancellationToken)
    {
        var unitSoldout = await _context.UnitSoldouts
            .AsNoTracking()
            .Where(s => s.Id == request.Id)
            .Select(s => new UnitSoldoutDto
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
                Notes = s.Notes,
                CreatedBy = s.CreatedByUser != null ? s.CreatedByUser.UserName : null,
                CreatedAt = s.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (unitSoldout == null)
        {
            throw new NotFoundException(nameof(RealEstate.Domain.Entities.UnitSoldout), request.Id);
        }

        return unitSoldout;
    }
}
