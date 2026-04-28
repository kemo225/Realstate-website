using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace RealEstate.Application.Features.Auth.Common;

public static class IdentityErrorMapper
{
    public static string[] ToMessages(IEnumerable<IdentityError> errors)
    {
        return errors
            .Select(MapError)
            .Distinct()
            .ToArray();
    }

    private static string MapError(IdentityError error)
    {
        return error.Code switch
        {
            "DuplicateUserName" => "Username is already taken.",
            "DuplicateEmail" => "Email is already in use.",
            "InvalidUserName" => "Username is invalid.",
            "InvalidEmail" => "Email is invalid.",
            "PasswordMismatch" => "Current password is incorrect.",
            "PasswordTooShort" => "Password does not meet minimum length requirements.",
            "PasswordRequiresNonAlphanumeric" => "Password must contain at least one special character.",
            "PasswordRequiresDigit" => "Password must contain at least one number.",
            "PasswordRequiresLower" => "Password must contain at least one lowercase letter.",
            "PasswordRequiresUpper" => "Password must contain at least one uppercase letter.",
            "InvalidToken" => "Password reset token is invalid or expired.",
            "ConcurrencyFailure" => "The record was updated by another process. Please retry.",
            _ => "Identity operation failed. Please verify your input and try again."
        };
    }
}
