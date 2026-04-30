using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Owners.Commands.DeleteOwner;

public class DeleteApplicantCommandHandler : IRequestHandler<DeleteApplicantCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteApplicantCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteApplicantCommand request, CancellationToken cancellationToken)
    {
        var Applicant = await _unitOfWork.Repository<Aplicant>().Query().Include(a=>a.Requests).FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (Applicant == null) throw new RealEstate.Application.Exceptions.NotFoundException("Applicant", request.Id);

        if (Applicant.Requests != null)
            throw new ValidatationException("Cannot Remove Applicant");


        _unitOfWork.Repository<Aplicant>().Delete(Applicant);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

