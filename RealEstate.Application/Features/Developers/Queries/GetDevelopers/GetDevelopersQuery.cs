using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Mappings;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Developers.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Developers.Queries.GetDevelopers;

public record GetDevelopersQuery : IRequest<PaginatedList<DeveloperDto>>
{
    public string? SearchKeyword { get; init; }
    public string? Name { get; init; }
    public string? SortBy { get; init; } = "name"; // Default sort
    public bool SortDescending { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetDevelopersQueryHandler : IRequestHandler<GetDevelopersQuery, PaginatedList<DeveloperDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDevelopersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DeveloperDto>> Handle(GetDevelopersQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Developer>()
            .Query()
            .AsNoTracking();

        // Filtering
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(d => d.Name.Contains(request.Name));
        }

        // Searching
        if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
        {
            query = query.Where(d => d.Name.Contains(request.SearchKeyword) || 
                                    (d.Description != null && d.Description.Contains(request.SearchKeyword)));
        }

        // Sorting
        query = request.SortBy?.ToLower() switch
        {
            "name" => request.SortDescending ? query.OrderByDescending(d => d.Name) : query.OrderBy(d => d.Name),
            "date" => request.SortDescending ? query.OrderByDescending(d => d.CreatedAt) : query.OrderBy(d => d.CreatedAt),
            _ => query.OrderBy(d => d.Name)
        };

        return await query
            .ProjectTo<DeveloperDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
