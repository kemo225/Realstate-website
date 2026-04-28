using MediatR;
using Microsoft.AspNetCore.Identity;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Auth.Commands.ForgotPassword;

public record ForgotPasswordCommand(ForgotPasswordRequest Request) : IRequest<Result<ForgotPasswordResponse>>;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<ForgotPasswordResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<ForgotPasswordResponse>> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Request.Email);
        string? resetToken = null;

        if (user != null)
        {
            resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        return Result<ForgotPasswordResponse>.Success(new ForgotPasswordResponse(command.Request.Email, resetToken));
    }
}
