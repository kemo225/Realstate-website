using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Properties.Queries.GetPropertyById;

public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPropertyByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PropertyDto?> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Domain.Entities.Unit>()
         .Query()
         .Where(u => u.IsActive && u.Id == request.Id)
         .AsNoTracking();

        var result = await query
            .Select(u => new
            {
                Unit = u,
                Detail = u.PropertyDetails
                    .FirstOrDefault(d => d.Status == PropertyStatus.Approved)
            })
            .Select(x => new PropertyDto
            {
                Id = x.Unit.Id,
                Name = x.Unit.Name,
                Description = x.Unit.Description,
                Price = x.Unit.Price,
                PropertyType = x.Unit.PropertyType.ToString(),
                IsFeatured = x.Unit.IsFeatured,

                ProjectName = x.Unit.Project.Name,
                LocationName = x.Unit.Project.Location.City,

                CommissionRate = x.Detail != null ? x.Detail.CommissionRate : null,
                InstallmentYears = x.Detail != null ? x.Detail.InstallmentYears : null,
                InstallmentDownPayment = x.Detail != null ? x.Detail.InstallmentDownPayment : null,
                PaymentType = x.Detail != null ? x.Detail.PaymentType.ToString() : null,
                Status = x.Detail != null ? x.Detail.Status.ToString() : null,

                ImageUrls = x.Unit.Images
                    .Select(i => i.ImageUrl)
                    .ToList(),

                Facilities = x.Unit.PropertyFacilities
                    .Select(pf => pf.Facility.Name)
                    .ToList(),

                Services = x.Unit.UnitServices
                    .Select(s => s.Service.Name)
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null)
            throw new NotFoundException("Unit", request.Id);

        return result;
    }
}
