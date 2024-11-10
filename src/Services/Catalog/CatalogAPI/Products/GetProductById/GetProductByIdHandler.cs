namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdQueryResult(Product Product);
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResult>;

public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductByIdQueryHandler.Handle method with {@query}", query);
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        if (product is null)
        {
            logger.LogError("No product with ID {@id} was found", query.Id);
            throw new ProductNotFoundException(query.Id);
        }
        return new GetProductByIdQueryResult(product);
    }
}