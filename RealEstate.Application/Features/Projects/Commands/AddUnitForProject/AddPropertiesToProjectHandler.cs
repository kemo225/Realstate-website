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

        

            foreach (var propertyDto in request.Properties)
            {
                if (await _unitOfWork.Repository<Project>()
    .ExistsAsync(p =>
        p.Id == request.ProjectId &&
        p.Properties.Any(u => u.Name == propertyDto.Name)))
                {
                    throw new Exceptions.ValidtationException($"A property with the name '{propertyDto.Name}' already exists.");
                }

                var property = new Domain.Entities.Unit
                {
                  
                    Name = propertyDto.Name,
                    Description = propertyDto.Description,
                    NoBathRoom= propertyDto.NoBathRoom,
                    NoKitchen = propertyDto.NoKithchen,
                    NoBedRoom = propertyDto.NoBedRoom,
                    Price = propertyDto.Price,
                    PropertyType = propertyDto.PropertyType,
                    ProjectId = request.ProjectId,
                    IsFeatured = propertyDto.IsFeatured
                };

                foreach (var facilityId in propertyDto.FacilityIds)
                {
                    var facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(facilityId);
                    if(facility == null)
                        throw new NotFoundException("Facility", facilityId);

                    property.PropertyFacilities.Add(new UnitFacility { FacilityId = facilityId });
                }
                foreach (var serviceId in propertyDto.ServicesIds)
                {
                    var service = await _unitOfWork.Repository<Domain.Entities.Service>().GetByIdAsync(serviceId);
                    if (service == null)
                        throw new NotFoundException("Service", serviceId);
                    property.UnitServices.Add(new UnitService { ServiceId = serviceId });
                }
                property.PropertyDetails.Add(new UnitDetail()
                {
                    CommissionRate= 0,
                    InstallmentDownPayment= propertyDto.InstallmentDownPayment ?? 0,
                    InstallmentYears= propertyDto.InstallmentYears ?? 0,
                    PaymentType= (propertyDto.PaymentType==PaymentType.Cash.ToString())?PaymentType.Cash: PaymentType.Installment,
                    Status=PropertyStatus.Approved,

                });

                project.Properties.Add(property);
            }

            await _unitOfWork.SaveChangesAsync();
            return project.Id;
        }
    }
}
