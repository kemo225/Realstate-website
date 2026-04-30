using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.Developers.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Developers.Queries.GetDeveloperById;

public record GetDeveloperByIdQuery(int Id) : IRequest<DeveloperDto>;

public class GetDeveloperByIdQueryHandler : IRequestHandler<GetDeveloperByIdQuery, DeveloperDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDeveloperByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DeveloperDto> Handle(GetDeveloperByIdQuery request, CancellationToken cancellationToken)
    {
        var developer = await _unitOfWork.Repository<Developer>()
            .Query()
            .AsNoTracking()
            .Include(d => d.CreatedByUser)
            .Include(d => d.UpdatedByUser)
            .Include(d => d.Gallery)
            .Include(d => d.Projects)
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (developer == null)
        {
            throw new NotFoundException("Developer", request.Id);
        }

        return _mapper.Map<DeveloperDto>(developer);
    }
}
