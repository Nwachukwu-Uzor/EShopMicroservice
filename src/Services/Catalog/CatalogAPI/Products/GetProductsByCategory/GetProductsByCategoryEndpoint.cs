using CatalogAPI.Products.GetProducts;

namespace CatalogAPI.Products.GetProductsByCategory;

public record GetProductByCategoryResponse(IReadOnlyList<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/category/{category}", async (ISender sender, string category) => 
            {
            var result = await sender.Send(new GetProductsByCategoryQuery(category));
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        }).WithName("GetProductsByCategory")
        .WithDescription("Returns products by category from catalog")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);;
    }
}