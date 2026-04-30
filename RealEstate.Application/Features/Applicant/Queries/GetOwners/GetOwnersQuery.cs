using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RealEstate.Application.Common.Mappings;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Owners.Queries.GetOwners;

public class OwnerDto
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public record GetOwnersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<OwnerDto>>;

public class GetOwnersQueryHandler : IRequestHandler<GetOwnersQuery, PaginatedList<OwnerDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOwnersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OwnerDto>> Handle(GetOwnersQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<Aplicant>()
            .Query()
            .OrderBy(x => x.FullName)
            .ProjectTo<OwnerDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
