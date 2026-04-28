namespace RealEstate.Application.Features.Auth.Models;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string? PhoneNumber
);

public record LoginRequest(
    string Email,
    string Password
);

public record ForgotPasswordRequest(
    string Email
);

public record ForgotPasswordResponse(
    string Email,
    string? ResetToken
);

public record ResetPasswordRequest(
    string Email,
    string Token,
    string NewPassword,
    string ConfirmPassword
);

public record UpdatePasswordRequest(
    string OldPassword,
    string NewPassword,
    string ConfirmPassword
);

public record UpdateAdminRequest(
    string UserName,
    string Email,
    string? PhoneNumber
);

public record UpdateAdminResponse(
    string UserName,
    string Email,
    string? PhoneNumber
);

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    string Email,
    string FullName
);

public record RefreshTokenRequest(
    string AccessToken,
    string RefreshToken
);
