using Parstech.Shop.Web;
using Parstech.Shop.Web.Components;
using Parstech.Shop.Web.Services.GrpcClients;
using System;
using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;

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
builder.Services.AddSingleton<ProductAdminGrpcClient>();
builder.Services.AddSingleton<BrandAdminGrpcClient>();
builder.Services.AddSingleton<CategoryAdminGrpcClient>();
builder.Services.AddSingleton<CouponAdminGrpcClient>();
builder.Services.AddSingleton<StoreAdminGrpcClient>();
builder.Services.AddSingleton<SectionAdminGrpcClient>();
builder.Services.AddSingleton<ProductDetailAdminGrpcClient>();
builder.Services.AddSingleton<RepresentationAdminGrpcClient>();
builder.Services.AddSingleton<PropertyAdminGrpcClient>();
builder.Services.AddSingleton<ConfigAdminGrpcClient>();
builder.Services.AddSingleton<UserAdminGrpcClient>();
builder.Services.AddSingleton<SettingsAdminGrpcClient>();
builder.Services.AddSingleton<FinancialAdminGrpcClient>();

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

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.FormCredit.FormCreditService.FormCreditServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.Rahkaran.RahkaranService.RahkaranServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.BrandAdmin.BrandAdminService.BrandAdminServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.CategoryAdmin.CategoryAdminService.CategoryAdminServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.CouponAdmin.CouponAdminService.CouponAdminServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.ProductAdmin.ProductAdminService.ProductAdminServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.StoreAdmin.StoreAdminService.StoreAdminServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.SectionAdmin.SectionAdminService.SectionAdminServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.ProductDetailAdmin.ProductDetailAdminService.ProductDetailAdminServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.RepresentationAdmin.RepresentationAdminService.RepresentationAdminServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.PropertyAdmin.PropertyAdminService.PropertyAdminServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Url"]);
});

builder.Services.AddGrpcClient<UserService.UserServiceClient>(
    options => options.Address = new Uri(builder.Configuration["GrpcSettings:ApiUrl"])
);

builder.Services.AddGrpcClient<Parstech.Shop.Shared.Protos.FinancialAdmin.FinancialAdminService.FinancialAdminServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["ApiServiceUrl"]);
});

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
