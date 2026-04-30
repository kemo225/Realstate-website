using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public RefreshTokenCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _userManager = userManager;
    }

    public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var tokenValue = command.Request.RefreshToken.Trim();

        var existingRefreshToken = await _unitOfWork.Repository<Domain.Entities.RefreshToken>()
            .Query()
            .FirstOrDefaultAsync(t => t.Token == tokenValue, cancellationToken);

        if (existingRefreshToken == null || !existingRefreshToken.IsActive)
        {
            return Result<AuthResponse>.Failure("Invalid or expired refresh token.");
        }

        var user = await _userManager.FindByIdAsync(existingRefreshToken.UserId);
        if (user == null)
        {
            return Result<AuthResponse>.Failure("User associated with refresh token was not found.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}".Trim())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var newAccessToken = _tokenService.GenerateAccessToken(claims);
        var newRefreshTokenValue = _tokenService.GenerateRefreshToken();

        existingRefreshToken.Revoked = DateTime.UtcNow;
        _unitOfWork.Repository<Domain.Entities.RefreshToken>().Update(existingRefreshToken);

        var newRefreshToken = new Domain.Entities.RefreshToken
        {
            Token = newRefreshTokenValue,
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
            CreatedById = user.Id
        };

        await _unitOfWork.Repository<Domain.Entities.RefreshToken>().AddAsync(newRefreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AuthResponse>.Success(new AuthResponse(
            newAccessToken,
            newRefreshTokenValue,
            user.Email ?? string.Empty,
            $"{user.FirstName} {user.LastName}".Trim()
        ));
    }
}
