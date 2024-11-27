using Ordering.API.Extensions;
using Ordering.Application.Extensions;
using Ordering.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApiServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();
var app = builder.Build();

app.Run();