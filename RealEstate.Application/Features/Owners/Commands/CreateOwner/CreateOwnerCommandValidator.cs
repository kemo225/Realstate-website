using FluentValidation;

namespace RealEstate.Application.Features.Owners.Commands.CreateOwner;

public class CreateOwnerCommandValidator : AbstractValidator<CreateOwnerCommand>
{
    public CreateOwnerCommandValidator()
    {
        RuleFor(p => p.FullName).NotEmpty().MaximumLength(200);
        RuleFor(p => p.Phone)
            .NotEmpty()
            .Length(11)
            .Matches(@"^\+?\d+$")
            .WithMessage("Phone must contain only digits and may start with +");
        RuleFor(p => p.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
    }
}

