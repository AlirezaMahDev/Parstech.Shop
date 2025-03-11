using Shop.Persistence.Context;
using Shop.Application;
using Shop.Infrastructure;
using Shop.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistenceService(builder.Configuration);
builder.Services.ConfigureInfrustructureService();
builder.Services.ConfigureApplicationService(builder.Configuration);

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod()
            .AllowAnyMethod()
            .WithOrigins("https://localhost:7040");
    });
});

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddSqlServer<DatabaseContext>("database");

var app = builder.Build();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseMigrationsEndPoint();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseRouting();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapStaticAssets();
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}