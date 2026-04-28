using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Mappings;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Properties.Queries.GetProperties;

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, PaginatedList<PropertyDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPropertiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PropertyDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Domain.Entities.Unit>()
            .Query()
            .Where(u => u.IsActive)
            .AsNoTracking();


        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(u =>
                u.Name.Contains(request.SearchTerm) ||
                (u.Description != null && u.Description.Contains(request.SearchTerm)));
        }

        // 💰 Price Filter
        if (request.MinPrice.HasValue)
            query = query.Where(u => u.Price >= request.MinPrice.Value);

        if (request.MaxPrice.HasValue)
            query = query.Where(u => u.Price <= request.MaxPrice.Value);

        // 🏢 Project Filter
        if (request.ProjectId.HasValue)
            query = query.Where(u => u.ProjectId == request.ProjectId.Value);

        // 🚀 Projection (مرة واحدة بس)
        var projectedQuery = query.Select(u => new PropertyDto
        {
            Id = u.Id,
            Name = u.Name,
            Description = u.Description,
            Price = u.Price,
            PropertyType = u.PropertyType.ToString(),
            IsFeatured = u.IsFeatured,
            ProjectName = u.Project.Name,
            LocationName = u.Project.Location.City,

            // 📌 أهم تحسين هنا
            CommissionRate = u.PropertyDetails
                .Where(d => d.Status == PropertyStatus.Approved)
                .Select(d => (decimal?)d.CommissionRate)
                .FirstOrDefault(),

            InstallmentYears = u.PropertyDetails
                .Where(d => d.Status == PropertyStatus.Approved)
                .Select(d => (decimal?)d.InstallmentYears)
                .FirstOrDefault(),

            InstallmentDownPayment = u.PropertyDetails
                .Where(d => d.Status == PropertyStatus.Approved)
                .Select(d => (decimal?)d.InstallmentDownPayment)
                .FirstOrDefault(),

            PaymentType = u.PropertyDetails
                .Where(d => d.Status == PropertyStatus.Approved)
                .Select(d => d.PaymentType.ToString())
                .FirstOrDefault(),

            Status = u.PropertyDetails
                .Where(d => d.Status == PropertyStatus.Approved)
                .Select(d => d.Status.ToString())
                .FirstOrDefault(),

            ImageUrls = u.Images.Select(i => i.ImageUrl).ToList(),
            Facilities = u.PropertyFacilities.Select(pf => pf.Facility.Name).ToList(),
            Services = u.UnitServices.Select(s => s.Service.Name).ToList()
        });

        // 📄 Pagination
        return await projectedQuery
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
