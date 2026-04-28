using System.Collections.Generic;
using System.Security.Claims;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
