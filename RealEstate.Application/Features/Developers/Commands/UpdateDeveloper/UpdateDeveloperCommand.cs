using MediatR;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Developers.Commands.UpdateDeveloper;

public record UpdateDeveloperCommand : IRequest<MediatR.Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class UpdateDeveloperCommandHandler : IRequestHandler<UpdateDeveloperCommand, MediatR.Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDeveloperCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MediatR.Unit> Handle(UpdateDeveloperCommand request, CancellationToken cancellationToken)
    {
        var developer = await _unitOfWork.Repository<Developer>().GetByIdAsync(request.Id);

        if (developer == null)
        {
            throw new NotFoundException("Developer", request.Id);
        }

        developer.Name = request.Name;
        developer.Description = request.Description;

        _unitOfWork.Repository<Developer>().Update(developer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MediatR.Unit.Value;
    }
}
