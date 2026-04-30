using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Models;

namespace RealEstate.Application.Features.Auth.Queries.GetAdmins;

public class GetAdminsQueryHandler : IRequestHandler<GetAdminsQuery, IEnumerable<AdminListItemDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAdminsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AdminListItemDto>> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
    {
        var query = from user in _context.Users.AsNoTracking()
                    join userRole in _context.UserRoles.AsNoTracking()
                        on user.Id equals userRole.UserId
                    join role in _context.Roles.AsNoTracking()
                        on userRole.RoleId equals role.Id
                    where role.Name == "Admin"
                    select new AdminListItemDto(
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.CreatedAt
                    );



        return query;
    }

    private static IQueryable<AdminListItemDto> ApplySorting(
        IQueryable<AdminListItemDto> query,
        string? sortBy,
        string? sortDirection)
    {
        var isDescending = !string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);
        var key = sortBy?.Trim().ToLowerInvariant();

        return key switch
        {
            "username" => isDescending ? query.OrderByDescending(x => x.Username) : query.OrderBy(x => x.Username),
            "email" => isDescending ? query.OrderByDescending(x => x.Email) : query.OrderBy(x => x.Email),
            _ => isDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt)
        };
    }
}
