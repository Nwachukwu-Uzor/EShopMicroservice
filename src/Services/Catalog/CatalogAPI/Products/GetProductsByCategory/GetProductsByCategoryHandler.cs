namespace CatalogAPI.Products.GetProductsByCategory;

public record GetProductsByCategoryQueryResult(IReadOnlyList<Product> Products);

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryQueryResult>;

public class GetProductsByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryQueryResult>
{
    public async Task<GetProductsByCategoryQueryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Calling GetProductsByCategoryQueryHandler.Handle method with {@query}", query);
        var products = await session.Query<Product>().Where(
            product => product.Category.Any(cat => cat.Equals(query.Category, StringComparison.CurrentCultureIgnoreCase))
        ).ToListAsync(cancellationToken);
        return new GetProductsByCategoryQueryResult(products);
    }
}