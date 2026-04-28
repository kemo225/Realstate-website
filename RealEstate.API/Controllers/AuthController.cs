using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.Auth.Commands.Login;
using RealEstate.Application.Features.Auth.Commands.AddAdmin;
using RealEstate.Application.Features.Auth.Commands.ForgotPassword;
using RealEstate.Application.Features.Auth.Commands.ResetPassword;
using RealEstate.Application.Features.Auth.Commands.UpdatePassword;
using RealEstate.Application.Features.Auth.Models;

namespace RealEstate.API.Controllers;

[Authorize]
public class AuthController : BaseApiController
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await Mediator.Send(new LoginCommand(request));
        if (result.Succeeded) return Ok(result);
        return Unauthorized(result);
    }

    [HttpPost("add-admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddAdmin(AddAdminRequest request)
    {
        var result = await Mediator.Send(new AddAdminCommand(request));
        if (result.Succeeded) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        return Ok(await Mediator.Send(new ForgotPasswordCommand(request)));
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var result = await Mediator.Send(new ResetPasswordCommand(request));
        if (result.Succeeded) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
    {
        var result = await Mediator.Send(new UpdatePasswordCommand(request));
        if (result.Succeeded) return Ok(result);
        return BadRequest(result);
    }
}
