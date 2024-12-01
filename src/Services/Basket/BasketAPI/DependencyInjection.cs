using BuildingBlocks.Messaging.MassTransit;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BasketAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddBasketServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;
        services.AddCarter();
        // Add MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });
        // Data Related Services
        builder.Services.AddMarten(opts =>
        {
            opts.Connection(builder.Configuration.GetConnectionString("Database")!);
            opts.Schema.For<ShoppingCart>().Identity(x => x.Username);
        }).UseLightweightSessions();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });
        services.Decorate<IBasketRepository, CachedBasketRepository>();
        // Add Exception Handler
        services.AddExceptionHandler<CustomExceptionHandler>();
        // Add Grpc services
        builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
        {
            options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
    
            return handler;
        });
        // Health Checks
        services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
            .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
        // Message Broker
        services.AddMessageBroker(builder.Configuration);
        return services;
    }

    public static WebApplication UseBasketServices(this WebApplication app)
    {
        // Use Custom Exception Handler
        app.UseExceptionHandler(opt => { });
        // Use Health check with detailed UI Response Writer
        app.UseHealthChecks("/api/health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        // Configure HTTP Request Pipeline
        app.MapCarter();
        return app;
    }
}