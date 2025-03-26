using Parstech.Shop.Context;
using Parstech.Shop.Context.Application;
using Parstech.Shop.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddContext();

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});
builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseRouting();
app.UseGrpcWeb(new() { DefaultEnabled = true });

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();