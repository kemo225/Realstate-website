using MediatR;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Developers.Commands.CreateDeveloper;

public record CreateDeveloperCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDeveloperCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
    {
        var developer = new Developer
        {
            Name = request.Name,
            Description = request.Description
        };

        await _unitOfWork.Repository<Developer>().AddAsync(developer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return developer.Id;
    }
}
