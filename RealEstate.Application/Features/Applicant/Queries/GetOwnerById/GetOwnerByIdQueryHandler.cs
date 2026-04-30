using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RealEstate.Application.Features.Owners.Queries.GetOwners;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Owners.Queries.GetOwnerById;

public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOwnerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OwnerDto?> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
    {
        var owner = await _unitOfWork.Repository<Aplicant>().GetByIdAsync(request.Id);
        if (owner == null) throw new RealEstate.Application.Exceptions.NotFoundException("Owner", request.Id);
        return _mapper.Map<OwnerDto>(owner);
    }
}
