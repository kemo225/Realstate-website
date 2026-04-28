using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Owners.Commands.UpdateOwner;

public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOwnerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        var owner = await _unitOfWork.Repository<Owner>().GetByIdAsync(request.Id);
        if (owner == null) throw new RealEstate.Application.Exceptions.NotFoundException("Owner", request.Id);

        if (await _unitOfWork.Repository<Owner>().ExistsAsync(o => o.Email == request.Email && o.Id!=request.Id))
            throw new ValidtationException("Email Is Exist For Another Owner");

        owner.FullName = request.FullName;
        owner.Email = request.Email;
        owner.Phone = request.Phone;
        owner.Notes = request.Notes;

        _unitOfWork.Repository<Owner>().Update(owner);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(owner.Id);
    }
}

