using System.Linq;
using AutoMapper;
using RealEstate.Application.Features.Owners.Queries.GetOwners;
using RealEstate.Application.Features.Deals.Models;
using RealEstate.Application.Features.Leads.Models;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Application.Features.Locations.Models;
using RealEstate.Application.Features.Projects.Models;
using RealEstate.Application.Features.Facilities.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Owner, OwnerDto>();

 


        CreateMap<Lead, LeadDto>()
            .ForMember(d => d.PropertyName, opt => opt.MapFrom(s => s.Property != null ? s.Property.Name : ""))
                    .ForMember(d => d.ProjectName, opt => opt.MapFrom(s => s.Property != null && s.Property.Project != null ? s.Property.Project.Name : ""));


        CreateMap<Deal, DealDto>();

        CreateMap<Deal, DealDetailsDto>();

        CreateMap<Location, LocationDto>();
        
        CreateMap<Project, ProjectDto>()
            .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.Location != null ? $"{s.Location.City}, {s.Location.District}" : ""))
            .ForMember(d => d.ImageUrls, opt => opt.MapFrom(s => s.Images.Select(i => i.ImageUrl)));

        CreateMap<Facility, FacilityDto>();
        CreateMap<Service, SercviceDto>();

    }
}
