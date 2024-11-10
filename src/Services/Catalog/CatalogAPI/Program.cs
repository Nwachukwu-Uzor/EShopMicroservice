using CatalogAPI.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container
var assembly  = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.DisableNpgsqlLogging = true;
}).UseLightweightSessions();
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();
// Configure the request pipeline
app.MapCarter();
app.UseExceptionHandler(opt => {});
app.Run();