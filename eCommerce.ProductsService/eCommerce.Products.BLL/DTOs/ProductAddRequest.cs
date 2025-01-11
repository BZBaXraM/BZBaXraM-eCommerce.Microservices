namespace eCommerce.Products.BLL.DTOs;

public class ProductAddRequest
{
    public string ProductName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public double? UnitPrice { get; set; }
    public int? QuantityInStock { get; set; }
}