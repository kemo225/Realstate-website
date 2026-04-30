using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Domain.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Application.Exceptions;

namespace RealEstate.Application.Features.Leads.Commands.CreateLead;

public class UpdateLeadVieewdCommandHandler : IRequestHandler<UpdateLeadViewdCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLeadVieewdCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateLeadViewdCommand request, CancellationToken cancellationToken)
    {
        var lead = await _unitOfWork.Repository<Domain.Entities.Lead>().GetByIdAsync(request.LeadId);
        if (lead is null)
            throw new NotFoundException("The specified Lead Not Exist");

        lead.StatusLead = request.status;

     

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}

