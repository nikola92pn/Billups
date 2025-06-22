using Billups.Api.Models;
using FluentValidation.Results;

namespace Billups.Api.Mappers;

public static class ValidationErrorMapper
{
    public static ValidationErrorResponse ToResponse(this ValidationResult validationResult)
    {
        var errors = validationResult.Errors.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage));
        return new ValidationErrorResponse("Validation failed.", errors);
    }
}