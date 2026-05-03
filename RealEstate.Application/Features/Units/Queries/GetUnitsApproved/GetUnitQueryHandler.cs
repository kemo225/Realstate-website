using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Mappings;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Services;

namespace RealEstate.Application.Features.Properties.Queries.GetProperties;

public class GetUnitQueryHandler : IRequestHandler<GetUnitQuery, PaginatedList<UnitDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITranslationService _translationService;
    private readonly ILanguageContext _languageContext;

    public GetUnitQueryHandler(IUnitOfWork unitOfWork, ITranslationService translationService, ILanguageContext languageContext)
    {
        _unitOfWork = unitOfWork;
        _translationService = translationService;
        _languageContext = languageContext;
    }

    public async Task<PaginatedList<UnitDto>> Handle(GetUnitQuery request, CancellationToken cancellationToken)
    {
        var result = new PaginatedList<UnitDto>();
        if (_languageContext.Language.ToLower() == _languageContext.DefaultLanguage)
        {
            var query = _unitOfWork.Repository<RealEstate.Domain.Entities.Unit>()
            .Query()
            .Where(u => u.IsActive)
            .AsNoTracking();

            // Price filters
            if (request.MinPrice.HasValue)
                query = query.Where(u => u.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(u => u.Price <= request.MaxPrice.Value);

            // Project filter
            if (request.ProjectId.HasValue)
                query = query.Where(u => u.ProjectId == request.ProjectId.Value);

             result = await query.Select(u => new UnitDto
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                Price = u.Price,
                PropertyType = u.PropertyType.ToString(),
                IsFeatured = u.IsFeatured,
                FloorNumber = u.FloorNumber,
                Area = u.Area,
                NoBathRoom = u.NoBathRoom,
                NoBedRoom = u.NoBedRoom,
                NoKitchen = u.NoKitchen,
                FloorName = u.FloorName,
                IsActive = u.IsActive,
                ProjectName = u.Project != null ? u.Project.Name : null,
                LocationName = u.Project != null && u.Project.Location != null ? u.Project.Location.City : null,

                PaymentPlans = u.PaymentPlans
                    .Where(p => p.Status == PropertyStatus.Approved)
                    .Select(p => new PaymentPlanDtoRead
                    {
                        PlanStatus = p.Status.ToString(),
                        InstallmentDownPayment = p.InstallmentDownPayment,
                        InstallmentMothes = p.InstallmentMothes,
                        PaymentType = p.PaymentType.ToString()
                    }).ToList(),

                ImageUrls = u.Images.Select(i => i.ImageUrl).ToList(),

                // We'll map these temporarily and replace with translations
                Facilities = u.PropertyFacilities.Select(pf => pf.FacilityId.ToString()).ToList(),
                Services = u.UnitServices.Select(us => us.ServiceId.ToString()).ToList(),

                CreatedBy = u.CreatedByUser != null ? u.CreatedByUser.UserName : null,
                CreatedAt = u.CreatedAt,
                UpdatedBy = u.UpdatedByUser != null ? u.UpdatedByUser.UserName : null,
                UpdatedAt = u.UpdatedAt
            }).OrderByDescending(u => u.IsFeatured)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
            return result;
        }

        if (result.Items.Any())
        {
            var language = _languageContext.Language;
            var unitIds = result.Items.Select(u => u.Id).ToList();

            // 1. Fetch Unit Translations
            var unitTranslations = await _translationService.GetTranslationsAsync(
                TranslatableEntity.Unit, unitIds, language, cancellationToken);

            // 2. Collect all unique Facility and Service IDs across all units in this page
            var allUnitIdsInPage = result.Items.Select(u => u.Id).ToList();
            
            // Re-fetch raw data for facilities and services mapping because projection lost the IDs in a clean way
            // Actually, I can use the IDs I put in the strings above
            
            var allFacilityIds = new HashSet<int>();
            var allServiceIds = new HashSet<int>();
            
            // Since I projected them into the DTO's list as strings, I need to get them back or re-query.
            // Better: Re-query the navigation properties for the current page items to get the actual names/translations
            var unitsWithNav = await _unitOfWork.Repository<RealEstate.Domain.Entities.Unit>().Query()
                .AsNoTracking()
                .Include(u => u.PropertyFacilities).ThenInclude(pf => pf.Facility)
                .Include(u => u.UnitServices).ThenInclude(us => us.Service)
                .Where(u => unitIds.Contains(u.Id))
                .ToListAsync(cancellationToken);

            foreach(var u in unitsWithNav)
            {
                foreach(var pf in u.PropertyFacilities) allFacilityIds.Add(pf.FacilityId);
                foreach(var us in u.UnitServices) allServiceIds.Add(us.ServiceId);
            }

            var facilityTranslations = allFacilityIds.Any()
                ? await _translationService.GetTranslationsAsync(TranslatableEntity.Facility, allFacilityIds, language, cancellationToken)
                : new Dictionary<(int, string), string>();

            var serviceTranslations = allServiceIds.Any()
                ? await _translationService.GetTranslationsAsync(TranslatableEntity.Service, allServiceIds, language, cancellationToken)
                : new Dictionary<(int, string), string>();

            foreach (var item in result.Items)
            {
                // Apply Unit Translations
                if (unitTranslations.TryGetValue((item.Id, "Name"), out var tName)) item.Name = tName;
                if (unitTranslations.TryGetValue((item.Id, "Description"), out var tDesc)) item.Description = tDesc;

                // Apply Facility/Service Translations
                var unitNav = unitsWithNav.First(u => u.Id == item.Id);
                
                item.Facilities = unitNav.PropertyFacilities
                    .Where(pf => pf.Facility != null)
                    .Select(pf => facilityTranslations.TryGetValue((pf.FacilityId, "Name"), out var fName) ? fName : pf.Facility!.Name)
                    .ToList();

                item.Services = unitNav.UnitServices
                    .Where(us => us.Service != null)
                    .Select(us => serviceTranslations.TryGetValue((us.ServiceId, "Name"), out var sName) ? sName : us.Service!.Name)
                    .ToList();
            }
        }

        return result;
    }
}
