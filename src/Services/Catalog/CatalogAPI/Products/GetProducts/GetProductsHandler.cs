namespace CatalogAPI.Products.GetProducts;

public record GetProductsQueryResult(IEnumerable<Product> Products);
public record GetProductsQuery(int? Page = 1, int? Size=10) : IQuery<GetProductsQueryResult>;

internal class GetProductQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
{
    public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToPagedListAsync(query.Page ?? 1, query.Size ?? 10, cancellationToken);
        return new GetProductsQueryResult(products);
    }
}