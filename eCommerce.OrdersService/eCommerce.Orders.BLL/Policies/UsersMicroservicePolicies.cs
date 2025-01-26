namespace eCommerce.Orders.BLL.Policies;

public class UsersMicroservicePolicies(ILogger<UsersMicroservicePolicies> logger) : IUsersMicroservicePolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return Policy.HandleResult<HttpResponseMessage>(r =>
                !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (outcome, timespan, attempt, context) =>
                {
                    logger.LogInformation("Retry {Attempt} after {TimespanSeconds} seconds", attempt, timespan.Seconds);
                }
            );
    }
}