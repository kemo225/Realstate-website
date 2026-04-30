using FluentValidation;

namespace RealEstate.Application.Features.Leads.Commands.CreateLead;

public class UpdateLeadVieewdCommandValidator : AbstractValidator<UpdateLeadViewdCommand>
{
    public UpdateLeadVieewdCommandValidator()
    {
        RuleFor(x => x.LeadId)
            .GreaterThan(0).WithMessage("LeadId must Greater Than 0");

        
    }
}
