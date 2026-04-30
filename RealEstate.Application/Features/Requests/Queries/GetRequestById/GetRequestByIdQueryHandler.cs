using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Features.Requests.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Requests.Queries.GetRequestById;

public class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, RequestDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRequestByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RequestDetailsDto> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Repository<Request>().Query()
            .AsNoTracking()
            .Where(r => r.Id == request.Id)
            .ProjectTo<RequestDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null)
        {
            throw new NotFoundException("Request", request.Id);
        }

        return result;
    }
}
