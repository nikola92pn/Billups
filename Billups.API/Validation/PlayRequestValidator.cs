using Billups.Api.Models;
using FluentValidation;

namespace Billups.Api.Validation;

public class PlayRequestValidator : AbstractValidator<PlayRequest>
{
    public PlayRequestValidator()
    {
        RuleFor(x => x.Player)
            .NotNull().WithMessage("Player choice id is required.")
            .InclusiveBetween(1,5).WithMessage("Player choice id should be chosen from choices list.");
    }
}