namespace Billups.Api.Models;

public record ErrorResponse(string Message, string? Details = null);