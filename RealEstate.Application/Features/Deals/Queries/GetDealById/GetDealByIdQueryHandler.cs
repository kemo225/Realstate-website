using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Features.Deals.Models;

namespace RealEstate.Application.Features.Deals.Queries.GetDealById;

public class GetDealByIdQueryHandler : IRequestHandler<GetDealByIdQuery, DealDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDealByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DealDetailsDto> Handle(GetDealByIdQuery request, CancellationToken cancellationToken)
    {
        var deal = await _context.Deals
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
        
        if (deal == null) throw new RealEstate.Application.Exceptions.NotFoundException("Deal", request.Id);

        return _mapper.Map<DealDetailsDto>(deal);
    }
}
