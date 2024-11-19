namespace BasketAPI.Extensions;

public static class BasketDependencies
{
    public static IServiceCollection AddBasketServices(this IServiceCollection services)
    {
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddExceptionHandler<CustomExceptionHandler>();
        return services;
    }
}