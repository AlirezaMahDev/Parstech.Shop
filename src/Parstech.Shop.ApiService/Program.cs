using Parstech.Shop.ApiService;
using Parstech.Shop.ApiService.Application;
using Parstech.Shop.ApiService.Services;
using Parstech.Shop.ApiService.Services.GrpcServices;
using Parstech.Shop.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Configure application services (including gRPC)
builder.Services.AddGrpc(options => 
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddGrpcWeb();
builder.AddServiceDefaults();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application services
builder.Services.ConfigureApplicationService(builder.Configuration);

// Register all gRPC services
RegisterGrpcServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

// Map controllers and gRPC services
app.MapControllers();
app.MapGrpcServices();

app.Run();

// Helper method to register all gRPC services
void RegisterGrpcServices(IServiceCollection services)
{
    // Admin services
    services.AddGrpcService<AuthAdminGrpcService>();
    services.AddGrpcService<BrandAdminGrpcService>();
    services.AddGrpcService<CategoryAdminGrpcService>();
    services.AddGrpcService<ConfigAdminGrpcService>();
    services.AddGrpcService<CouponAdminGrpcService>();
    services.AddGrpcService<DashboardAdminGrpcService>();
    services.AddGrpcService<FinancialAdminGrpcService>();
    services.AddGrpcService<OrderAdminGrpcService>();
    services.AddGrpcService<ProductAdminGrpcService>();
    services.AddGrpcService<ProductComponentsAdminGrpcService>();
    services.AddGrpcService<ProductDetailAdminGrpcService>();
    services.AddGrpcService<ProductInventoryAdminGrpcService>();
    services.AddGrpcService<PropertyAdminGrpcService>();
    services.AddGrpcService<RepresentationAdminGrpcService>();
    services.AddGrpcService<ReportsAdminGrpcService>();
    services.AddGrpcService<RoleAdminGrpcService>();
    services.AddGrpcService<SectionAdminGrpcService>();
    services.AddGrpcService<SelectionsAdminGrpcService>();
    services.AddGrpcService<SettingsAdminGrpcService>();
    services.AddGrpcService<StoreAdminGrpcService>();
    services.AddGrpcService<UserAdminGrpcService>();
    
    // Client services
    services.AddGrpcService<BrandGrpcService>();
    services.AddGrpcService<CategoryGrpcService>();
    services.AddGrpcService<CouponGrpcService>();
    services.AddGrpcService<FormCreditGrpcService>();
    services.AddGrpcService<OrderCheckoutService>();
    services.AddGrpcService<OrderGrpcService>();
    services.AddGrpcService<PaymentGrpcService>();
    services.AddGrpcService<ProductDetailGrpcService>();
    services.AddGrpcService<ProductGalleryGrpcService>();
    services.AddGrpcService<ProductGrpcService>();
    services.AddGrpcService<RahkaranGrpcService>();
    services.AddGrpcService<SectionGrpcService>();
    services.AddGrpcService<ShippingGrpcService>();
    services.AddGrpcService<SiteSettingGrpcService>();
    services.AddGrpcService<TorobGrpcService>();
    services.AddGrpcService<TorobService>();
    services.AddGrpcService<UserAuthGrpcService>();
    services.AddGrpcService<UserPreferencesGrpcService>();
    services.AddGrpcService<UserProductGrpcService>();
    services.AddGrpcService<UserProfileGrpcService>();
    services.AddGrpcService<UserStoreGrpcService>();
    services.AddGrpcService<WalletGrpcService>();
    services.AddGrpcService<WalletTransactionGrpcService>();
}

// Extension method to easily register gRPC services
namespace Parstech.Shop.ApiService
{
    public static class GrpcServiceExtensions
    {
        public static IServiceCollection AddGrpcService<T>(this IServiceCollection services) where T : class
        {
            services.AddSingleton<T>();
            return services;
        }
    
        public static IEndpointRouteBuilder MapGrpcServices(this IEndpointRouteBuilder endpoints)
        {
            // Admin services
            endpoints.MapGrpcService<AuthAdminGrpcService>();
            endpoints.MapGrpcService<BrandAdminGrpcService>();
            endpoints.MapGrpcService<CategoryAdminGrpcService>();
            endpoints.MapGrpcService<ConfigAdminGrpcService>();
            endpoints.MapGrpcService<CouponAdminGrpcService>();
            endpoints.MapGrpcService<DashboardAdminGrpcService>();
            endpoints.MapGrpcService<FinancialAdminGrpcService>();
            endpoints.MapGrpcService<OrderAdminGrpcService>();
            endpoints.MapGrpcService<ProductAdminGrpcService>();
            endpoints.MapGrpcService<ProductComponentsAdminGrpcService>();
            endpoints.MapGrpcService<ProductDetailAdminGrpcService>();
            endpoints.MapGrpcService<ProductInventoryAdminGrpcService>();
            endpoints.MapGrpcService<PropertyAdminGrpcService>();
            endpoints.MapGrpcService<RepresentationAdminGrpcService>();
            endpoints.MapGrpcService<ReportsAdminGrpcService>();
            endpoints.MapGrpcService<RoleAdminGrpcService>();
            endpoints.MapGrpcService<SectionAdminGrpcService>();
            endpoints.MapGrpcService<SelectionsAdminGrpcService>();
            endpoints.MapGrpcService<SettingsAdminGrpcService>();
            endpoints.MapGrpcService<StoreAdminGrpcService>();
            endpoints.MapGrpcService<UserAdminGrpcService>();
        
            // Client services
            endpoints.MapGrpcService<BrandGrpcService>();
            endpoints.MapGrpcService<CategoryGrpcService>();
            endpoints.MapGrpcService<CouponGrpcService>();
            endpoints.MapGrpcService<FormCreditGrpcService>();
            endpoints.MapGrpcService<OrderCheckoutService>();
            endpoints.MapGrpcService<OrderGrpcService>();
            endpoints.MapGrpcService<PaymentGrpcService>();
            endpoints.MapGrpcService<ProductDetailGrpcService>();
            endpoints.MapGrpcService<ProductGalleryGrpcService>();
            endpoints.MapGrpcService<ProductGrpcService>();
            endpoints.MapGrpcService<RahkaranGrpcService>();
            endpoints.MapGrpcService<SectionGrpcService>();
            endpoints.MapGrpcService<ShippingGrpcService>();
            endpoints.MapGrpcService<SiteSettingGrpcService>();
            endpoints.MapGrpcService<TorobGrpcService>();
            endpoints.MapGrpcService<TorobService>();
            endpoints.MapGrpcService<UserAuthGrpcService>();
            endpoints.MapGrpcService<UserPreferencesGrpcService>();
            endpoints.MapGrpcService<UserProductGrpcService>();
            endpoints.MapGrpcService<UserProfileGrpcService>();
            endpoints.MapGrpcService<UserStoreGrpcService>();
            endpoints.MapGrpcService<WalletGrpcService>();
            endpoints.MapGrpcService<WalletTransactionGrpcService>();
        
            return endpoints;
        }
    }
}