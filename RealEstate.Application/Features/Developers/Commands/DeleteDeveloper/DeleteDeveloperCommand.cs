using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Developers.Commands.DeleteDeveloper;

public record DeleteDeveloperCommand(int Id) : IRequest<MediatR.Unit>;

public class DeleteDeveloperCommandHandler : IRequestHandler<DeleteDeveloperCommand, MediatR.Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDeveloperCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MediatR.Unit> Handle(DeleteDeveloperCommand request, CancellationToken cancellationToken)
    {
        var developer = await _unitOfWork.Repository<Developer>()
            .Query()
            .Include(d => d.Projects)
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (developer == null)
        {
            throw new NotFoundException("Developer", request.Id);
        }

        if (developer.Projects.Any())
        {
            throw new ValidatationException("Cannot delete developer because it has associated projects.");
        }

        // Soft delete
        developer.IsDeleted = true;
        foreach (var gallery in developer.Gallery)
        {
            _unitOfWork.Repository<DeveloperGallery>().Delete(gallery);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MediatR.Unit.Value;
    }
}
