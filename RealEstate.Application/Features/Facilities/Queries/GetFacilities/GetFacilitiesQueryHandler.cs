using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Features.Facilities.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Facilities.Queries.GetFacilities;

public class GetFacilitiesQueryHandler : IRequestHandler<GetFacilitiesQuery, IEnumerable<FacilityDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFacilitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FacilityDto>> Handle(GetFacilitiesQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<Facility>().Query()
            .ProjectTo<FacilityDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
