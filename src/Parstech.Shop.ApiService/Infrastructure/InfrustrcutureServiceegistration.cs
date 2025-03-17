using Parstech.Shop.ApiService.Persistence.Repositories;
using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Dapper.Product.Commands;
using Parstech.Shop.ApiService.Persistence.Dapper.ProductProperty.Queries;
using Parstech.Shop.ApiService.Persistence.Dapper.Categury.Queries;
using Parstech.Shop.ApiService.Persistence.Dapper.Order.Commands;
using Parstech.Shop.ApiService.Persistence.Dapper.User.Queries;
using Parstech.Shop.ApiService.Persistence.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Persistence.Dapper.WalletTransaction.Queries;
using Parstech.Shop.ApiService.Persistence.Dapper.ProductStockPrice.Commands;
using Parstech.Shop.ApiService.Persistence.Dapper.Setting.Queries;
using Parstech.Shop.ApiService.Application.Dapper.Categury.Queries;
using Parstech.Shop.ApiService.Application.Dapper.ProductProperty.Queries;
using Parstech.Shop.ApiService.Persistence.Dapper.OrderDetail.Queries;
using Parstech.Shop.ApiService.Application.Dapper.OrderDetail.Queries;
using Parstech.Shop.ApiService.Application.Dapper.Product.Commands;
using Parstech.Shop.ApiService.Application.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Application.Dapper.ProductStockPrice.Commands;
using Parstech.Shop.ApiService.Application.Dapper.Order.Commands;
using Parstech.Shop.ApiService.Application.Dapper.WalletTransaction.Queries;
using Parstech.Shop.ApiService.Application.Dapper.Setting.Queries;
using Parstech.Shop.ApiService.Application.Dapper.User.Queries;

namespace Parstech.Shop.ApiService.Infrastructure;

public static class InfrustrcutureServiceegistration
{
    public static IServiceCollection ConfigureInfrustructureService(this IServiceCollection Services)
    {
        Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        Services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
        Services.AddScoped<ISocialSettingRepository, SocialSettingRepository>();
        Services.AddScoped<ISectionDetailRepository, SectionDetailRepository>();
        Services.AddScoped<ISectionRepository, SectionRepository>();
        Services.AddScoped<ICateguryRepository, CateguryRepository>();
        Services.AddScoped<ISectionTypeRepository, SectionTypeRepository>();
        Services.AddScoped<IUserBillingRepository, UserBillingRepository>();
        Services.AddScoped<IUserRepository, UserRepository>();
        Services.AddScoped<IUserShippingRepository, UserShippingRepository>();
        Services.AddScoped<IRoleRepository, RoleRepository>();
        Services.AddScoped<IUserStoreRepository, UserStoreRepository>();
        Services.AddScoped<IProductRepository, ProductRepository>();
        Services.AddScoped<IBrandRepository, BrandRepository>();
        Services.AddScoped<IProductGallleryRepository, ProductGalleryRepository>();
        Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        Services.AddScoped<IPropertyRepository, PropertyRepository>();
        Services.AddScoped<ITaxRepository, TaxRepository>();
        Services.AddScoped<IProductPropertyRepository, ProductPropertyRepository>();
        Services.AddScoped<IProductCateguryRepository, ProductCateguryRepository>();
        Services.AddScoped<IProductRepresesntationRepository, ProductRepresesntationRepository>();
        Services.AddScoped<IPropertyCateguryRepository, PropertyCateguryRepository>();
        Services.AddScoped<IRepresentationRepository, RepresentationRepository>();
        Services.AddScoped<IRepresentationTypeRepository, RepresentationTypeRepository>();
        Services.AddScoped<IProductLogRepository, ProductLogRepository>();
        Services.AddScoped<IOrderRepository, OrderRepository>();
        Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        Services.AddScoped<IOrderShippingRepository, OrderShippingRepository>();
        Services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
        Services.AddScoped<IStatusRepository, StatusRepository>();
        Services.AddScoped<IProductLogTypeRepository, ProductLogTypeRepository>();
        Services.AddScoped<ICouponRepository, CouponRepository>();
        Services.AddScoped<ICouponTypeRepository, CouponTypeRepository>();
        Services.AddScoped<IOrderCouponRepository, OrderCouponRepository>();
        Services.AddScoped<IWalletRepository, WalletRepository>();
        Services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
        Services.AddScoped<IWalletTypesRepository, WalletTypesRepository>();
        Services.AddScoped<IReporstRepository, ReporstRepository>();
        Services.AddScoped<ITicketRepository, TicketRepository>();
        Services.AddScoped<ITicketDetailRepository, TicketDetailRepository>();
        Services.AddScoped<ITicketStatusesRepository, TicketStatusesRepository>();
        Services.AddScoped<ITicketStatusesRepository, TicketStatusesRepository>();
        Services.AddScoped<IProductRelatedRepository, ProductRelatedRepository>();
        Services.AddScoped<IOrderPayRepository, OrderPayRepository>();
        Services.AddScoped<IUserProductRepository, UserProductRepository>();
        Services.AddScoped<ICouponPcuRepository, CouponPcuRepository>();
        Services.AddScoped<IPayTypeRepository, PayTypeRepository>();
        Services.AddScoped<IShippingTypeRepository, ShippingTypeRepository>();
        Services.AddScoped<IStateRepository, StateRepository>();
        Services.AddScoped<IProductStockPriceRepository, ProductStockPriceRepository>();
        Services.AddScoped<IFormCreditRepository, FormCreditRepository>();
        Services.AddScoped<ISecoundPayAfterDargahRepository, SecoundPayAfterDargahRepository>();
        Services.AddScoped<IRahkaranOrderRepository, RahkaranOrderRepository>();
        Services.AddScoped<IRahkaranUserRepository, RahkaranUserRepository>();
        Services.AddScoped<IRahkaranProductRepository, RahkaranProductRepository>();
        Services.AddScoped<IProductStockPriceSectionRepository, ProductStockPriceSectionRepository>();
        Services.AddScoped<IUserCateguryRepository, UserCateguryRepository>();


        Services.AddScoped<IProductCommand, ProductCommand>();
        Services.AddScoped<IProductStockPriceCommand, ProductStockPriceCommand>();
        Services.AddScoped<IProductQueries, ProductQueries>();
        Services.AddScoped<ICateguryQueries, CateguryQueries>();
        Services.AddScoped<IOrderCommand, OrderCommand>();
        Services.AddScoped<IProductPropertyQueries, ProductPropertyQueries>();
        Services.AddScoped<IOrderDetailQueries, OrderDetailQueries>();
        Services.AddScoped<ISettingQuery, SettingQuery>();
        Services.AddScoped<ITransactionQueries, TransactionQueries>();
        Services.AddScoped<IUserQueries, UserQueries>();

        return Services;
    }
}