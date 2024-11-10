namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdQueryResult(Product Product);
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResult>;

public class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        return new GetProductByIdQueryResult(product);
    }
}