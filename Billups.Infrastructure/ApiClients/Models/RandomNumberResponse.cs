using System.Text.Json.Serialization;

namespace Billups.Infrastructure.ApiClients.Models;

internal record RandomNumberResponse
{
    [JsonPropertyName("random_number")] 
    public int RandomNumber { get; init; }
}