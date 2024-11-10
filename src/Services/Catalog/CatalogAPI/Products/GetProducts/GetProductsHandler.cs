namespace CatalogAPI.Products.GetProducts;

public record GetProductsResponse(IReadOnlyList<Product> Products);
public record GetProductsQuery() : IQuery<GetProductsResponse>;

internal class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<GetProductsResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle method called with {@query}", query);
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResponse(products);
    }
}