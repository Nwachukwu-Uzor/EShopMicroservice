using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreateEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreateEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    const string ORDER_FULFILL_MENT = "OrderFulfillment";
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {event}", domainEvent.GetType());
        if (await featureManager.IsEnabledAsync(ORDER_FULFILL_MENT))
        {
            var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
    }
}