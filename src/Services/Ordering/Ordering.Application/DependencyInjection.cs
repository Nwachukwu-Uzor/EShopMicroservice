using System.Reflection;
using BuildingBlocks.Behaviours;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Ordering.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddMediatR(config =>
    {
      config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
      config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
      config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
    });
    // Add Feature Flag
    services.AddFeatureManagement();
    // Message Broker
    services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
    return services;
  }  
}