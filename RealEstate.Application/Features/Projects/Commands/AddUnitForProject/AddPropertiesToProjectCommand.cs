using MediatR;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.Projects.Commands.AddPropertyForProject
{
    public class AddPropertiesToProjectCommand:IRequest<int>
    {
        public int ProjectId { get; set; }
        public List<CreatePropertyDto> Properties { get; set; } = new List<CreatePropertyDto>();
    }
    public class CreatePropertyDto
    {
       
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public PropertyType PropertyType { get; set; } 
        public string PaymentType { get; set; } = string.Empty;
        public int NoBathRoom { get; set; }
        public int NoBedRoom { get; set; }
        public int FloorNumber { get; set; }
        public int Area { get; set; }

        public int NoKithchen { get; set; }
        public string? FloorName { get; set; }
        public enView View { get; set; }
        public int? InstallmentYears { get; set; }
        public decimal? InstallmentDownPayment { get; set; }
        public bool IsFeatured { get; set; }
        public List<int> FacilityIds { get; set; } = new();
        public List<int> ServicesIds { get; set; } = new();

    }
}
