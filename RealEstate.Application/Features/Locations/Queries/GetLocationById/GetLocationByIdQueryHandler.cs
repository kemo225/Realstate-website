using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RealEstate.Application.Features.Locations.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Locations.Queries.GetLocationById;

public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, LocationDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLocationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LocationDto?> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<Location>().GetByIdAsync(request.Id);
        if (location == null) throw new RealEstate.Application.Exceptions.NotFoundException("Location", request.Id);
        return _mapper.Map<LocationDto>(location);
    }
}
