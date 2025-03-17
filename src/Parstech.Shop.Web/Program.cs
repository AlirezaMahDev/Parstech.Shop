using Parstech.Shop.Web;
using Parstech.Shop.Web.Components;
using Parstech.Shop.ServiceDefaults;
using Parstech.Shop.Web.Extensions;
using Parstech.Shop.Web.GrpcClients;
using Parstech.Shop.Web.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add ApiServiceUrl configuration
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
{
    { "ApiServiceUrl", "https://localhost:7156" }
});

// Register all gRPC clients using extension method
builder.Services.AddGrpcClients(builder.Configuration);

// Register scoped clients
builder.Services.AddScoped<UserGrpcClient>();
builder.Services.AddScoped<CouponAdminGrpcClient>();
builder.Services.AddScoped<StoreAdminGrpcClient>();
builder.Services.AddScoped<SectionAdminGrpcClient>();
builder.Services.AddScoped<ProductDetailAdminGrpcClient>();
builder.Services.AddScoped<RepresentationAdminGrpcClient>();
builder.Services.AddScoped<PropertyAdminGrpcClient>();
builder.Services.AddScoped<ConfigAdminGrpcClient>();
builder.Services.AddScoped<UserAdminGrpcClient>();
builder.Services.AddScoped<SettingsAdminGrpcClient>();
builder.Services.AddScoped<FinancialAdminGrpcClient>();
builder.Services.AddScoped<ReportsAdminGrpcClient>();
builder.Services.AddScoped<ProductAdminGrpcClient>();
builder.Services.AddScoped<RoleAdminGrpcClient>();
builder.Services.AddScoped<DashboardAdminGrpcClient>();
builder.Services.AddScoped<SelectionsAdminGrpcClient>();
builder.Services.AddScoped<AuthAdminGrpcClient>();

// Register gRPC client services
builder.Services.AddScoped<IConfigAdminGrpcClient, ConfigAdminGrpcClient>();
builder.Services.AddScoped<IUserAdminGrpcClient, UserAdminGrpcClient>();
builder.Services.AddScoped<ISettingsAdminGrpcClient, SettingsAdminGrpcClient>();
builder.Services.AddScoped<IPropertyAdminGrpcClient, PropertyAdminGrpcClient>();
builder.Services.AddScoped<IRepresentationAdminGrpcClient, RepresentationAdminGrpcClient>();
builder.Services.AddScoped<IFinancialAdminGrpcClient, FinancialAdminGrpcClient>();
builder.Services.AddScoped<IReportsAdminGrpcClient, ReportsAdminGrpcClient>();
builder.Services.AddScoped<IOrderAdminGrpcClient, OrderAdminGrpcClient>();
builder.Services.AddScoped<IProductAdminGrpcClient, ProductAdminGrpcClient>();
builder.Services.AddScoped<IRoleAdminGrpcClient, RoleAdminGrpcClient>();
builder.Services.AddScoped<IStoreAdminGrpcClient, StoreAdminGrpcClient>();
builder.Services.AddScoped<IDashboardAdminGrpcClient, DashboardAdminGrpcClient>();
builder.Services.AddScoped<ISelectionsAdminGrpcClient, SelectionsAdminGrpcClient>();
builder.Services.AddScoped<IAuthAdminGrpcClient, AuthAdminGrpcClient>();
builder.Services.AddScoped<IProductComponentsAdminGrpcClient, ProductComponentsAdminGrpcClient>();
builder.Services.AddScoped<IProductInventoryAdminGrpcClient, ProductInventoryAdminGrpcClient>();

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

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
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