using Billups.Api.Models;
using Billups.Application.Interfaces;
using FluentValidation;

namespace Billups.Api.Validation;

public class PlayRequestValidator : AbstractValidator<PlayRequest>
{
    public PlayRequestValidator(IChoiceProvider  choiceProvider)
    {
        var validChoiceIds = choiceProvider.GetAll().Select(c => c.Id).ToList(); // if too expensive, the list could be cached
        RuleFor(x => x.Player)
            .NotNull().WithMessage("Player choice id is required.")
            .Must(choiceId => validChoiceIds.Contains(choiceId)).WithMessage("Player choice id should be chosen from the choices list.");
    }
}