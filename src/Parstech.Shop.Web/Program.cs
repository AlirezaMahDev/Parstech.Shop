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
builder.Services.AddSingleton<WalletTransactionGrpcClient>();
builder.Services.AddSingleton<ShippingGrpcClient>();
builder.Services.AddSingleton<PaymentGrpcClient>();
builder.Services.AddSingleton<UserProfileGrpcClient>();
builder.Services.AddSingleton<UserPreferencesGrpcClient>();
builder.Services.AddSingleton<SectionGrpcClient>();
builder.Services.AddSingleton<UserProductGrpcClient>();
builder.Services.AddSingleton<TorobGrpcClient>();
builder.Services.AddSingleton<ProductDetailGrpcClient>();
builder.Services.AddSingleton<ProductGalleryGrpcClient>();
builder.Services.AddSingleton<UserStoreGrpcClient>();
builder.Services.AddSingleton<CategoryGrpcClient>();
builder.Services.AddSingleton<BrandGrpcClient>();
builder.Services.AddSingleton<SiteSettingGrpcClient>();
builder.Services.AddSingleton<UserAuthGrpcClient>();

// Add gRPC clients
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
    options.Address = new Uri(builder.Configuration["ApiServiceUrl"]);
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

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.Section.SectionService.SectionServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserProduct.UserProductService.UserProductServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.Torob.TorobService.TorobServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.ProductDetail.ProductDetailService.ProductDetailServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.ProductGallery.ProductGalleryService.ProductGalleryServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserStore.UserStoreService.UserStoreServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.Category.CategoryService.CategoryServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.Brand.BrandService.BrandServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.Product.ProductService.ProductServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserPreferencesService.UserPreferencesService.UserPreferencesServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.SiteSetting.SiteSettingService.SiteSettingServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["ApiServiceUrl"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserAuth.UserAuthService.UserAuthServiceClient>(options =>
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
