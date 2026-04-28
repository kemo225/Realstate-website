using MediatR;
using Microsoft.AspNetCore.Identity;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Common;
using RealEstate.Application.Features.Auth.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Auth.Commands.UpdateAdmin;

public record UpdateAdminCommand(UpdateAdminRequest Request) : IRequest<Result<UpdateAdminResponse>>;

public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, Result<UpdateAdminResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUserService _currentUserService;

    public UpdateAdminCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
    }

    public async Task<Result<UpdateAdminResponse>> Handle(UpdateAdminCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Result<UpdateAdminResponse>.Failure("User is not authenticated.");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result<UpdateAdminResponse>.Failure("Admin account was not found.");
        }

        if (!await _userManager.IsInRoleAsync(user, "Admin"))
        {
            return Result<UpdateAdminResponse>.Failure("Only admins can update admin profile details.");
        }

        user.UserName = command.Request.UserName;
        user.Email = command.Request.Email;
        user.PhoneNumber = command.Request.PhoneNumber;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return Result<UpdateAdminResponse>.Failure(IdentityErrorMapper.ToMessages(updateResult.Errors));
        }

        var response = new UpdateAdminResponse(
            user.UserName ?? string.Empty,
            user.Email ?? string.Empty,
            user.PhoneNumber);

        return Result<UpdateAdminResponse>.Success(response);
    }
}
