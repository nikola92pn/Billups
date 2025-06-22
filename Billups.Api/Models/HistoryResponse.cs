namespace Billups.Api.Models;

public record HistoryResponse(ChoiceResponse PlayerChoice, ChoiceResponse CpuChoice, DateTime CreatedAt);