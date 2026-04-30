using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Auth.Commands.Login;

public record LoginCommand(LoginRequest Request) : IRequest<Result<AuthResponse>>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuthResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, command.Request.Password))
        {
            return Result<AuthResponse>.Failure("Invalid email or password");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new Domain.Entities.RefreshToken
        {
            Token = refreshToken,
            Expires = System.DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
            CreatedAt = System.DateTime.UtcNow,
            CreatedById = user.Id
        };

        await _unitOfWork.Repository<Domain.Entities.RefreshToken>().AddAsync(refreshTokenEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AuthResponse>.Success(new AuthResponse(
            accessToken,
            refreshToken,
            user.Email!,
            $"{user.FirstName} {user.LastName}"
        ));
    }
}

