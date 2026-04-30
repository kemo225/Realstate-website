using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Features.Facilities.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.Facilities.Queries.GetFacilityById
{
    internal class GetFaciltyByQueryId : IRequestHandler<GetFacilityByIdQuery, FacilityDto>
    {
       private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;


            public GetFaciltyByQueryId(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

        public async Task<FacilityDto> Handle(GetFacilityByIdQuery request, CancellationToken cancellationToken)
        {
            var Facility = await _unitOfWork.Repository<Domain.Entities.Facility>().Query()
                .FirstOrDefaultAsync(r => r.Id == request.Id);
            if (Facility == null)
            {
                throw new Exception("Facility not found");
            }
            return _mapper.Map<FacilityDto>(Facility);
        }

        
    }
}
