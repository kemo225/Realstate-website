using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    private readonly IMapper _mapper;

    public GetDealsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DealDto>> Handle(GetDealsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Deals
            .AsNoTracking();

      

     

        return await query
            .OrderByDescending(d => d.CreatedAt)
            .ProjectTo<DealDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
