using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Auth.Commands.Logout;

public record LogoutCommand(string RefreshToken) : IRequest<RealEstate.Application.Common.Models.Result>;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, RealEstate.Application.Common.Models.Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public LogoutCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<RealEstate.Application.Common.Models.Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
       throw new Exceptions.ValidatationException("User is not authenticated.");

        var refreshToken = await _unitOfWork.Repository<RealEstate.Domain.Entities.RefreshToken>()
            .Query()
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.UserId == userId && rt.Revoked == null, cancellationToken);

        if (refreshToken == null)
            throw new Exceptions.ValidatationException("User is not authenticated.");


        refreshToken.Revoked = DateTime.UtcNow;
        _unitOfWork.Repository<RealEstate.Domain.Entities.RefreshToken>().Update(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
