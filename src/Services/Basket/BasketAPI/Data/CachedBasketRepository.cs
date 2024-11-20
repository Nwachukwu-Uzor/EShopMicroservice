using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace BasketAPI.Data;

public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
{
    private const string CachePrefix = "BASKET_MS_SHOPPING_CART_";
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(CachePrefix + username, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
            Console.WriteLine("Retrieved from cache");
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }
        var basketFromDb = await basketRepository.GetBasket(username, cancellationToken);
        await cache.SetStringAsync(CachePrefix + username, JsonSerializer.Serialize(basketFromDb), cancellationToken);
        return basketFromDb;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        var basketResult = await basketRepository.StoreBasket(basket, cancellationToken);
        await cache.SetStringAsync(CachePrefix + basketResult.Username, JsonSerializer.Serialize(basketResult), cancellationToken);
        return basketResult;
    }

    public async Task DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasket(userName, cancellationToken);
        await cache.RemoveAsync(CachePrefix + userName, cancellationToken);
    }
}