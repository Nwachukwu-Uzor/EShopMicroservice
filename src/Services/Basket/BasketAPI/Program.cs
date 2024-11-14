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
var app = builder.Build();
// Configure HTTP Request Pipeline
app.MapCarter();
app.Run();
