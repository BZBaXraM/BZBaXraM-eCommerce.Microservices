using eCommerce.Orders.BLL.DTOs;
using eCommerce.Orders.BLL.Services;
using eCommerce.Orders.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace eCommerce.Orders.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrdersService service) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyList<OrderResponse?>> GetOrders()
    {
        return await service.GetOrdersAsync();
    }

    [HttpGet("search/orderId/{orderId:guid}")]
    public async Task<OrderResponse?> GetOrderById(Guid orderId)
    {
        var filter = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);

        return await service.GetOrderByConditionAsync(filter);
    }

    [HttpGet("search/productId/{productId:guid}")]
    public async Task<IReadOnlyList<OrderResponse?>> GetOrdersByProductId(Guid productId)
    {
        var filter = Builders<Order>.Filter.ElemMatch(temp => temp.OrderItems,
            Builders<OrderItem>.Filter.Eq(tempProduct => tempProduct.ProductId, productId));

        var orders = await service.GetOrdersByConditionAsync(filter);
        return orders;
    }

    [HttpGet("/search/orderDate/{orderDate:datetime}")]
    public async Task<IEnumerable<OrderResponse?>> GetOrdersByOrderDate(DateTime orderDate)
    {
        var filter = Builders<Order>.Filter.Eq(temp => temp.OrderDate.ToString("yyyy-MM-dd"),
            orderDate.ToString("yyyy-MM-dd"));

        var orders = await service.GetOrdersByConditionAsync(filter);
        return orders;
    }

    [HttpPost]
    public async Task<IActionResult> Post(OrderAddRequest? orderAddRequest)
    {
        if (orderAddRequest == null)
        {
            return BadRequest("Invalid order data");
        }

        OrderResponse? orderResponse = await service.AddOrderAsync(orderAddRequest);

        if (orderResponse == null)
        {
            return Problem("Error in adding order");
        }


        return Created($"api/Orders/search/orderId/{orderResponse.OrderId}", orderResponse);
    }


    [HttpPut("{orderId:guid}")]
    public async Task<IActionResult> Put(Guid orderId, OrderUpdateRequest? orderUpdateRequest)
    {
        if (orderUpdateRequest == null)
        {
            return BadRequest("Invalid order data");
        }

        if (orderId != orderUpdateRequest.OrderId)
        {
            return BadRequest("OrderID in the URL doesn't match with the OrderID in the Request body");
        }

        var orderResponse = await service.UpdateOrderAsync(orderUpdateRequest);

        return orderResponse == null ? Problem("Error in updating order") : Ok(orderResponse);
    }


    [HttpDelete("{orderId:guid}")]
    public async Task<IActionResult> Delete(Guid orderId)
    {
        if (orderId == Guid.Empty)
        {
            return BadRequest("Invalid order ID");
        }

        var isDeleted = await service.DeleteOrderAsync(orderId);

        return !isDeleted ? Problem("Error in deleting order") : Ok(isDeleted);
    }
}