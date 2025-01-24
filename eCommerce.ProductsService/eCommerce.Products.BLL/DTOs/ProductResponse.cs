namespace eCommerce.Products.BLL.DTOs;

public class ProductResponse // original from Products.Microservice
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal? UnitPrice { get; set; }
    public int? QuantityInStock { get; set; }
}