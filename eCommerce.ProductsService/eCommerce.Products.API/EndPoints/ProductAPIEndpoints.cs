namespace eCommerce.Products.API.EndPoints;

public class ProductAPIEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async (IProductsService service) =>
        {
            var products = await service.GetProductsAsync();

            return products;
        });

        app.MapGet("/api/products/search/product-id/{id:guid}", async (IProductsService service, Guid id) =>
        {
            var product = await service.GetProductByCondition(p => p.ProductId == id);

            return product;
        });

        app.MapGet("/api/products/search/{searchString}",
            async (IProductsService productsService, string searchString) =>
            {
                var products = await productsService.GetProductByCondition(p =>
                    p.Name.Contains(searchString) || p.Category.Contains(searchString));

                return products;
            });

        app.MapPost("/api/products", async (IProductsService service, ProductAddRequest request) =>
        {
            var product = await service.AddProductAsync(request);

            return product;
        });

        app.MapPut("/api/products", async (IProductsService service, ProductUpdateRequest request) =>
        {
            var product = await service.UpdateProductAsync(request);

            return product;
        });

        app.MapDelete("/api/products/{id:guid}", async (IProductsService service, Guid id) =>
        {
            var result = await service.DeleteProductAsync(id);

            return result;
        });
    }
}