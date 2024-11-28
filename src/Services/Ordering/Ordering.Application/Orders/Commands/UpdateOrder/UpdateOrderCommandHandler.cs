using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdateOrderCommand, UpdateOrderCommandResult>
{
    public async Task<UpdateOrderCommandResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        // Check if the order exists
        var orderId = OrderId.Of(command.Order.Id);
        var existingOrder = await context.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
        if (existingOrder is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }
        // Update Order entity from Database
        UpdateOrderWithNewValues(existingOrder, command.Order);
        // Save the update to the Database
        context.Orders.Update(existingOrder);
        await context.SaveChangesAsync(cancellationToken);
        // Return a result
        return new UpdateOrderCommandResult(true);
    }

    private void UpdateOrderWithNewValues(Order existingOrder, OrderDto orderDto)
    {
        var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
        var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        var payment  = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration,
            orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);
        
        existingOrder.Update(OrderName.Of(orderDto.OrderName), shippingAddress, billingAddress, payment, orderDto.Status);
    }
}