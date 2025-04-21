using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.Product;
using Shop.Application.Enum;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductProperty.Requests.Queries;
using Shop.Application.Features.ProductRelated.Requests.Queries;


namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class ProductDetailShowQueryHandler : IRequestHandler<ProductDetailShowQueryReq, ProductDetailShowDto>
    {

        private readonly IMediator _mediator;
        private readonly IProductRepository _productRep;
        private readonly IBrandRepository _brandRep;
        private readonly IProductGallleryRepository _productGallleryRep;
        private readonly IProductPropertyRepository _productPropertyRep;
        private readonly IPropertyRepository _propertyRep;
        private readonly IPropertyCateguryRepository _propertCactrguryRep;
        private readonly IUserStoreRepository _userStoreRep;
        private readonly IUserRepository _userRep;
        private readonly IMapper _mapper;
        private readonly IProductStockPriceRepository _productStockRep;
        private readonly IProductQueries _productQueries;
        private readonly string _connectionString;

        public ProductDetailShowQueryHandler(
            IMediator mediator, IProductRepository productRep,
            IMapper mapper,
            IBrandRepository brandRep,
            IProductGallleryRepository productGallleryRep,
            IProductPropertyRepository productPropertyRep,
            IPropertyRepository propertyRep,
            IUserStoreRepository userStoreRep,
            IProductQueries productQueries,
            IConfiguration configuration,
            IPropertyCateguryRepository propertCactrguryRep, IProductStockPriceRepository productStockRep, IUserRepository userRep)
        {
            _mediator = mediator;
            _productRep = productRep;
            _mapper = mapper;
            _brandRep = brandRep;
            _productGallleryRep = productGallleryRep;
            _propertyRep = propertyRep;
            _productPropertyRep = productPropertyRep;
            _userStoreRep = userStoreRep;
            _propertCactrguryRep = propertCactrguryRep;
            _productStockRep = productStockRep;
            _productQueries = productQueries;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            _userRep = userRep;
        }

        public async Task<ProductDetailShowDto> Handle(ProductDetailShowQueryReq request, CancellationToken cancellationToken)
        {

            var productStockPrice = await _productStockRep.GetAsync(request.productStockPriceId);

            #region Check Discount Price And Date
            if (productStockPrice.DiscountDate < DateTime.Now)
            {
                productStockPrice.DiscountDate = null;
                productStockPrice.DiscountPrice = 0;
                await _productStockRep.UpdateAsync(productStockPrice);
            }
            #endregion


            var product = await _productRep.GetAsync(productStockPrice.ProductId);
            var productId = product.Id;

            if (product.TypeId == 3)
            {
                productId = product.ParentId.Value;
            }
            var Result = _mapper.Map<ProductDetailShowDto>(product);
            Result.ProductStockPriceId = productStockPrice.Id;
            Result.ShortLink = product.ShortLink;
            Result.DiscountDate = productStockPrice.DiscountDate;
            Result.CateguryOfUserId = productStockPrice.CateguryOfUserId;
            var brand = await _brandRep.GetAsync(product.BrandId);
            var Store = await _userStoreRep.GetAsync(productStockPrice.StoreId);

            


            Result.Store = Store.StoreName;
            Result.StoreLatin = Store.LatinStoreName;
            var existUserCategury = false;
            switch (product.TypeId)
            {
                case 2:
                    var variations = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetFirstVariation, new { @parentId = product.Id }).ToList());
                    if (variations.Count > 0)
                    {
                        var variation = variations.FirstOrDefault();
                        Result.ProductStockPriceId = variation.Id;


                        #region Check UserCategury

                        if (request.userName != null)
                        {
                            existUserCategury = await _userRep.ExistUserCategury(request.userName);
                        }
                        if (variation.CateguryOfUserId != null)
                        {
                            if (!existUserCategury)
                            {
                                if (variation.CateguryOfUserType == CateguryOfUserType.ShowDiscoutProductForUserCategury.ToString())
                                {
                                    Result.DiscountPrice = 0;
                                    Result.SalePrice = variation.SalePrice;
                                    Result.Quantity = variation.Quantity;

                                }
                                else if (variation.CateguryOfUserType == CateguryOfUserType.ShowProductJustForUserCategury.ToString())
                                {
                                    Result.DiscountPrice = 0;
                                    Result.SalePrice = 0;
                                    Result.Quantity = 0;
                                }
                            }
                            else
                            {
                                Result.DiscountPrice = variation.DiscountPrice;
                                Result.SalePrice = variation.SalePrice;
                                Result.Quantity = variation.Quantity;
                            }
                        }
                        else
                        {
                            Result.DiscountPrice = variation.DiscountPrice;
                            Result.SalePrice = variation.SalePrice;
                            Result.Quantity = variation.Quantity;
                        }
                        #endregion


                        //Result.Quantity = variation.Quantity;
                        //Result.SalePrice = variation.SalePrice;
                        //Result.DiscountPrice = variation.DiscountPrice;
                    }

                    break;

                default:


                    #region Check UserCategury

                    if (request.userName != null)
                    {
                        existUserCategury = await _userRep.ExistUserCategury(request.userName);
                    }
                    if (productStockPrice.CateguryOfUserId != null)
                    {
                        if (!existUserCategury)
                        {
                            if (productStockPrice.CateguryOfUserType == CateguryOfUserType.ShowDiscoutProductForUserCategury.ToString())
                            {
                                Result.DiscountPrice = 0;
                                Result.SalePrice = productStockPrice.SalePrice;
                                Result.Quantity = productStockPrice.Quantity;

                            }
                            else if (productStockPrice.CateguryOfUserType == CateguryOfUserType.ShowProductJustForUserCategury.ToString())
                            {
                                Result.DiscountPrice = 0;
                                Result.SalePrice = 0;
                                Result.Quantity = 0;
                            }
                        }
                        else
                        {
                            Result.DiscountPrice = productStockPrice.DiscountPrice;
                            Result.SalePrice = productStockPrice.SalePrice;
                            Result.Quantity = productStockPrice.Quantity;
                        }
                    }
                    else
                    {
                        Result.DiscountPrice = productStockPrice.DiscountPrice;
                        Result.SalePrice = productStockPrice.SalePrice;
                        Result.Quantity = productStockPrice.Quantity;
                    }
                    #endregion


                    //Result.DiscountPrice = productStockPrice.DiscountPrice;
                    //Result.SalePrice = productStockPrice.SalePrice;
                    //Result.Quantity = productStockPrice.Quantity;

                    break;
            }


            Result.Brand = _mapper.Map<BrandDto>(brand);
            
            switch (product.TypeId)
            {
                case 1:
                    break;
                case 2:
                    Result.Childs = await _mediator.Send(new GetChildsProductsByParrentIdQueryReq(product.Id));
                    break;
                case 3:
                    if (product.ParentId != null)
                    {
                        Result.Childs = await _mediator.Send(new GetChildsProductsByParrentIdQueryReq(product.ParentId.Value));
                    }
                    
                    break;
                case 4:
                    //Result.Childs = await _mediator.Send(new GetChildsProductsByParrentIdQueryReq(product.Id));
                    break;
                case 5:
                    if (product.ParentId != null)
                    {
                        Result.Childs = await _mediator.Send(new GetChildsProductsByParrentIdQueryReq(product.ParentId.Value));
                    }
                    break;
            }

            if (product.TypeId == 3)
            {
                var propList = await _mediator.Send(new PropertiesOfProductQueryReq(product.ParentId.Value));
                var BaseProperties = await _mediator.Send(new BasePropertiesOfProductQueryReq(product.ParentId.Value));
                Result.Properties = BaseProperties;
                Result.SomeProperties = propList.Take(7).ToList();
            }
            else
            {
                var propList = await _mediator.Send(new PropertiesOfProductQueryReq(product.Id));
                var BaseProperties = await _mediator.Send(new BasePropertiesOfProductQueryReq(product.Id));
                Result.Properties = BaseProperties;
                Result.SomeProperties = propList.Take(7).ToList();
            }
            Result.RelatedStores = await _mediator.Send(new GetRelatedProductsByProductIdQueryReq(productId, request.userName));
            return Result;
        }
    }
}
