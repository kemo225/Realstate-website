using FluentValidation;

namespace RealEstate.Application.Features.Auth.Queries.GetAdmins;

public class GetAdminsQueryValidator : AbstractValidator<GetAdminsQuery>
{

    public GetAdminsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);

 
    }
}
