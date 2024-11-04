var builder = WebApplication.CreateBuilder(args);
// Add services to the container
var app = builder.Build();
// Configure the request pipeline

app.Run();