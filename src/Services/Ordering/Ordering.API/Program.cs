using Ordering.API;
using Ordering.Application;
using Ordering.Application.Extensions;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApiServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration);
var app = builder.Build();
// Configure request pipeline
app.UseApiServices();
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}
app.Run();