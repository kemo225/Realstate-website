using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Leads.Commands.DeleteLead;

public class DeleteLeadCommandHandler : IRequestHandler<DeleteLeadCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLeadCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = await _unitOfWork.Repository<Lead>().GetByIdAsync(request.Id);

        if (lead == null)
            throw new RealEstate.Application.Exceptions.NotFoundException("Lead", request.Id);
        lead.isActive = false;

        _unitOfWork.Repository<Lead>().Update(lead);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
