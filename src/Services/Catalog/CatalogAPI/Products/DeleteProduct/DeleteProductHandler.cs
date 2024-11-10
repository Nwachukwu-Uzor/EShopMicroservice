namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductCommandResult(bool IsSuccess);

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductCommandResult>;

public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : IRequestHandler<DeleteProductCommand, DeleteProductCommandResult>
{
    public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Calling DeleteProductCommandHandler.Handle method with {@Command}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
        {
            logger.LogError("Calling DeleteProductCommandHandler.Handle method with {@Command} failed", command);
            throw new ProductNotFoundException(command.Id);
        }
        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductCommandResult(true);
    }
}