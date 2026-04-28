using MediatR;
using Microsoft.AspNetCore.Identity;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Common;
using RealEstate.Application.Features.Auth.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Auth.Commands.UpdatePassword;

public record UpdatePasswordCommand(UpdatePasswordRequest Request) : IRequest<Result>;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePasswordCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Result.Failure("User is not authenticated.");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure("User account was not found.");
        }

        var updateResult = await _userManager.ChangePasswordAsync(user, command.Request.OldPassword, command.Request.NewPassword);
        if (!updateResult.Succeeded)
        {
            return Result.Failure(IdentityErrorMapper.ToMessages(updateResult.Errors));
        }

        return Result.Success();
    }
}
