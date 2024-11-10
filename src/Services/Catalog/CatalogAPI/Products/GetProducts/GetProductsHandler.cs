namespace CatalogAPI.Products.GetProducts;

public record GetProductsResponse(IReadOnlyList<Product> Products);
public record GetProductsQuery() : IQuery<GetProductsResponse>;

internal class GetProductQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<GetProductsResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResponse(products);
    }
}