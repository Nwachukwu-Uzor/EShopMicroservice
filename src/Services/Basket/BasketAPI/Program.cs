using BasketAPI;

var builder = WebApplication.CreateBuilder(args);
// Add Service to DI container

builder.Services.AddBasketServices(builder);

var app = builder.Build();
app.UseBasketServices();
app.Run();
