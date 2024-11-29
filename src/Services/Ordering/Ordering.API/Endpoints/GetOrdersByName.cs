using Ordering.Application.Orders.Queries.GetOrderByName;
using Ordering.Domain.Models;

namespace Ordering.API.Endpoints;
// Receives and order name parameter
// Retrieves all orders with the given order name
// Returns the response to the client

// public record GetOrdersByNameRequest(string OrderName);
public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders/{orderName}", async (string orderName, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByNameQuery(orderName));
            var response = result.Adapt<GetOrdersByNameResponse>();
            return Results.Ok(response);
        }).WithName("GetOrdersByName")
        .WithDescription("Get orders by name")
        .WithSummary("Get orders by name")
        .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}