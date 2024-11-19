using BasketAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add Service to DI container
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.Username);
}).UseLightweightSessions();
builder.Services.AddBasketServices();
var app = builder.Build();
app.UseExceptionHandler(opt => { });
// Configure HTTP Request Pipeline
app.MapCarter();
app.Run();
