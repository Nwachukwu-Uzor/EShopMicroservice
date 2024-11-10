namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductRequest(
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price);

public record UpdateProductResponse(bool IsSuccess);


public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/products/{id:guid}", async (ISender sender, Guid id, UpdateProductRequest request) =>
        {
            var product = request.Adapt<UpdateProductCommand>() with { Id = id };
            var result = await sender.Send(product);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        }).WithName("UpdateProduct")
        .WithDescription("Updates a product in the catalog")
        .WithSummary("Update a product in the catalog")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);;;
    }
}