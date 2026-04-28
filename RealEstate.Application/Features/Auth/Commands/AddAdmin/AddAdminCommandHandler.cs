using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Common;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Auth.Commands.AddAdmin;

public class AddAdminCommandHandler : IRequestHandler<AddAdminCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AddAdminCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(AddAdminCommand command, CancellationToken cancellationToken)
    {
        if(await _userManager.FindByEmailAsync(command.Request.Email) != null)
        {
            throw new Exceptions.ValidtationException($"A user with the email '{command.Request.Email}' already exists.");
        }
        var user = new ApplicationUser
        {
            UserName = command.Request.Email,
            Email = command.Request.Email,
            FirstName = command.Request.FirstName,
            LastName = command.Request.LastName,
            EmailConfirmed = true // Admins created by other admins are pre-confirmed
        };

        var result = await _userManager.CreateAsync(user, command.Request.Password);

        if (!result.Succeeded)
        {
            return Result<string>.Failure(IdentityErrorMapper.ToMessages(result.Errors));
        }

        await _userManager.AddToRoleAsync(user, "Admin");

        return Result<string>.Success(user.Id);
    }
}
