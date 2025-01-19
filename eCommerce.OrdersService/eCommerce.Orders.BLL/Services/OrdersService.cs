namespace eCommerce.Orders.BLL.Services;

public class OrdersService(
    IOrdersRepository ordersRepository,
    IMapper mapper,
    IValidator<OrderAddRequest?> orderAddRequestValidator,
    IValidator<OrderItemAddRequest> orderItemAddRequestValidator,
    IValidator<OrderUpdateRequest> orderUpdateRequestValidator,
    IValidator<OrderItemUpdateRequest> orderItemUpdateRequestValidator,
    UsersMicroserviceClient usersMicroserviceClient,
    ProductsMicroserviceClient productsMicroserviceClient)
    : IOrdersService
{
    public async Task<OrderResponse?> AddOrderAsync(OrderAddRequest? orderAddRequest)
    {
        if (orderAddRequest == null)
        {
            throw new ArgumentNullException(nameof(orderAddRequest));
        }


        var orderAddRequestValidationResult = await orderAddRequestValidator.ValidateAsync(orderAddRequest);

        if (!orderAddRequestValidationResult.IsValid)
        {
            var errors = string.Join(", ", orderAddRequestValidationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        foreach (var orderItemAddRequest in orderAddRequest.OrderItems)
        {
            var orderItemAddRequestValidationResult =
                await orderItemAddRequestValidator.ValidateAsync(orderItemAddRequest);

            if (!orderItemAddRequestValidationResult.IsValid)
            {
                var errors = string.Join(", ",
                    orderItemAddRequestValidationResult.Errors.Select(temp => temp.ErrorMessage));
                throw new ArgumentException(errors);
            }

            var product = await productsMicroserviceClient.GetProductByIdAsync(orderItemAddRequest.ProductId);

            if (product == null)
            {
                throw new ArgumentException("Invalid product ID");
            }
        }

        var orderInput = mapper.Map<Order>(orderAddRequest);

        foreach (var orderItem in orderInput.OrderItems)
        {
            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
        }

        orderInput.TotalBill = orderInput.OrderItems.Sum(temp => temp.TotalPrice);

        var addedOrder = await ordersRepository.AddOrder(orderInput);

        if (addedOrder == null)
        {
            return null;
        }

        var user = await usersMicroserviceClient.GetUserByIdAsync(addedOrder.UserId);

        if (user == null)
        {
            throw new ArgumentException("Invalid user ID");
        }

        var addedOrderResponse = mapper.Map<OrderResponse>(addedOrder);

        return addedOrderResponse;
    }


    public async Task<OrderResponse?> UpdateOrderAsync(OrderUpdateRequest orderUpdateRequest)
    {
        if (orderUpdateRequest == null)
        {
            throw new ArgumentNullException(nameof(orderUpdateRequest));
        }


        var orderUpdateRequestValidationResult = await orderUpdateRequestValidator.ValidateAsync(orderUpdateRequest);

        if (!orderUpdateRequestValidationResult.IsValid)
        {
            var errors = string.Join(", ",
                orderUpdateRequestValidationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        foreach (var orderItemUpdateRequest in orderUpdateRequest.OrderItems)
        {
            var orderItemUpdateRequestValidationResult =
                await orderItemUpdateRequestValidator.ValidateAsync(orderItemUpdateRequest);

            if (!orderItemUpdateRequestValidationResult.IsValid)
            {
                var errors = string.Join(", ",
                    orderItemUpdateRequestValidationResult.Errors.Select(temp => temp.ErrorMessage));

                throw new ArgumentException(errors);
            }

            var product = await productsMicroserviceClient.GetProductByIdAsync(orderItemUpdateRequest.ProductId);

            if (product == null)
            {
                throw new ArgumentException("Invalid product ID");
            }
        }


        var orderInput = mapper.Map<Order>(orderUpdateRequest);

        foreach (var orderItem in orderInput.OrderItems)
        {
            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
        }

        orderInput.TotalBill = orderInput.OrderItems.Sum(temp => temp.TotalPrice);


        var updatedOrder = await ordersRepository.UpdateOrder(orderInput);

        if (updatedOrder == null)
        {
            return null;
        }

        var user = await usersMicroserviceClient.GetUserByIdAsync(updatedOrder.UserId);

        if (user == null)
        {
            throw new ArgumentException("Invalid user id");
        }

        var updatedOrderResponse = mapper.Map<OrderResponse>(updatedOrder);

        return updatedOrderResponse;
    }


    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        var filter = Builders<Order>.Filter.Eq(temp => temp.OrderId, id);
        var existingOrder = await ordersRepository.GetOrderByCondition(filter);

        if (existingOrder == null)
        {
            return false;
        }

        var isDeleted = await ordersRepository.DeleteOrder(id);
        return isDeleted;
    }


    public async Task<OrderResponse?> GetOrderByConditionAsync(FilterDefinition<Order> filter)
    {
        var order = await ordersRepository.GetOrderByCondition(filter);
        if (order == null)
            return null;

        OrderResponse orderResponse = mapper.Map<OrderResponse>(order);
        return orderResponse;
    }


    public async Task<IReadOnlyList<OrderResponse?>> GetOrdersByConditionAsync(FilterDefinition<Order> filter)
    {
        var orders = await ordersRepository.GetOrdersByCondition(filter);

        var orderResponses = mapper.Map<IEnumerable<OrderResponse>>(orders);

        return orderResponses.ToList();
    }


    public async Task<IReadOnlyList<OrderResponse?>> GetOrdersAsync()
    {
        var orders = await ordersRepository.GetOrders();

        var orderResponses = mapper.Map<IEnumerable<OrderResponse>>(orders);

        IEnumerable<OrderResponse> responses = orderResponses.ToList();
        foreach (var response in responses)
        {
            if (response is null) return null!;

            foreach (var orderItem in response.OrderItems)
            {
                var product = await productsMicroserviceClient.GetProductByIdAsync(orderItem.ProductId);

                if (product == null) return null!;

                mapper.Map(product, orderItem);
            }
        }

        return responses.ToList();
    }
}