using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Projects.Commands.AddPropertyForProject
{
    public class AddPropertiesToProjectHandler : IRequestHandler<AddPropertiesToProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITranslationService _translationService;

        public AddPropertiesToProjectHandler(IUnitOfWork unitOfWork, ITranslationService translationService)
        {
            _unitOfWork = unitOfWork;
            _translationService = translationService;
        }

        public async Task<int> Handle(AddPropertiesToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Repository<Project>().GetByIdAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotFoundException("Project", request.ProjectId);
            }

            foreach (var unitInput in request.Units)
            {
                if (await _unitOfWork.Repository<Project>()
                    .ExistsAsync(p =>
                        p.Id == request.ProjectId &&
                        p.Properties.Any(u => u.Name == unitInput.Name.En)))
                {
                    throw new ValidatationException($"A property with the name '{unitInput.Name.En}' already exists.");
                }

                var parsedType = ParseUnitType(unitInput.Type);
                var parsedStatus = ParseUnitStatus(unitInput.Status, parsedType);

                var property = new RealEstate.Domain.Entities.Unit
                {
                    Name = unitInput.Name.En,
                    Description = string.IsNullOrWhiteSpace(unitInput.Description.En) ? null : unitInput.Description.En,
                    NoBathRoom = unitInput.NoBathRoom,
                    NoKitchen = unitInput.NoKithchen,
                    NoBedRoom = unitInput.NoBedRoom,
                    Price = unitInput.Price,
                    PropertyType = unitInput.PropertyType,
                    ProjectId = request.ProjectId,
                    IsFeatured = unitInput.IsFeatured,
                    Area = unitInput.Area,
                    FloorNumber = unitInput.FloorNumber,
                    FloorName = unitInput.FloorName,
                    View = unitInput.View,
                    Type = parsedType,
                    Status = parsedStatus
                };

                if (unitInput.FacilityIds.Count > 0)
                {
                    foreach (var facilityId in unitInput.FacilityIds)
                    {
                        var facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(facilityId);
                        if (facility == null)
                        {
                            throw new ValidatationException($"Facility with ID '{facilityId}' does not exist.");
                        }

                        property.PropertyFacilities.Add(new UnitFacility { FacilityId = facilityId });
                    }
                }

                if (unitInput.ServicesIds.Count > 0)
                {
                    foreach (var serviceId in unitInput.ServicesIds)
                    {
                        var service = await _unitOfWork.Repository<RealEstate.Domain.Entities.Service>().GetByIdAsync(serviceId);
                        if (service == null)
                        {
                            throw new ValidatationException($"Service with ID '{serviceId}' does not exist.");
                        }

                        property.UnitServices.Add(new UnitService { ServiceId = serviceId });
                    }
                }

                foreach (var payment in unitInput.PaymentPlans)
                {
                    property.PaymentPlans.Add(new PaymentPlan
                    {
                        CommissionRate = 0,
                        InstallmentDownPayment = payment.InstallmentDownPayment ?? 0,
                        InstallmentMothes = payment.InstallmentMonthes ?? 0,
                        PaymentType = string.Equals(payment.PaymentType, PaymentType.Cash.ToString(), StringComparison.OrdinalIgnoreCase)
                            ? PaymentType.Cash
                            : PaymentType.Installment,
                        Status = PropertyStatus.Approved
                    });
                }

                await _unitOfWork.Repository<RealEstate.Domain.Entities.Unit>().AddAsync(property);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _translationService.SaveTranslationsAsync(
                    TranslatableEntity.Unit,
                    property.Id,
                    new Dictionary<string, TranslationInputDto>
                    {
                        ["Name"] = unitInput.Name,
                        ["Description"] = unitInput.Description
                    },
                    cancellationToken);
            }

            return project.Id;
        }

        private static enTyoeUnit? ParseUnitType(string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return null;
            }

            return Enum.TryParse<enTyoeUnit>(type, true, out var parsed)
                ? parsed
                : null;
        }

        private static enStatusUnit? ParseUnitStatus(string? status, enTyoeUnit? type)
        {
            if (type != enTyoeUnit.Buy || string.IsNullOrWhiteSpace(status))
            {
                return null;
            }

            return Enum.TryParse<enStatusUnit>(status, true, out var parsed)
                ? parsed
                : null;
        }
    }
}
