using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Domain.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Application.Exceptions;

namespace RealEstate.Application.Features.Leads.Commands.CreateLead;

public class CreateLeadCommandHandler : IRequestHandler<CreateLeadCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLeadCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.Repository<Domain.Entities.Unit>().ExistsAsync(p => p.Id == request.UnitId) == false)
            throw new ValidtationException("The specified property does not exist.");
        if (await _unitOfWork.Repository<Lead>().ExistsAsync(l => l.Email == request.Email && l.PropertyId == request.UnitId))
            throw new ValidtationException("A lead with the same email already exists for this property.");



        var lead = new Lead
        {
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            PropertyId = request.UnitId,
            Notes = request.Notes,
            StatusLead = enStatusLead.Pending
        };

        await _unitOfWork.Repository<Lead>().AddAsync(lead);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(lead.Id);
    }
}

