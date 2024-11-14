namespace BasketAPI.Basket.GetBasket;

// public record GetBasketRequest(string Username);
public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/basket/{username}", async (ISender sender, string username) =>
        {
            var query = new GetBasketQuery(username);
            var result = await sender.Send(query);
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        }).WithName("GetBasket")
        .WithDisplayName("Get Basket")
        .WithSummary("Get Basket")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}