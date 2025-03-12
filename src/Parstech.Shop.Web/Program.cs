using Parstech.Shop.Web;
using Parstech.Shop.Web.Components;
using Parstech.Shop.Web.Services.GrpcClients;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add ApiServiceUrl configuration
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
{
    {"ApiServiceUrl", "https://localhost:7156"}
});

// Register gRPC clients
builder.Services.AddSingleton<ProductGrpcClient>();
builder.Services.AddSingleton<OrderGrpcClient>();
builder.Services.AddSingleton<UserGrpcClient>();
builder.Services.AddSingleton<UserShippingGrpcClient>();
builder.Services.AddSingleton<OrderShippingGrpcClient>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader().WithOrigins("https://localhost:7040");
    });
});

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        client.BaseAddress = new("https+http://apiservice");
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseOutputCache();

app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapRazorPages();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
