using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Mappings;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Locations.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace RealEstate.Application.Features.Locations.Queries.GetLocations;

public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, PaginatedList<LocationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLocationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LocationDto>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<Location>()
            .Query()
            .AsNoTracking()
            .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
