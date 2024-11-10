namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommandResult(bool IsSuccess);

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price) : ICommand<UpdateProductCommandResult>;

public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Category = command.Category;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductCommandResult(true);
    }
}