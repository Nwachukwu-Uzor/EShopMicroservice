using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;
// Receives an UpdateOrderRequest
// Maps the Request to a Update order command
// Dispatches the command with MediatR
// Returns the result to the calling client

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/orders", async (UpdateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateOrderResponse>();
            return Results.Ok(response);
        }).WithName("UpdateOrder")
        .WithDisplayName("Update an order")
        .WithSummary("Updates an order")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}