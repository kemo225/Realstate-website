using MediatR;
using Microsoft.AspNetCore.Identity;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Common;
using RealEstate.Application.Features.Auth.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Auth.Commands.ResetPassword;

public record ResetPasswordCommand(ResetPasswordRequest Request) : IRequest<Result>;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Request.Email);
        if (user == null)
        {
            return Result.Failure("Unable to reset password for the provided account.");
        }

        var resetResult = await _userManager.ResetPasswordAsync(user, command.Request.Token, command.Request.NewPassword);
        if (!resetResult.Succeeded)
        {
            return Result.Failure(IdentityErrorMapper.ToMessages(resetResult.Errors));
        }

        return Result.Success();
    }
}
