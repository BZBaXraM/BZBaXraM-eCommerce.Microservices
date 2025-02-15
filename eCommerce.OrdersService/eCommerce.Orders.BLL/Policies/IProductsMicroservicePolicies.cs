namespace eCommerce.Orders.BLL.Policies;

public interface IProductsMicroservicePolicies
{
    IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy();
    IAsyncPolicy<HttpResponseMessage> GetBulkheadIsolationPolicy();
    IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy();
}