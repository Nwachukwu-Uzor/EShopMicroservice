namespace CatalogAPI.Products.GetProducts;

public record GetProductResponse(IReadOnlyList<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            }).WithName("GetProducts")
            .WithDescription("Returns products from catalog")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}