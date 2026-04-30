using FluentValidation;

namespace RealEstate.Application.Features.Auth.Commands.AddAdmin;

public class AddAdminCommandValidator : AbstractValidator<AddAdminCommand>
{
    public AddAdminCommandValidator()
    {
        RuleFor(x => x.Request.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Request.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Request.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Request.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}
