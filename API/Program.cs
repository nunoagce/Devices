using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services
    .AddControllers();

builder.Services
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    .AddOpenApi()
    .AddApplication()
    .AddInfrastructure(configuration);

var app = builder.Build();

await app.Services.UseMigrationsAsync(app.Environment.IsDevelopment());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
