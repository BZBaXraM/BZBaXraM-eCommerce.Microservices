namespace eCommerce.Orders.BLL.Policies;

public class PollyPolicies(ILogger<UsersMicroservicePolicies> logger) : IPollyPolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount)
    {
        return Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount,
                retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (_, timespan, retryAttempt, _) =>
                {
                    logger.LogInformation("Retry {RetryAttempt} after {TimespanTotalSeconds} seconds", retryAttempt,
                        timespan.TotalSeconds);
                });
    }


    public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(int handledEventsAllowedBeforeBreaking,
        TimeSpan durationOfBreak)
    {
        return Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking,
                durationOfBreak,
                (_, timespan) =>
                {
                    logger.LogInformation(
                        "Circuit breaker opened for {TimespanTotalMinutes} minutes due to consecutive 3 failures. The subsequent requests will be blocked",
                        timespan.TotalMinutes);
                },
                onReset: () =>
                {
                    logger.LogInformation($"Circuit breaker closed. The subsequent requests will be allowed.");
                });
    }


    public IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(TimeSpan timeout)
    {
        return Policy.TimeoutAsync<HttpResponseMessage>(timeout);
    }
}