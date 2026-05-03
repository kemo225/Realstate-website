using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Properties.Queries.GetPropertyById;

public class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQuery, UnitDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITranslationService _translationService;
    private readonly ILanguageContext _languageContext;

    public GetUnitByIdQueryHandler(IUnitOfWork unitOfWork, ITranslationService translationService, ILanguageContext languageContext)
    {
        _unitOfWork = unitOfWork;
        _translationService = translationService;
        _languageContext = languageContext;
    }

    public async Task<UnitDto?> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
    {

        var unit = await _unitOfWork.Repository<RealEstate.Domain.Entities.Unit>()
            .Query()
            .AsNoTracking()
            .Include(u => u.Project)
            .Include(u => u.PropertyFacilities).ThenInclude(pf => pf.Facility)
            .Include(u => u.UnitServices).ThenInclude(us => us.Service)
            .Include(u => u.PaymentPlans)
            .Where(u => u.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        var result = new UnitDto();
        if (_languageContext.Language.ToLower() == _languageContext.DefaultLanguage)
        {
            if (unit == null)
                throw new NotFoundException("Unit", request.Id);

             result = new UnitDto
            {
                Id = unit.Id,
                Name = unit.Name,
                Description = unit.Description,
                Price = unit.Price,
                FloorNumber = unit.FloorNumber,
                Area = unit.Area,
                NoBathRoom = unit.NoBathRoom,
                NoBedRoom = unit.NoBedRoom,
                NoKitchen = unit.NoKitchen,
                FloorName = unit.FloorName,
                PropertyType = unit.PropertyType.ToString(),
                IsFeatured = unit.IsFeatured,
                IsActive = unit.IsActive,
                ProjectName = unit.Project?.Name,
                LocationName = unit.Project?.Location?.City,

                PaymentPlans = unit.PaymentPlans
                   .Where(p => p.Status == PropertyStatus.Approved)
                   .Select(p => new PaymentPlanDtoRead
                   {
                       PlanStatus = p.Status.ToString(),
                       InstallmentDownPayment = p.InstallmentDownPayment,
                       InstallmentMothes = p.InstallmentMothes,
                       PaymentType = p.PaymentType.ToString()
                   }).ToList(),

                ImageUrls = unit.Images.Select(i => i.ImageUrl).ToList(),

                Facilities = unit.PropertyFacilities
                   .Where(pf => pf.Facility != null)
                   .Select(pf => pf.Facility!.Name)
                   .ToList(),

                Services = unit.UnitServices
                   .Where(us => us.Service != null)
                   .Select(us => us.Service!.Name)
                   .ToList(),

                CreatedBy = unit.CreatedByUser?.UserName,
                CreatedAt = unit.CreatedAt,
                UpdatedBy = unit.UpdatedByUser?.UserName,
                UpdatedAt = unit.UpdatedAt
            };
            return result;
        }

        var language = _languageContext.Language;
        
        var unitTranslations = await _translationService.GetTranslationsAsync(
            TranslatableEntity.Unit, new[] { unit.Id }, language, cancellationToken);
            
        var facilityIds = unit.PropertyFacilities.Select(pf => pf.FacilityId).Distinct().ToList();
        var facilityTranslations = facilityIds.Any() 
            ? await _translationService.GetTranslationsAsync(TranslatableEntity.Facility, facilityIds, language, cancellationToken)
            : new Dictionary<(int, string), string>();

        var serviceIds = unit.UnitServices.Select(us => us.ServiceId).Distinct().ToList();
        var serviceTranslations = serviceIds.Any()
            ? await _translationService.GetTranslationsAsync(TranslatableEntity.Service, serviceIds, language, cancellationToken)
            : new Dictionary<(int, string), string>();

         result = new UnitDto
        {
            Id = unit.Id,
            Name = unitTranslations.TryGetValue((unit.Id, "Name"), out var tName) ? tName : unit.Name,
            Description = unitTranslations.TryGetValue((unit.Id, "Description"), out var tDesc) ? tDesc : unit.Description,
            Price = unit.Price,
            FloorNumber = unit.FloorNumber,
            Area = unit.Area,
            NoBathRoom = unit.NoBathRoom,
            NoBedRoom = unit.NoBedRoom,
            NoKitchen = unit.NoKitchen,
            FloorName = unit.FloorName,
            PropertyType = unit.PropertyType.ToString(),
            IsFeatured = unit.IsFeatured,
            IsActive = unit.IsActive,
            ProjectName = unit.Project?.Name,
            LocationName = unit.Project?.Location?.City,

            PaymentPlans = unit.PaymentPlans
                .Where(p => p.Status == PropertyStatus.Approved)
                .Select(p => new PaymentPlanDtoRead
                {
                    PlanStatus = p.Status.ToString(),
                    InstallmentDownPayment = p.InstallmentDownPayment,
                    InstallmentMothes = p.InstallmentMothes,
                    PaymentType = p.PaymentType.ToString()
                }).ToList(),

            ImageUrls = unit.Images.Select(i => i.ImageUrl).ToList(),

            Facilities = unit.PropertyFacilities
                .Where(pf => pf.Facility != null)
                .Select(pf => facilityTranslations.TryGetValue((pf.FacilityId, "Name"), out var fName) ? fName : pf.Facility!.Name)
                .ToList(),

            Services = unit.UnitServices
                .Where(us => us.Service != null)
                .Select(us => serviceTranslations.TryGetValue((us.ServiceId, "Name"), out var sName) ? sName : us.Service!.Name)
                .ToList(),

            CreatedBy = unit.CreatedByUser?.UserName,
            CreatedAt = unit.CreatedAt,
            UpdatedBy = unit.UpdatedByUser?.UserName,
            UpdatedAt = unit.UpdatedAt
        };

        return result;
    }
}
