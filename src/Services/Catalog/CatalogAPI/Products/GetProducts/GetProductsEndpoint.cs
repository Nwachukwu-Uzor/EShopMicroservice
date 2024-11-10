namespace CatalogAPI.Products.GetProducts;

public record GetProductsRequest(int? Page = 1, int? Size=10);
public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", async ([AsParameters]GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsQueryResult>();
                return Results.Ok(response);
            }).WithName("GetProducts")
            .WithDescription("Returns products from catalog")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}