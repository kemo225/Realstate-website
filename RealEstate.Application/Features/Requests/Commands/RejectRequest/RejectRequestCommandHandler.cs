using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Requests.Commands.RejectRequest;

public class RejectRequestCommandHandler : IRequestHandler<RejectRequestCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public RejectRequestCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RejectRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Request>().Query()
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new Exceptions.NotFoundException("Request", request.Id);
        }

        if (entity.Status != RequestStatus.Pending)
        {
            throw new Exceptions.ValidatationException($"Request is already {entity.Status}");
        }

        entity.Status = RequestStatus.Rejected;

        _unitOfWork.Repository<Request>().Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
