namespace BasketAPI.Basket.GetBasket;

public record GetBasketQueryResult(ShoppingCart Cart);

public record GetBasketQuery(string Username) : IQuery<GetBasketQueryResult>;

public class GetBasketQueryHandler() : IQueryHandler<GetBasketQuery, GetBasketQueryResult>
{
    public async Task<GetBasketQueryResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        // var shoppingCart = await session.Query<ShoppingCart>()
        //     .FirstOrDefaultAsync(cart => cart.Username.Equals(query.Username, StringComparison.OrdinalIgnoreCase), cancellationToken);
        //
        // if (shoppingCart is null)
        // {
        //     throw new NotFoundException(query.Username);
        // }
        //
        // return new GetBasketQueryResult(shoppingCart);
        return await Task.FromResult(new GetBasketQueryResult(new ShoppingCart("user")));
    }
}