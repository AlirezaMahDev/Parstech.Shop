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

// Register singleton gRPC clients
builder.Services.AddSingleton<ProductGrpcClient>();
builder.Services.AddSingleton<OrderGrpcClient>();
builder.Services.AddSingleton<UserGrpcClient>();
builder.Services.AddSingleton<UserShippingGrpcClient>();
builder.Services.AddSingleton<OrderShippingGrpcClient>();
builder.Services.AddSingleton<OrderCheckoutGrpcClient>();
builder.Services.AddSingleton<CouponGrpcClient>();
builder.Services.AddSingleton<WalletGrpcClient>();
builder.Services.AddSingleton<ShippingGrpcClient>();
builder.Services.AddSingleton<PaymentGrpcClient>();
builder.Services.AddSingleton<UserProfileGrpcClient>();
builder.Services.AddSingleton<UserPreferencesGrpcClient>();

// Add gRPC channel
builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.OrderCheckout.OrderCheckoutService.OrderCheckoutServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.CouponService.CouponService.CouponServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.WalletService.WalletService.WalletServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.ShippingService.ShippingService.ShippingServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.PaymentService.PaymentService.PaymentServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserProfileService.UserProfileService.UserProfileServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<SectionGrpcClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<UserProductGrpcClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<CategoryGrpcClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<BrandGrpcClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<UserStoreGrpcClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<ProductDetailGrpcClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<ProductGalleryGrpcClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<TorobGrpcClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserPreferencesService.UserPreferencesService.UserPreferencesServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

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
