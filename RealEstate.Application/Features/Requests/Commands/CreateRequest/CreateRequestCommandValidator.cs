using FluentValidation;
using RealEstate.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Requests.Commands.CreateRequest;

public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateRequestCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.UnitName)
            .NotEmpty().WithMessage("Unit name is required.");

        RuleFor(v => v.ApplicantId)
            .NotEmpty().WithMessage("Applicant ID is required.")
            .MustAsync(ApplicantExists).WithMessage("Applicant does not exist.");
    }

    private async Task<bool> ApplicantExists(int applicantId, CancellationToken cancellationToken)
    {
        return await _context.Applicants.AnyAsync(a => a.Id == applicantId, cancellationToken);
    }
}
