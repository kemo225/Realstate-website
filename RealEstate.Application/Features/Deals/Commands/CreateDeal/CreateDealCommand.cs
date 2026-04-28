using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Deals.Commands.CreateDeal;

public record CreateDealCommand(
    int UnitId,
    string FullName,
    string Email,
    string Phone,
    DateTime DealDate,
    string DealType
    ) : IRequest<Result<int>>;

public class CreateDealCommandHandler : IRequestHandler<CreateDealCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateDealCommand request, CancellationToken cancellationToken)
    {

        var unit = await _unitOfWork.Repository<Domain.Entities.Unit>()
     .Query()
     .Include(u => u.Project)
     .Include(u => u.PropertyDetails)
     .Where(u => u.IsActive && u.Id == request.UnitId)
     .FirstOrDefaultAsync(cancellationToken);

        if (unit == null)
            throw new NotFoundException("Property", request.UnitId);

        var unitDetails = unit.PropertyDetails
            .FirstOrDefault(p => p.Status == PropertyStatus.Approved);

        if (unitDetails == null)
            throw new Exceptions.ValidtationException("Unit Not Have Approved Details");

        if (unitDetails.Status == PropertyStatus.Sold)
            throw new Exceptions.ValidtationException("Unit already sold");

        var deal = new Deal
        {
            DealDate = request.DealDate,
            ClientName = request.FullName,
            Phone = request.Phone,
            Email = request.Email,
            UnitRequestId = request.UnitId
        };

        unitDetails.Status = PropertyStatus.Sold;
        unit.IsActive = false;
        unit.SoldCount++;

        await _unitOfWork.Repository<Deal>().AddAsync(deal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(deal.Id);
    }
}

