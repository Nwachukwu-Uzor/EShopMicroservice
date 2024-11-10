using JasperFx.Core;

namespace CatalogAPI.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(
    IDocumentSession session,
    ILogger<CreateProductCommandHandler> logger) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Calling CreateProductCommandHandler.Handle method with {@command}", command);
        // Map command to product entity
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
            Category = command.Category,
        };
        // save product to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        // return result
        return new CreateProductResult(product.Id);
    }
}