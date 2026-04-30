using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Owners.Commands.UpdateOwner;

public class UpdateApplicantCommandHandler : IRequestHandler<UpdateApplicantCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateApplicantCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(UpdateApplicantCommand request, CancellationToken cancellationToken)
    {
        var owner = await _unitOfWork.Repository<Aplicant>().GetByIdAsync(request.Id);
        if (owner == null) throw new RealEstate.Application.Exceptions.NotFoundException("Owner", request.Id);

        if (await _unitOfWork.Repository<Aplicant>().ExistsAsync(o => o.Email == request.Email && o.Id!=request.Id))
            throw new ValidatationException("Email Is Exist For Another Owner");

        owner.FullName = request.FullName;
        owner.Email = request.Email;
        owner.Phone = request.Phone;
        owner.Notes = request.Notes;

        _unitOfWork.Repository<Aplicant>().Update(owner);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(owner.Id);
    }
}

