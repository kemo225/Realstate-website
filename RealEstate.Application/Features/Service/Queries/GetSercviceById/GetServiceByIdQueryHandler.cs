using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Features.Facilities.Models;
using RealEstate.Application.Features.Facilities.Queries.GetFacilityById;
using RealEstate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.Service.Queries.GetSercviceById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetSercviceByIdQuery, SercviceDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        
        public GetServiceByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SercviceDto> Handle(GetSercviceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.Repository<Domain.Entities.Service>().Query()
                .FirstOrDefaultAsync(cancellationToken);
            if(service == null) {
                throw new Exception("Service not found");
            }
            return _mapper.Map<SercviceDto>(service);
        }
     
    }
}
