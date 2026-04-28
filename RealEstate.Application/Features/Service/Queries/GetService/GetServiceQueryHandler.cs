using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Features.Facilities.Models;
using RealEstate.Application.Features.Facilities.Queries.GetFacilities;
using RealEstate.Application.Features.Service.Queries.GetService;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Service.Queries.GetServiceQueryHandler;

public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, IEnumerable<SercviceDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetServiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SercviceDto>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<Domain.Entities.Service>().Query()
            .ProjectTo<SercviceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
