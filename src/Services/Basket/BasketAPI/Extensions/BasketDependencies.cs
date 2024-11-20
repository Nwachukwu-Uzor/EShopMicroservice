namespace BasketAPI.Extensions;

public static class BasketDependencies
{
    public static IServiceCollection AddBasketServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });
        services.Decorate<IBasketRepository, CachedBasketRepository>();
        return services;
    }
}