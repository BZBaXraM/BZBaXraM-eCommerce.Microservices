namespace eCommerce.Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(
    IProductsService service,
    ProductAddRequestValidator addRequestValidator,
    ProductUpdateRequestValidator updateRequestValidator)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProductResponse>> GetProducts()
    {
        var products = await service.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("search/product-id/{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await service.GetProductByCondition(p => p.ProductId == id);
        return Ok(product);
    }

    [HttpGet("search/{searchString}")]
    public async Task<IActionResult> SearchProducts(string searchString)
    {
        var products = await service.GetProductByCondition(p =>
            p.Name.Contains(searchString) || p.Category.Contains(searchString));
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductAddRequest request)
    {
        var requestValidationResult = await addRequestValidator.ValidateAsync(request);

        if (!requestValidationResult.IsValid)
        {
            return BadRequest(requestValidationResult.Errors);
        }

        var product = await service.AddProductAsync(request);
        return Ok(product);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductUpdateRequest request)
    {
        var requestValidationResult = await updateRequestValidator.ValidateAsync(request);

        if (!requestValidationResult.IsValid)
        {
            return BadRequest(requestValidationResult.Errors);
        }

        var product = await service.UpdateProductAsync(request);
        return Ok(product);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await service.DeleteProductAsync(id);
        return Ok(result);
    }
}