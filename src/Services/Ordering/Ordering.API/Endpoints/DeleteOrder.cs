using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;
// Accepts a ID to delete order for
// Retrieves the order from the database and deletes it
// Return the response to the client

// public record DeleteOrderRequest(Guid OrderId);

public record DeleteOrderResponse(bool IsSuccess);
public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/orders/{id:Guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(id));
            var response = result.Adapt<DeleteOrderResponse>();
            return Results.Ok(response);
        }).WithName("DeleteOrder")
        .WithDescription("Deletes an order")
        .WithSummary("Deletes an order")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}