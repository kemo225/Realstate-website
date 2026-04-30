using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Owners.Commands.CreateOwner;

public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateApplicantCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateApplicantCommand request, CancellationToken cancellationToken)
    {

        if (await _unitOfWork.Repository<Aplicant>().ExistsAsync(o=>o.Email==request.Email))
            throw new ValidatationException("Email Is Exist For Another Owner");
        var owner = new Aplicant
        {
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            Notes = request.Notes,
        };

        await _unitOfWork.Repository<Aplicant>().AddAsync(owner);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(owner.Id);
    }
}

