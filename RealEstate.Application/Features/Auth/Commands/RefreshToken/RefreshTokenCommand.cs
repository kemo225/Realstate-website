using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Models;

namespace RealEstate.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(RefreshTokenRequest Request) : IRequest<Result<AuthResponse>>;
