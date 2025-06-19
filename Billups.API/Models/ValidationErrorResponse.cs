namespace Billups.Api.Models;

public record ValidationErrorResponse(
    string Message,
    IEnumerable<ValidationError> Errors);

public record ValidationError(
    string Property,
    string Error);