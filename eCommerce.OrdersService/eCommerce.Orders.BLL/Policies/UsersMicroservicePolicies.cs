namespace eCommerce.Orders.BLL.Policies;

public class UsersMicroservicePolicies(IPollyPolicies pollyPolicies)
    : IUsersMicroservicePolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy()
    {
        var retryPolicy = pollyPolicies.GetRetryPolicy(5);
        var circuitBreakerPolicy = pollyPolicies.GetCircuitBreakerPolicy(3, TimeSpan.FromMinutes(2));
        var timeoutPolicy = pollyPolicies.GetTimeoutPolicy(TimeSpan.FromSeconds(5));

        return Policy.WrapAsync(retryPolicy, circuitBreakerPolicy, timeoutPolicy);
    }
}