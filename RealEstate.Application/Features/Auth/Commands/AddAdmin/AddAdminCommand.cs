using RealEstate.Application.Common.Models;
using MediatR;

namespace RealEstate.Application.Features.Auth.Commands.AddAdmin;

public record AddAdminRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);

public record AddAdminCommand(AddAdminRequest Request) : IRequest<Result<string>>;
