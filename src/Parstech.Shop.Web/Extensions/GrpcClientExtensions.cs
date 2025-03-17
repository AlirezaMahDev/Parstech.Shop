using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Extensions;

public static class GrpcClientExtensions
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        // Register singleton gRPC clients
        services.AddSingleton<ProductGrpcClient>();
        services.AddSingleton<OrderGrpcClient>();
        services.AddSingleton<UserGrpcClient>();
        services.AddSingleton<UserShippingGrpcClient>();
        services.AddSingleton<OrderShippingGrpcClient>();
        services.AddSingleton<OrderCheckoutGrpcClient>();
        services.AddSingleton<CouponGrpcClient>();
        services.AddSingleton<WalletGrpcClient>();
        services.AddSingleton<WalletTransactionGrpcClient>();
        services.AddSingleton<ShippingGrpcClient>();
        services.AddSingleton<PaymentGrpcClient>();
        services.AddSingleton<UserProfileGrpcClient>();
        services.AddSingleton<UserPreferencesGrpcClient>();
        services.AddSingleton<SectionGrpcClient>();
        services.AddSingleton<UserProductGrpcClient>();
        services.AddSingleton<TorobGrpcClient>();
        services.AddSingleton<ProductDetailGrpcClient>();
        services.AddSingleton<ProductGalleryGrpcClient>();
        services.AddSingleton<UserStoreGrpcClient>();
        services.AddSingleton<CategoryGrpcClient>();
        services.AddSingleton<BrandGrpcClient>();
        services.AddSingleton<ProductAdminGrpcClient>();
        services.AddSingleton<BrandAdminGrpcClient>();
        services.AddSingleton<CategoryAdminGrpcClient>();
        services.AddSingleton<CouponAdminGrpcClient>();
        services.AddSingleton<StoreAdminGrpcClient>();
        services.AddSingleton<SectionAdminGrpcClient>();
        services.AddSingleton<ProductDetailAdminGrpcClient>();
        services.AddSingleton<RepresentationAdminGrpcClient>();
        services.AddSingleton<PropertyAdminGrpcClient>();
        services.AddSingleton<ConfigAdminGrpcClient>();
        services.AddSingleton<UserAdminGrpcClient>();
        services.AddSingleton<SettingsAdminGrpcClient>();
        services.AddSingleton<FinancialAdminGrpcClient>();
        services.AddSingleton<ReportsAdminGrpcClient>();
        services.AddSingleton<DashboardAdminGrpcClient>();
        services.AddSingleton<RoleAdminGrpcClient>();
        services.AddSingleton<SelectionsAdminGrpcClient>();
        services.AddSingleton<AuthAdminGrpcClient>();
        services.AddSingleton<FormCreditGrpcClient>();
        services.AddSingleton<UserAuthGrpcClient>();
        services.AddSingleton<SiteSettingGrpcClient>();

        // Configure gRPC clients
        var apiServiceUrl = configuration["ApiServiceUrl"] ?? "https://localhost:7156";
        var grpcServerUrl = configuration["GrpcServer:Url"] ?? apiServiceUrl;
        var productServiceUrl = configuration.GetValue<string>("GrpcSettings:ProductAddress") ?? grpcServerUrl;

        // Add common gRPC clients
        RegisterCommonGrpcClients(services, grpcServerUrl);
        
        // Add admin gRPC clients
        RegisterAdminGrpcClients(services, grpcServerUrl, productServiceUrl, apiServiceUrl);

        return services;
    }

    private static void RegisterCommonGrpcClients(IServiceCollection services, string grpcServerUrl)
    {
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.OrderCheckout.OrderCheckoutService.OrderCheckoutServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.CouponService.CouponService.CouponServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.ShippingService.ShippingService.ShippingServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.PaymentService.PaymentService.PaymentServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserProfileService.UserProfileService.UserProfileServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.Section.SectionService.SectionServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserProduct.UserProductService.UserProductServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.Torob.TorobService.TorobServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.ProductDetail.ProductDetailService.ProductDetailServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.ProductGallery.ProductGalleryService.ProductGalleryServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserStore.UserStoreService.UserStoreServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.Category.CategoryService.CategoryServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.Brand.BrandService.BrandServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.Product.ProductService.ProductServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserPreferencesService.UserPreferencesService.UserPreferencesServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserAuth.UserAuthService.UserAuthServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.FormCredit.FormCreditService.FormCreditServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.Rahkaran.RahkaranService.RahkaranServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));
    }

    private static void RegisterAdminGrpcClients(IServiceCollection services, string grpcServerUrl, string productServiceUrl, string apiServiceUrl)
    {
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.BrandAdmin.BrandAdminService.BrandAdminServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.CategoryAdmin.CategoryAdminService.CategoryAdminServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.CouponAdmin.CouponAdminService.CouponAdminServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.ProductAdmin.ProductAdminService.ProductAdminServiceClient>(
            options => options.Address = new Uri(productServiceUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.ProductComponentsAdmin.ProductComponentsAdminService.ProductComponentsAdminServiceClient>(
            options => options.Address = new Uri(productServiceUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.ProductInventoryAdmin.ProductInventoryAdminService.ProductInventoryAdminServiceClient>(
            options => options.Address = new Uri(productServiceUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.StoreAdmin.StoreAdminService.StoreAdminServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.SectionAdmin.SectionAdminService.SectionAdminServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.ProductDetailAdmin.ProductDetailAdminService.ProductDetailAdminServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.RepresentationAdmin.RepresentationAdminService.RepresentationAdminServiceClient>(
            options => options.Address = new Uri(grpcServerUrl));

        services.AddGrpcClient<Parstech.Shop.Shared.Protos.PropertyAdmin.PropertyAdminService.PropertyAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.ConfigAdmin.ConfigAdminService.ConfigAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.UserAdmin.UserAdminService.UserAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.SettingsAdmin.SettingsAdminService.SettingsAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.FinancialAdmin.FinancialAdminService.FinancialAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.ReportsAdmin.ReportsAdminService.ReportsAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.OrderAdmin.OrderAdminService.OrderAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.RoleAdmin.RoleAdminService.RoleAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.SelectionsAdmin.SelectionsAdminService.SelectionsAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.DashboardAdmin.DashboardAdminService.DashboardAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.AuthAdmin.AuthAdminService.AuthAdminServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.WalletService.WalletService.WalletServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
            
        services.AddGrpcClient<Parstech.Shop.Shared.Protos.SiteSetting.SiteSettingService.SiteSettingServiceClient>(
            options => options.Address = new Uri(apiServiceUrl));
    }
} 