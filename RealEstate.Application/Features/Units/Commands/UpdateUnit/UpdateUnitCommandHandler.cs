using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.Properties.Models;
using RealEstate.Application.Features.Units.Commands.UpdateUnit;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Properties.Commands.UpdateProperty;

public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUnitCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
       

       var Unit=  await _unitOfWork.Repository<Domain.Entities.Unit>()
        .Query().Include(u => u.Project).Include(u=>u.PropertyDetails.Any(p=>p.Status==PropertyStatus.Approved))
        .Where(u => u.IsActive && u.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (Unit == null)
            throw new NotFoundException("Property", request.Id);

        var propertyDetailsId = Unit?.PropertyDetails
    .FirstOrDefault(p => p.Status == PropertyStatus.Approved)?.Id ??0;

        var UnitDetails = await _unitOfWork.Repository<UnitDetail>().GetByIdAsync(propertyDetailsId);
        if(UnitDetails == null)
            throw new Exceptions.ValidtationException("Unit Not Have Unit Details");

        if (await _unitOfWork.Repository<Project>()
    .ExistsAsync(p =>
        p.Id == Unit.ProjectId &&
        p.Properties.Any(u => u.Name == request.Name)))
        {
            throw new Exceptions.ValidtationException($"A Unit Exist in Project Cannot Update for  '{request.Name}' already exists.");
        }




        Unit.Name = request.Name;
        Unit.Description = request.Description;
        Unit.Price = request.Price;
        Unit.PropertyType = request.PropertyType;
        UnitDetails.InstallmentDownPayment = request.installmentDownPayment;
        UnitDetails.CommissionRate = request.CmmisionRate;
        UnitDetails.InstallmentYears =Convert.ToInt32(request.installmentYears);
        UnitDetails.PaymentType = request!.PaymentType.ToLower() == PaymentType.Cash.ToString().ToLower() ? PaymentType.Cash : PaymentType.Installment;





        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Id;
    }
}

