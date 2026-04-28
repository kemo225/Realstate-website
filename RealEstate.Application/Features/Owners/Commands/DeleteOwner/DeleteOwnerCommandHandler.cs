using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Owners.Commands.DeleteOwner;

public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOwnerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
    {
        var owner = await _unitOfWork.Repository<Owner>().GetByIdAsync(request.Id);
        if (owner == null) throw new RealEstate.Application.Exceptions.NotFoundException("Owner", request.Id);


        _unitOfWork.Repository<Owner>().Delete(owner);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

