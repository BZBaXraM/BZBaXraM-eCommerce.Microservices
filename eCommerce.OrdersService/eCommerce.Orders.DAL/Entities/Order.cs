namespace eCommerce.Orders.DAL.Entities;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid _id { get; set; }

    [BsonRepresentation(BsonType.String)] public Guid OrderId { get; set; }

    [BsonRepresentation(BsonType.String)] public Guid UserId { get; set; }


    [BsonRepresentation(BsonType.String)] public DateTime OrderDate { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal TotalBill { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
}