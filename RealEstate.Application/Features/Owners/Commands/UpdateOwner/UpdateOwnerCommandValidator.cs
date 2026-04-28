using FluentValidation;

namespace RealEstate.Application.Features.Owners.Commands.UpdateOwner;

public class UpdateOwnerCommandValidator : AbstractValidator<UpdateOwnerCommand>
{
    public UpdateOwnerCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.FullName).NotEmpty().MaximumLength(200);
        RuleFor(p => p.Phone)
            .NotEmpty()
            .MaximumLength(20)
            .Matches(@"^\+?\d+$")
            .WithMessage("Phone must contain only digits and may start with +"); RuleFor(p => p.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
    }
}

