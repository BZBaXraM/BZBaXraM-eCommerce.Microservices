namespace eCommerce.Orders.BLL.Policies;

public class ProductsMicroservicePolicies(ILogger<ProductsMicroservicePolicies> logger) : IProductsMicroservicePolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
    {
        return Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .FallbackAsync(_ =>
            {
                logger.LogWarning("Fallback triggered: The request failed, returning dummy data");

                var product = new ProductDto(Guid.Empty,
                    "Temporarily Unavailable (fallback)",
                    "Temporarily Unavailable (fallback)",
                    0,
                    0
                );

                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = content
                });
            });
    }

    public IAsyncPolicy<HttpResponseMessage> GetBulkheadIsolationPolicy()
    {
        return Policy.BulkheadAsync<HttpResponseMessage>(2, 40, _ =>
        {
            logger.LogWarning("Bulkhead triggered: The request failed, returning dummy data");

            throw new BulkheadRejectedException("Bulkhead triggered: The request failed, returning dummy data");
        });
    }

    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy()
    {
        return Policy.WrapAsync(GetFallbackPolicy(), GetBulkheadIsolationPolicy());
    }
}