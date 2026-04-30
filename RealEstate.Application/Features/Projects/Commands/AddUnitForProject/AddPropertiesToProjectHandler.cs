using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.Projects.Commands.AddPropertyForProject
{
    public class AddPropertiesToProjectHandler : IRequestHandler<AddPropertiesToProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPropertiesToProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(AddPropertiesToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Repository<Project>().GetByIdAsync(request.ProjectId);
            if (project == null)
                throw new NotFoundException("Project", request.ProjectId);

        

            foreach (var Unit in request.Units)
            {
                if (await _unitOfWork.Repository<Project>()
    .ExistsAsync(p =>
        p.Id == request.ProjectId &&
        p.Properties.Any(u => u.Name == Unit.Name)))
                {
                    throw new Exceptions.ValidatationException($"A property with the name '{Unit.Name}' already exists.");
                }

                var property = new Domain.Entities.Unit
                {
                  
                    Name = Unit.Name,
                    Description = Unit.Description,
                    NoBathRoom= Unit.NoBathRoom,
                    NoKitchen = Unit.NoKithchen,
                    NoBedRoom = Unit.NoBedRoom,
                    Price = Unit.Price,
                    PropertyType = Unit.PropertyType,
                    ProjectId = request.ProjectId,
                    IsFeatured = Unit.IsFeatured,
                    Area=Unit.Area,
                    FloorNumber=Unit.FloorNumber,
                    FloorName=Unit.FloorName
                };
                if(Unit.FacilityIds.Count>0)
                {
                    foreach (var facilityId in Unit.FacilityIds)
                    {
                        var facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(facilityId);
                        if (facility == null)
                            throw new ValidatationException($"Facility with ID '{facilityId}' does not exist.");
                        property.PropertyFacilities.Add(new UnitFacility { FacilityId = facilityId });
                    }
                }

                if (Unit.ServicesIds.Count > 0)
                {
                    foreach (var serviceId in Unit.ServicesIds)
                    {
                        var service = await _unitOfWork.Repository<Domain.Entities.Service>().GetByIdAsync(serviceId);
                        if (service == null)
                            throw new ValidatationException($"Service with ID '{serviceId}' does not exist.");
                        property.UnitServices.Add(new UnitService { ServiceId = serviceId });
                    }
                }
                foreach (var Payment in Unit.PaymentPlans)
                {
                    property.PaymentPlans.Add(new PaymentPlan()
                    {
                        CommissionRate = 0,
                        InstallmentDownPayment = Payment.InstallmentDownPayment ?? 0,
                        InstallmentMothes = Payment.InstallmentMonthes ?? 0,
                        PaymentType = (Payment.PaymentType.ToLower() == PaymentType.Cash.ToString().ToLower()) ? PaymentType.Cash : PaymentType.Installment,
                        Status = PropertyStatus.Approved,
                    });
                }


                project.Properties.Add(property);
            }

            await _unitOfWork.SaveChangesAsync();
            return project.Id;
        }
    }
}
