using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Persistence.Repositories;
using Parstech.Shop.Context.Application.Dapper.Product.Commands;
using Parstech.Shop.Context.Persistence.Dapper.Product.Commands;
using Parstech.Shop.Context.Application.Dapper.ProductStockPrice.Commands;
using Parstech.Shop.Context.Persistence.Dapper.ProductStockPrice.Commands;
using Parstech.Shop.Context.Application.Dapper.Product.Queries;
using Parstech.Shop.Context.Persistence.Dapper.Product.Queries;
using Parstech.Shop.Context.Application.Dapper.Categury.Queries;
using Parstech.Shop.Context.Application.Dapper.Order.Commands;
using Parstech.Shop.Context.Persistence.Dapper.Order.Commands;
using Parstech.Shop.Context.Application.Dapper.ProductProperty.Queries;
using Parstech.Shop.Context.Persistence.Dapper.ProductProperty.Queries;
using Parstech.Shop.Context.Application.Dapper.OrderDetail.Queries;
using Parstech.Shop.Context.Application.Dapper.Setting.Queries;
using Parstech.Shop.Context.Persistence.Dapper.Setting.Queries;
using Parstech.Shop.Context.Application.Dapper.WalletTransaction.Queries;
using Parstech.Shop.Context.Persistence.Dapper.WalletTransaction.Queries;
using Parstech.Shop.Context.Application.Dapper.User.Queries;
using Parstech.Shop.Context.Persistence.Dapper.Categury.Queries;
using Parstech.Shop.Context.Persistence.Dapper.OrderDetail.Queries;
using Parstech.Shop.Context.Persistence.Dapper.User.Queries;

namespace Parstech.Shop.Context.Infrastructure;

public static class InfrustrcutureServiceegistration
{
    public static void ConfigureInfrustructureService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
        builder.Services.AddScoped<ISocialSettingRepository, SocialSettingRepository>();
        builder.Services.AddScoped<ISectionDetailRepository, SectionDetailRepository>();
        builder.Services.AddScoped<ISectionRepository, SectionRepository>();
        builder.Services.AddScoped<ICateguryRepository, CateguryRepository>();
        builder.Services.AddScoped<ISectionTypeRepository, SectionTypeRepository>();
        builder.Services.AddScoped<IUserBillingRepository, UserBillingRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserShippingRepository, UserShippingRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IUserStoreRepository, UserStoreRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();
        builder.Services.AddScoped<IProductGallleryRepository, ProductGalleryRepository>();
        builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
        builder.Services.AddScoped<ITaxRepository, TaxRepository>();
        builder.Services.AddScoped<IProductPropertyRepository, ProductPropertyRepository>();
        builder.Services.AddScoped<IProductCateguryRepository, ProductCateguryRepository>();
        builder.Services.AddScoped<IProductRepresesntationRepository, ProductRepresesntationRepository>();
        builder.Services.AddScoped<IPropertyCateguryRepository, PropertyCateguryRepository>();
        builder.Services.AddScoped<IRepresentationRepository, RepresentationRepository>();
        builder.Services.AddScoped<IRepresentationTypeRepository, RepresentationTypeRepository>();
        builder.Services.AddScoped<IProductLogRepository, ProductLogRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        builder.Services.AddScoped<IOrderShippingRepository, OrderShippingRepository>();
        builder.Services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
        builder.Services.AddScoped<IStatusRepository, StatusRepository>();
        builder.Services.AddScoped<IProductLogTypeRepository, ProductLogTypeRepository>();
        builder.Services.AddScoped<ICouponRepository, CouponRepository>();
        builder.Services.AddScoped<ICouponTypeRepository, CouponTypeRepository>();
        builder.Services.AddScoped<IOrderCouponRepository, OrderCouponRepository>();
        builder.Services.AddScoped<IWalletRepository, WalletRepository>();
        builder.Services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
        builder.Services.AddScoped<IWalletTypesRepository, WalletTypesRepository>();
        builder.Services.AddScoped<IReporstRepository, ReporstRepository>();
        builder.Services.AddScoped<ITicketRepository, TicketRepository>();
        builder.Services.AddScoped<ITicketDetailRepository, TicketDetailRepository>();
        builder.Services.AddScoped<ITicketStatusesRepository, TicketStatusesRepository>();
        builder.Services.AddScoped<ITicketStatusesRepository, TicketStatusesRepository>();
        builder.Services.AddScoped<IProductRelatedRepository, ProductRelatedRepository>();
        builder.Services.AddScoped<IOrderPayRepository, OrderPayRepository>();
        builder.Services.AddScoped<IUserProductRepository, UserProductRepository>();
        builder.Services.AddScoped<ICouponPcuRepository, CouponPcuRepository>();
        builder.Services.AddScoped<IPayTypeRepository, PayTypeRepository>();
        builder.Services.AddScoped<IShippingTypeRepository, ShippingTypeRepository>();
        builder.Services.AddScoped<IStateRepository, StateRepository>();
        builder.Services.AddScoped<IProductStockPriceRepository, ProductStockPriceRepository>();
        builder.Services.AddScoped<IFormCreditRepository, FormCreditRepository>();
        builder.Services.AddScoped<ISecoundPayAfterDargahRepository, SecoundPayAfterDargahRepository>();
        builder.Services.AddScoped<IRahkaranOrderRepository, RahkaranOrderRepository>();
        builder.Services.AddScoped<IRahkaranUserRepository, RahkaranUserRepository>();
        builder.Services.AddScoped<IRahkaranProductRepository, RahkaranProductRepository>();
        builder.Services.AddScoped<IProductStockPriceSectionRepository, ProductStockPriceSectionRepository>();
        builder.Services.AddScoped<IUserCateguryRepository, UserCateguryRepository>();



        builder.Services.AddScoped<IProductCommand, ProductCommand>();
        builder.Services.AddScoped<IProductStockPriceCommand, ProductStockPriceCommand>();
        builder.Services.AddScoped<IProductQueries, ProductQueries>();
        builder.Services.AddScoped<ICateguryQueries, CateguryQueries>();
        builder.Services.AddScoped<IOrderCommand, OrderCommand>();
        builder.Services.AddScoped<IProductPropertyQueries, ProductPropertyQueries>();
        builder.Services.AddScoped<IOrderDetailQueries, OrderDetailQueries>();
        builder.Services.AddScoped<ISettingQuery, SettingQuery>();
        builder.Services.AddScoped<ITransactionQueries, TransactionQueries>();
        builder.Services.AddScoped<IUserQueries, UserQueries>();
    }
}