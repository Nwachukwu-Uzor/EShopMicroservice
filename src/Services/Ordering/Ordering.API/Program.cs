using Ordering.API.Extensions;
using Ordering.Application.Extensions;
using Ordering.Infrastructure.Data.Extensions;
using Ordering.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApiServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();
var app = builder.Build();
// Configure request pipeline
app.UseApiServices();
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}
app.Run();