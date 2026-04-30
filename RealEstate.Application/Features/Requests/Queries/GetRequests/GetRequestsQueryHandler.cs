using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Mappings;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Requests.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Requests.Queries.GetRequests;

public class GetRequestsQueryHandler : IRequestHandler<GetRequestsQuery, PaginatedList<RequestDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRequestsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RequestDto>> Handle(GetRequestsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Request>().Query()
            .AsNoTracking();

        if (request.Status.HasValue)
        {
            query = query.Where(r => r.Status == request.Status.Value);
        }

        return await query
            .OrderByDescending(r => r.CreatedAt)
            .ProjectTo<RequestDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
