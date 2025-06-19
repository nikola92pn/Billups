using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace Billups.Infrastructure.ApiClients;

internal static class HttpPolicies
{
    internal static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(ILogger logger) =>
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // wait time 2, 4, 8 seconds
                onRetry: (result, timespan, retryAttempt, _) =>
                {
                    logger.LogWarning(
                        "Retry {RetryAttempt} after {Delay} seconds due to {Reason}.",
                        retryAttempt,
                        timespan.TotalSeconds,
                        result.Exception != null
                            ? result.Exception.Message
                            : $"HTTP {(int)result.Result.StatusCode} {result.Result.StatusCode}"
                    );
                }
            ); 

    internal static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
}