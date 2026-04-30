using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Features.Leads.Models;

using AutoMapper.QueryableExtensions;

namespace RealEstate.Application.Features.Leads.Queries.GetLeadById;

public class GetLeadByIdQueryHandler : IRequestHandler<GetLeadByIdQuery, LeadDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLeadByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LeadDto?> Handle(GetLeadByIdQuery request, CancellationToken cancellationToken)
    {
        var lead = await _context.Leads
            .AsNoTracking()
            .ProjectTo<LeadDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);
        
        if (lead == null) throw new RealEstate.Application.Exceptions.NotFoundException("Lead", request.Id);

        return lead;
    }
}
