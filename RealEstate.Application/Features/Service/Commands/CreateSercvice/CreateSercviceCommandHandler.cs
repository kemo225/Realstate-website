using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Facilities.Commands.CreateFacility;

public class CreateSercviceCommandHandler : IRequestHandler<CreateSercviceCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSercviceCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateSercviceCommand request, CancellationToken cancellationToken)
    {
        var Srvice = new Domain.Entities.Service
        {
            Name = request.Name
        };

        await _unitOfWork.Repository<Domain.Entities.Service>().AddAsync(Srvice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Srvice.Id;
    }
}
