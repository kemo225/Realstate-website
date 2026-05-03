using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Projects.Commands.AddPropertyForProject
{
    public class AddPropertiesToProjectCommand : IRequest<int>
    {
        public int ProjectId { get; set; }
        public List<CreatePropertyDto> Units { get; set; } = new();
    }

    public class CreatePropertyDto
    {
        public TranslationInputDto Name { get; set; } = new();
        public TranslationInputDto Description { get; set; } = new();
        public decimal Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public int NoBathRoom { get; set; }
        public int NoBedRoom { get; set; }
        public int FloorNumber { get; set; }
        public int Area { get; set; }
        public int NoKithchen { get; set; }
        public string? FloorName { get; set; }
        public enView View { get; set; }
        public List<PaymentPlanDtoCreate> PaymentPlans { get; set; } = new();
        public bool IsFeatured { get; set; }
        public List<int> FacilityIds { get; set; } = new();
        public List<int> ServicesIds { get; set; } = new();
    }

    public class PaymentPlanDtoCreate
    {
        public int? InstallmentMonthes { get; set; }
        public decimal? InstallmentDownPayment { get; set; }
        public string PaymentType { get; set; } = string.Empty;
    }
}
