namespace CatalogAPI.Products.CreateProduct;

public record CreateProductRequest(string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);
public record CreateProductResponse(Guid Id);
public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (CreateProductRequest request, ISender sender) =>
        {
            var createProductCommand = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(createProductCommand);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/api/products/{response.Id}", response);
        }).WithName("CreateProduct")
        .Produces(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Creates Product")
        .WithSummary("Creates Product");
    }
}