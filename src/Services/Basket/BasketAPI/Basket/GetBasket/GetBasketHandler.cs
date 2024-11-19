namespace BasketAPI.Basket.GetBasket;

public record GetBasketQueryResult(ShoppingCart Cart);

public record GetBasketQuery(string Username) : IQuery<GetBasketQueryResult>;

public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketQueryResult>
{
    public async Task<GetBasketQueryResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(query.Username, cancellationToken);
        return new GetBasketQueryResult(basket);
    }
}