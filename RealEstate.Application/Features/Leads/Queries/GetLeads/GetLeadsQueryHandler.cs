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
using RealEstate.Application.Features.Leads.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Leads.Queries.GetLeads;

public class GetLeadsQueryHandler : IRequestHandler<GetLeadsQuery, PaginatedList<LeadDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLeadsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LeadDto>> Handle(GetLeadsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Leads
            .AsNoTracking();


        if (request.UnitId.HasValue)
        {
            query = query.Where(l => l.UnitId == request.UnitId.Value);
        }

        return await query
            .OrderByDescending(l => l.CreatedAt)
            .ProjectTo<LeadDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
