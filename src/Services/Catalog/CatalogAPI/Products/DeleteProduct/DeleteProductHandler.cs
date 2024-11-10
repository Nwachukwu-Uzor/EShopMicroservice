namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductCommandResult(bool IsSuccess);

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductCommandResult>;

public class DeleteProductCommandHandler(IDocumentSession session) : IRequestHandler<DeleteProductCommand, DeleteProductCommandResult>
{
    public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductCommandResult(true);
    }
}