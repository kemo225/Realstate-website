using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Requests.Commands.CreateRequest;

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRequestCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        // 1. Check if Unit exists by Name
        var unit = await _unitOfWork.Repository<RealEstate.Domain.Entities.Unit>().Query()
            .FirstOrDefaultAsync(u => u.Name == request.UnitName, cancellationToken);

        if (unit == null)
        {
            throw new NotFoundException("Unit does not exist");
        }

        // 2. Check Unit status
        // User said: If Unit.IsActive == true -> "This unit is still active and not available for request"
        if (unit.IsActive)
        {
            throw new ValidatationException("This unit is still active and not available for request");
        }

        // 10. Prevent duplicate request: Same applicant cannot request same unit twice
        var existingRequest = await _unitOfWork.Repository<Request>().Query()
            .AnyAsync(r => r.UnitId == unit.Id && r.ApplicantId == request.ApplicantId && r.Status != RequestStatus.Rejected, cancellationToken);

        if (existingRequest)
        {
            throw new ValidatationException("You have already submitted a pending or approved request for this unit.");
        }

        // 3. Create Request
        var entity = new Request
        {
            UnitId = unit.Id,
            ApplicantId = request.ApplicantId,
            Status = RequestStatus.Pending
        };

        await _unitOfWork.Repository<Request>().AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
