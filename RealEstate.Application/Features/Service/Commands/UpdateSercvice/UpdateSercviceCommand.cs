using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Service.Commands.UpdateSercvice;

public class UpdateSercviceCommand : IRequest<int>
{
    public int Id { get; set; }
    public TranslationInputDto Name { get; set; } = new();

}
