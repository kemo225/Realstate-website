using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.Facilities.Commands.DeleteFacility
{
    public class DeleteFacilityHandler : IRequestHandler<DeleteFacilityCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFacilityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async   Task<bool> Handle(DeleteFacilityCommand request, CancellationToken cancellationToken)
        {
            var facilty = await _unitOfWork.Repository<Domain.Entities.Facility>()
                .Query().Include(f=>f.PropertyFacilities).FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
            if (facilty == null)
                throw new NotFoundException("Facilty Not Exist");
            if(facilty.PropertyFacilities != null && facilty.PropertyFacilities.Count > 0)
                throw new ValidtationException("Cannot delete facility that is associated with properties.");
            return true;


        }
    }
}
