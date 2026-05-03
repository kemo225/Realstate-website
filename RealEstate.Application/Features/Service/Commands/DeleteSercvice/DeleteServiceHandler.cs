using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Service.Commands.DeleteSercvice
{
    public class DeleteServiceHandler : IRequestHandler<DeleteSercviceCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSercviceCommand request, CancellationToken cancellationToken)
        {
            var facilty = await _unitOfWork.Repository<Domain.Entities.Service>()
                .Query().Include(f => f.UnitServices).FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
            if (facilty == null)
                throw new NotFoundException("");
            if (facilty.UnitServices != null && facilty.UnitServices.Count > 0)
                throw new ValidatationException("Cannot delete service that is associated with properties.");
            return true;


        }
    }
}
