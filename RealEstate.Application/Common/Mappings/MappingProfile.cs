using System.Linq;
using AutoMapper;
using RealEstate.Application.Features.Owners.Queries.GetOwners;
using RealEstate.Application.Features.Deals.Models;
using RealEstate.Application.Features.Leads.Models;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Application.Features.Locations.Models;
using RealEstate.Application.Features.Projects.Models;
using RealEstate.Application.Features.Facilities.Models;
using RealEstate.Application.Features.Developers.Models;
using RealEstate.Application.Features.Requests.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Aplicant, OwnerDto>();

 


        CreateMap<Lead, LeadDto>()
            .ForMember(d => d.PropertyName, opt => opt.MapFrom(s => s.Unit != null ? s.Unit.Name : ""))
            .ForMember(d => d.UnitId, opt => opt.MapFrom(s => s.Unit != null ? s.Unit.Id : 0))
            .ForMember(d => d.ProjectName, opt => opt.MapFrom(s => s.Unit != null && s.Unit.Project != null ? s.Unit.Project.Name : ""))
            .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => s.CreatedByUser != null ? s.CreatedByUser.UserName : ""))
            .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(s => s.UpdatedByUser != null ? s.UpdatedByUser.UserName : ""));


        CreateMap<Deal, DealDto>();

        CreateMap<Deal, DealDetailsDto>();

        CreateMap<Location, LocationDto>()
            .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => s.CreatedByUser != null ? s.CreatedByUser.UserName : ""))
            .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(s => s.UpdatedByUser != null ? s.UpdatedByUser.UserName : ""));
        
        CreateMap<Project, ProjectDto>()
            .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.Location != null ? $"{s.Location.City}, {s.Location.District}" : ""))
            .ForMember(d => d.DeveloperName, opt => opt.MapFrom(s => s.Developer != null ? s.Developer.Name : ""))
            .ForMember(d => d.ImageUrls, opt => opt.MapFrom(s => s.Images.Select(i => i.ImageUrl)))
            .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => s.CreatedByUser != null ? s.CreatedByUser.UserName : ""))
            .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(s => s.UpdatedByUser != null ? s.UpdatedByUser.UserName : ""));

        CreateMap<Facility, FacilityDto>();
        CreateMap<Service, SercviceDto>();

        //CreateMap<PaymentPlan, UnitDetailDto>()
        //    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Unit != null ? s.Unit.Price : 0))
        //    .ForMember(d => d.Area, opt => opt.MapFrom(s => s.Unit != null ? s.Unit.Area : 0))
        //    .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.HasValue ? s.Status.ToString() : ""))
        //    .ForMember(d => d.ApprovedBy, opt => opt.MapFrom(s => s.ApprovedByUser != null ? s.ApprovedByUser.UserName : ""))
        //    .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => s.CreatedByUser != null ? s.CreatedByUser.UserName : ""))
        //    .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(s => s.UpdatedByUser != null ? s.UpdatedByUser.UserName : ""));

        CreateMap<Developer, DeveloperDto>()
            .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => s.CreatedByUser != null ? s.CreatedByUser.UserName : ""))
            .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(s => s.UpdatedByUser != null ? s.UpdatedByUser.UserName : ""));

        CreateMap<DeveloperGallery, DeveloperGalleryDto>();
        CreateMap<Project, DeveloperProjectDto>();

        CreateMap<Request, RequestDto>()
            .ForMember(d => d.UnitName, opt => opt.MapFrom(s => s.Unit != null ? s.Unit.Name : ""))
            .ForMember(d => d.ApplicantName, opt => opt.MapFrom(s => s.Applicant != null ? s.Applicant.FullName : ""))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        CreateMap<Request, RequestDetailsDto>()
            .ForMember(d => d.UnitName, opt => opt.MapFrom(s => s.Unit != null ? s.Unit.Name : ""))
            .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.Unit != null ? s.Unit.Price : 0))
            .ForMember(d => d.UnitArea, opt => opt.MapFrom(s => s.Unit != null ? s.Unit.Area : 0))
            .ForMember(d => d.ApplicantName, opt => opt.MapFrom(s => s.Applicant != null ? s.Applicant.FullName : ""))
            .ForMember(d => d.ApplicantEmail, opt => opt.MapFrom(s => s.Applicant != null ? s.Applicant.Email : ""))
            .ForMember(d => d.ApplicantPhone, opt => opt.MapFrom(s => s.Applicant != null ? s.Applicant.Phone : ""))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => s.CreatedByUser != null ? s.CreatedByUser.UserName : ""))
            .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(s => s.UpdatedByUser != null ? s.UpdatedByUser.UserName : ""))
            .ForMember(d => d.ApprovedBy, opt => opt.MapFrom(s => s.ApprovedByUser != null ? s.ApprovedByUser.UserName : ""));
    }
}
