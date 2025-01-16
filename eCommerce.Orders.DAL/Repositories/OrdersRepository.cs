using eCommerce.Orders.DAL.Entities;
using MongoDB.Driver;

namespace eCommerce.Orders.DAL.Repositories;

public class OrdersRepository(IMongoDatabase mongoDatabase) : IOrdersRepository
{
    private readonly IMongoCollection<Order> _orders = mongoDatabase.GetCollection<Order>(CollectionName);
    private const string CollectionName = "orders";


    public async Task<Order?> AddOrder(Order order)
    {
        order.OrderId = Guid.NewGuid();
        order._id = order.OrderId;

        foreach (var item in order.OrderItems)
        {
            item._id = Guid.NewGuid();
        }

        await _orders.InsertOneAsync(order);
        return order;
    }


    public async Task<bool> DeleteOrder(Guid orderId)
    {
        var filter = Builders<Order>.Filter.Eq(temp => temp.OrderId, orderId);

        var existingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();

        if (existingOrder is null)
        {
            return false;
        }

        var deleteResult = await _orders.DeleteOneAsync(filter);

        return deleteResult.DeletedCount > 0;
    }


    public async Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        return (await _orders.FindAsync(filter)).FirstOrDefault();
    }


    public async Task<IReadOnlyList<Order>> GetOrders()
    {
        return (await _orders.FindAsync(Builders<Order>.Filter.Empty)).ToList();
    }


    public async Task<IReadOnlyList<Order?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        return (await _orders.FindAsync(filter)).ToList();
    }


    public async Task<Order?> UpdateOrder(Order order)
    {
        var filter = Builders<Order>.Filter.Eq(temp => temp.OrderId, order.OrderId);

        var existingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();

        if (existingOrder is null)
        {
            return null;
        }
        
        order._id = existingOrder._id;

        var replaceOneResult = await _orders.ReplaceOneAsync(filter, order);

        return replaceOneResult.IsAcknowledged ? order : null;
    }
}