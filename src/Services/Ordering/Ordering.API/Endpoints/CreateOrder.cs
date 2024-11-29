using Microsoft.AspNetCore.Http.HttpResults;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);
public record CreateOrderResponse(Guid Id);

// Accepts a CreateOrderRequest
// Converts the request to a CreateOrderCommand
// Create the order from the command and stores it in the database
// Return the order Id to the client
public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateOrderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateOrderResponse>();
            return Results.Created($"api/orders/{response.Id}", response);
        }).WithName("CreateOrder")
        .WithDescription("Creates a new order")
        .WithSummary("Creates a new order")
        .Produces<CreateOrderRequest>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}