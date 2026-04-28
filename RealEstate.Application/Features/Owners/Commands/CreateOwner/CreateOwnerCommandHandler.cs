using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Owners.Commands.CreateOwner;

public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOwnerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
    {

        if (await _unitOfWork.Repository<Owner>().ExistsAsync(o=>o.Email==request.Email))
            throw new ValidtationException("Email Is Exist For Another Owner");
        var owner = new Owner
        {
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            Notes = request.Notes,
        };

        await _unitOfWork.Repository<Owner>().AddAsync(owner);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(owner.Id);
    }
}

