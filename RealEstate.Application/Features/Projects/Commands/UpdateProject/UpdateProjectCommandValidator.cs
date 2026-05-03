using FluentValidation;

namespace RealEstate.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Name.En)
            .NotEmpty().WithMessage("Name in English (En) is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 150 characters.");

    

        When(x => !string.IsNullOrWhiteSpace(x.Name.De), () =>
            RuleFor(x => x.Name.De).MaximumLength(150));

        When(x => !string.IsNullOrWhiteSpace(x.Name.Pl), () =>
            RuleFor(x => x.Name.Pl).MaximumLength(150));

        RuleFor(x => x.Description.En)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

   
        When(x => !string.IsNullOrWhiteSpace(x.Description.De), () =>
            RuleFor(x => x.Description.De).MaximumLength(1000));

        When(x => !string.IsNullOrWhiteSpace(x.Description.Pl), () =>
            RuleFor(x => x.Description.Pl).MaximumLength(1000));

        When(x => x.LocationId.HasValue, () =>
            RuleFor(x => x.LocationId!.Value)
                .GreaterThan(0).WithMessage("LocationId must be greater than 0 when provided."));

        When(x => x.DeveloperId.HasValue, () =>
            RuleFor(x => x.DeveloperId!.Value)
                .GreaterThan(0).WithMessage("DeveloperId must be greater than 0 when provided."));
    }
}
