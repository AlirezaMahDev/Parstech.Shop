using MediatR;
using Newtonsoft.Json;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.Property;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.ProductCategury.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductProperty.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Commands;
using Shop.Application.Features.Property.Requests.Commands;
using Shop.Application.Images;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Shop.Application.Features.Api.Handlers.Queries
{

    public class GetProductsFromWordpressQueryHandler : IRequestHandler<GetProductsFromWordpressQueryReq, List<resultWordpress>>
    {

        private readonly IMediator _mediator;
        private readonly IProductRepository _productRep;
        private readonly IProductStockPriceRepository _productStockRep;
       
        private readonly IUserStoreRepository _userStore;
        private readonly IPropertyRepository _propertyRep;
        private readonly ICateguryRepository _categuryRep;
        private readonly IProductGallleryRepository _productGalleryRep;

        public GetProductsFromWordpressQueryHandler(IMediator mediator,
            IProductRepository productRep,
            ICateguryRepository categuryRep,
            IProductGallleryRepository productGalleryRep,
            IPropertyRepository propertyRep,
            IUserStoreRepository userStore, IProductStockPriceRepository productStockRep)
        {
            _mediator = mediator;
            _productRep = productRep;
            _categuryRep = categuryRep;
            _productGalleryRep = productGalleryRep;
            _propertyRep = propertyRep;
            _userStore = userStore;
            _productStockRep = productStockRep;
        }
        public async Task<List<resultWordpress>> Handle(GetProductsFromWordpressQueryReq request, CancellationToken cancellationToken)
        {
            HttpClient clients = new HttpClient();
            List<resultWordpress>resultWordpresses = new List<resultWordpress>();

            //var path = $"https://parstechworld.ir/wp-json/wc/v3/products?&consumer_secret=cs_8207eb826d0e6b280caaa8c21ccfe35817b4e79a&consumer_key=ck_c1c3b0cb857602948565ea902b6f153b69ac0561&per_page=71&page={request.page}&order=asc";
            var path = $"https://parstechworld.ir/wp-json/wc/v3/products?&consumer_secret=cs_8207eb826d0e6b280caaa8c21ccfe35817b4e79a&consumer_key=ck_c1c3b0cb857602948565ea902b6f153b69ac0561&per_page=100&page={request.page}&order=desc";
            try
            {
                HttpResponseMessage response = await clients.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<List<WordpressDto>>(res);

                    foreach (var item in Result)
                    {
                        var currentProduct=await _productRep.GetProductByVosit(item.id);
                        if (currentProduct.Id != 0)
                        {
                            var ps=await _productStockRep.GetAllByProductId(currentProduct.Id);
                            if (ps.Count > 0)
                            {
                                var FirstPs = ps.FirstOrDefault();
                                switch (item.stock_status)
                                {
                                    case "instock": FirstPs.StockStatus = true; break;
                                    case "outofstock": FirstPs.StockStatus = false; break;
                                    default: FirstPs.StockStatus = false; break;
                                }

                                if (item.regular_price != "")
                                {
                                    FirstPs.SalePrice = long.Parse(item.regular_price);
                                }
                                else
                                {
                                    FirstPs.SalePrice = 0;
                                }
                                if (item.sale_price != "")
                                {
                                    FirstPs.DiscountPrice = long.Parse(item.sale_price);
                                }
                                else
                                {
                                    FirstPs.DiscountPrice = 0;
                                }
                                if (item.stock_quantity != 0 && item.stock_quantity != null)
                                {
                                    FirstPs.Quantity = item.stock_quantity.Value;
                                }
                                else
                                {
                                    FirstPs.Quantity = 0;
                                }

                                var shopname = "";
                                if (item.store.shop_name == "")
                                {
                                    shopname = item.store.name;
                                }
                                else
                                {
                                    shopname = item.store.shop_name;
                                }
                                var store = await _userStore.GetStoreByName(shopname);
                                if (store != null)
                                {
                                    FirstPs.StoreId = store.Id;
                                    FirstPs.RepId = store.RepId;
                                    foreach(var child in ps)
                                    {
                                        child.StoreId = store.Id;
                                        child.RepId = store.RepId;
                                        await _productStockRep.UpdateAsync(child);
                                    }
                                }

                                    await _productStockRep.UpdateAsync(FirstPs);
                            }
                            
                            //await _mediator.Send(new ProductStockPriceCreateCommandReq(productStockPriceDto));
                        }
                        else
                        {
                            try
                            {
                                ProductDto product = new ProductDto()
                                {
                                    Name = item.name,
                                    Visit = item.id,
                                    Code = item.sku,
                                    BrandId = 4,
                                    TaxId = 1,

                                };
                                if (item.description != null && item.description != "")
                                {
                                    product.Description = item.description;
                                }
                                else
                                {
                                    product.Description = "-";
                                }
                                switch (item.type)
                                {
                                    case "simple":
                                        product.TypeId = 1;
                                        break;
                                    case "variable":
                                        product.TypeId = 2;
                                        break;
                                    default: product.TypeId = 3; break;

                                }


                                var createdProduct = await _mediator.Send(new ProductCreateCommandReq(product));
                                var counter = 0;
                                foreach (var image in item.images)
                                {

                                    try
                                    {
                                        var fileName = $"{createdProduct.Visit}-{counter}";
                                        var url = image.src;
                                        var name = await ImageDownloadAndSave.DownloadImageAsync(fileName, new Uri(url));
                                        ProductGalleryDto productGallery = new ProductGalleryDto()
                                        {
                                            ProductId = createdProduct.Id,
                                            ImageName = name,
                                            Alt = image.alt,
                                            IsMain = true,
                                        };
                                        await _mediator.Send(new ProductGalleryCreateCommandReq(productGallery));
                                        counter++;
                                    }
                                    catch (Exception e)
                                    {
                                        resultWordpress r1 = new resultWordpress()
                                        {
                                            productId = item.id,
                                            type = "image",
                                            error = "ERROR"

                                        };
                                        resultWordpresses.Add(r1);
                                    }

                                }


                                var store = await _userStore.GetStoreByName(item.store.shop_name);
                                if (store != null)
                                {
                                    try
                                    {
                                        ProductStockPriceDto productStockPriceDto = new ProductStockPriceDto()
                                        {
                                            ProductId = createdProduct.Id,
                                            Price = 0,

                                            BasePrice = 0,
                                            MaximumSaleInOrder = 5,
                                            StoreId = store.Id,
                                            RepId = store.RepId,
                                            TaxId = 1,
                                        };
                                        switch (item.stock_status)
                                        {
                                            case "instock": productStockPriceDto.StockStatus = true; break;
                                            case "outofstock": productStockPriceDto.StockStatus = false; break;
                                            default: productStockPriceDto.StockStatus = false; break;
                                        }

                                        if (item.regular_price != "")
                                        {
                                            productStockPriceDto.SalePrice = long.Parse(item.regular_price);
                                        }
                                        else
                                        {
                                            productStockPriceDto.SalePrice = 0;
                                        }
                                        if (item.sale_price != "")
                                        {
                                            productStockPriceDto.DiscountPrice = long.Parse(item.sale_price);
                                        }
                                        else
                                        {
                                            productStockPriceDto.DiscountPrice = 0;
                                        }
                                        if (item.stock_quantity != 0 && item.stock_quantity != null)
                                        {
                                            productStockPriceDto.Quantity = item.stock_quantity.Value;
                                        }
                                        else
                                        {
                                            productStockPriceDto.Quantity = 0;
                                        }
                                        await _mediator.Send(new ProductStockPriceCreateCommandReq(productStockPriceDto));
                                    }
                                    catch (Exception ex)
                                    {

                                        resultWordpress r1 = new resultWordpress()
                                        {
                                            productId = item.id,
                                            type = "stockpric",
                                            error = ex.Message

                                        };
                                        resultWordpresses.Add(r1);
                                    }

                                }
                                else
                                {
                                    ProductStockPriceDto pStockPriceDto = new ProductStockPriceDto()
                                    {
                                        ProductId = createdProduct.Id,
                                        Price = 0,
                                        BasePrice = 0,
                                        MaximumSaleInOrder = 5,
                                        StoreId = 20,
                                        RepId = 1,
                                        TaxId = 1,
                                    };
                                    switch (item.stock_status)
                                    {
                                        case "instock": pStockPriceDto.StockStatus = true; break;
                                        case "outofstock": pStockPriceDto.StockStatus = false; break;
                                        default: pStockPriceDto.StockStatus = false; break;
                                    }

                                    if (item.regular_price != "")
                                    {
                                        pStockPriceDto.SalePrice = long.Parse(item.regular_price);
                                    }
                                    else
                                    {
                                        pStockPriceDto.SalePrice = 0;
                                    }
                                    if (item.sale_price != "")
                                    {
                                        pStockPriceDto.DiscountPrice = long.Parse(item.sale_price);
                                    }
                                    else
                                    {
                                        pStockPriceDto.DiscountPrice = 0;
                                    }
                                    if (item.stock_quantity != 0 && item.stock_quantity != null)
                                    {
                                        pStockPriceDto.Quantity = item.stock_quantity.Value;
                                    }
                                    else
                                    {
                                        pStockPriceDto.Quantity = 0;
                                    }
                                    await _mediator.Send(new ProductStockPriceCreateCommandReq(pStockPriceDto));
                                }

                                foreach (var category in item.categories)
                                {
                                    try
                                    {
                                        var categury = await _categuryRep.GetCateguryByName(category.name);
                                        if (categury != null)
                                        {
                                            ProductCateguryDto productCategury = new ProductCateguryDto()
                                            {
                                                ProductId = createdProduct.Id,
                                                CateguryId = categury.GroupId,
                                            };
                                            await _mediator.Send(new ProductCateguryCreateCommandReq(productCategury));
                                        }
                                        else
                                        {

                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        resultWordpress r1 = new resultWordpress()
                                        {
                                            productId = item.id,
                                            type = "categury",
                                            error = e.InnerException.ToString()

                                        };
                                        resultWordpresses.Add(r1);
                                    }
                                }

                                foreach (var attr in item.attributes)
                                {

                                    try
                                    {
                                        if (await _propertyRep.ExistProperty(attr.name))
                                        {
                                            var property = await _propertyRep.GetByName(attr.name);
                                            if (property != null)
                                            {
                                                ProductPropertyDto productProperty = new ProductPropertyDto()
                                                {
                                                    ProductId = createdProduct.Id,
                                                    PropertyId = property.Id,
                                                    Value = attr.options.First()
                                                };
                                                await _mediator.Send(new ProductPropertyCreateCommandReq(productProperty));
                                            }

                                        }
                                        else
                                        {
                                            PropertyDto property = new PropertyDto()
                                            {
                                                Caption = attr.name,
                                                CateguryId = 826,
                                                PropertyCateguryId = 4
                                            };
                                            var newproperty = await _mediator.Send(new PropertyCreateCommandReq(property));
                                            ProductPropertyDto productProperty = new ProductPropertyDto()
                                            {
                                                ProductId = createdProduct.Id,
                                                PropertyId = newproperty.Id,
                                                Value = attr.options.First()
                                            };
                                            await _mediator.Send(new ProductPropertyCreateCommandReq(productProperty));
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        resultWordpress r1 = new resultWordpress()
                                        {
                                            productId = item.id,
                                            type = "attribute",
                                            error = e.InnerException.ToString()

                                        };
                                        resultWordpresses.Add(r1);
                                    }
                                }


                            }
                            catch (Exception e)
                            {
                                resultWordpress r1 = new resultWordpress()
                                {
                                    productId = item.id,
                                    type = "attribute",
                                    error = e.InnerException.ToString()

                                };
                                resultWordpresses.Add(r1);
                            }
                        }
                        
                        
                    }


                }
            }
            catch (Exception e)
            {
                resultWordpress res = new resultWordpress()
                {
                    productId = 0,
                    type = "base",
                    error ="ERROR"
                    //error = e.InnerException.ToString()
               
                };
                resultWordpresses.Add(res);

            }
            return resultWordpresses;
        }
    }


    public class GetvariationsFromWordpressQueryHandler : IRequestHandler<GetvariationsFromWordpressQueryReq, List<resultWordpress>>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRep;
        private readonly IProductStockPriceRepository _productStockRep;
        private readonly IUserStoreRepository _userStore;
        private readonly IPropertyRepository _propertyRep;
        private readonly ICateguryRepository _categuryRep;
        private readonly IProductGallleryRepository _productGalleryRep;

        public GetvariationsFromWordpressQueryHandler(IMediator mediator,
            IProductRepository productRep,
            ICateguryRepository categuryRep,
            IProductGallleryRepository productGalleryRep,
            IPropertyRepository propertyRep,
            IProductStockPriceRepository productStockRep,
            IUserStoreRepository userStore)
        {
            _mediator = mediator;
            _productRep = productRep;
            _categuryRep = categuryRep;
            _productGalleryRep = productGalleryRep;
            _propertyRep = propertyRep;
            _userStore = userStore;
            _productStockRep= productStockRep;
        }
        public async Task<List<resultWordpress>> Handle(GetvariationsFromWordpressQueryReq request, CancellationToken cancellationToken)
        {
            HttpClient clients = new HttpClient();
            List<resultWordpress> resultWordpresses = new List<resultWordpress>();

            var variables =await _productRep.GetAllParentVariableProduct(null);
            foreach(var variable in variables)
            {
                
                var path = $"https://parstechworld.ir/wp-json/wc/v3/products/{variable.Visit}/variations?&consumer_secret=cs_8207eb826d0e6b280caaa8c21ccfe35817b4e79a&consumer_key=ck_c1c3b0cb857602948565ea902b6f153b69ac0561";
                try
                {
                    HttpResponseMessage response = await clients.GetAsync(path);
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync();
                        var Result = JsonConvert.DeserializeObject<List<variationRoot>>(res);

                        foreach (var item in Result)
                        {
                            

                            try
                            {
                                var variationname = item.attributes.FirstOrDefault();
                                if (variationname != null)
                                {
                                    ProductDto product = new ProductDto()
                                    {
                                        Name = $"{variable.Name}-{variationname.option}",
                                        Visit = item.id,
                                        Code = item.sku,
                                        BrandId = 4,
                                        TaxId = 1,
                                        Description = "-",
                                        TypeId = 3,
                                        VariationName = variationname.option,
                                        ParentId = variable.Id,
                                        ShortLink = variable.ShortLink,
                                    };



                                    var createdProduct = await _mediator.Send(new ProductCreateCommandReq(product));



                                    var VariableStockPrices = await _productStockRep.GetAllByProductId(variable.Id);
                                    if (VariableStockPrices.Count > 0)
                                    {
                                        var firstVariablestock = VariableStockPrices.FirstOrDefault();
                                        ProductStockPriceDto productStockPriceDto = new ProductStockPriceDto()
                                        {
                                            ProductId = createdProduct.Id,
                                            Price = 0,

                                            BasePrice = 0,
                                            MaximumSaleInOrder = 5,
                                            StoreId = firstVariablestock.StoreId,
                                            RepId = firstVariablestock.RepId,
                                            TaxId = 1,
                                        };
                                        switch (item.stock_status)
                                        {
                                            case "instock": productStockPriceDto.StockStatus = true; break;
                                            case "outofstock": productStockPriceDto.StockStatus = false; break;
                                            default: productStockPriceDto.StockStatus = false; break;
                                        }

                                        if (item.regular_price != "")
                                        {
                                            productStockPriceDto.SalePrice = long.Parse(item.regular_price);
                                        }
                                        else
                                        {
                                            productStockPriceDto.SalePrice = 0;
                                        }
                                        if (item.sale_price != "")
                                        {
                                            productStockPriceDto.DiscountPrice = long.Parse(item.sale_price);
                                        }
                                        else
                                        {
                                            productStockPriceDto.DiscountPrice = 0;
                                        }
                                        if (item.stock_quantity != 0)
                                        {
                                            productStockPriceDto.Quantity = item.stock_quantity;
                                        }
                                        else
                                        {
                                            productStockPriceDto.Quantity = 0;
                                        }
                                        await _mediator.Send(new ProductStockPriceCreateCommandReq(productStockPriceDto));

                                    }
                                    else
                                    {
                                        ProductStockPriceDto pStockPriceDto = new ProductStockPriceDto()
                                        {
                                            ProductId = createdProduct.Id,
                                            Price = 0,
                                            BasePrice = 0,
                                            MaximumSaleInOrder = 5,
                                            StoreId = 14,
                                            RepId = 29,
                                            TaxId = 1,
                                        };
                                        switch (item.stock_status)
                                        {
                                            case "instock": pStockPriceDto.StockStatus = true; break;
                                            case "outofstock": pStockPriceDto.StockStatus = false; break;
                                            default: pStockPriceDto.StockStatus = false; break;
                                        }

                                        if (item.regular_price != "")
                                        {
                                            pStockPriceDto.SalePrice = long.Parse(item.regular_price);
                                        }
                                        else
                                        {
                                            pStockPriceDto.SalePrice = 0;
                                        }
                                        if (item.sale_price != "")
                                        {
                                            pStockPriceDto.DiscountPrice = long.Parse(item.sale_price);
                                        }
                                        else
                                        {
                                            pStockPriceDto.DiscountPrice = 0;
                                        }
                                        if (item.stock_quantity != 0)
                                        {
                                            pStockPriceDto.Quantity = item.stock_quantity;
                                        }
                                        else
                                        {
                                            pStockPriceDto.Quantity = 0;
                                        }
                                        await _mediator.Send(new ProductStockPriceCreateCommandReq(pStockPriceDto));
                                    }
                                }
                                



                            }
                            catch (Exception e)
                            {
                                resultWordpress r1 = new resultWordpress()
                                {
                                    productId = item.id,
                                    type = "attribute",
                                    error = e.InnerException.ToString()

                                };
                                resultWordpresses.Add(r1);
                            }

                        }


                    }
                }
                catch (Exception e)
                {
                    resultWordpress res = new resultWordpress()
                    {
                        productId = 0,
                        type = "base",
                        error = e.InnerException.ToString()

                    };
                    resultWordpresses.Add(res);

                }
            }

            
            return resultWordpresses;
        }
    }




    public class FixproductStockPriceQueryHandler : IRequestHandler<FixproductStockPriceQueryReq, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRep;
        private readonly IProductStockPriceRepository _productStockRep;
        private readonly IUserStoreRepository _userStore;
        private readonly IPropertyRepository _propertyRep;
        private readonly ICateguryRepository _categuryRep;
        private readonly IProductGallleryRepository _productGalleryRep;

        public FixproductStockPriceQueryHandler(IMediator mediator,
            IProductRepository productRep,
            ICateguryRepository categuryRep,
            IProductGallleryRepository productGalleryRep,
            IPropertyRepository propertyRep,
            IProductStockPriceRepository productStockRep,
            IUserStoreRepository userStore)
        {
            _mediator = mediator;
            _productRep = productRep;
            _categuryRep = categuryRep;
            _productGalleryRep = productGalleryRep;
            _propertyRep = propertyRep;
            _userStore = userStore;
            _productStockRep = productStockRep;
        }
        public async Task<Unit> Handle(FixproductStockPriceQueryReq request, CancellationToken cancellationToken)
        {
            HttpClient clients = new HttpClient();
            List<resultWordpress> resultWordpresses = new List<resultWordpress>();

            var products = await _productRep.GetAll();
            foreach (var item in products)
            {
                var ps=await _productStockRep.GetFirstProductStockPriceIdFromProductId(item.Id);
                if(ps==0)
                {
                    ProductStockPriceDto pStockPriceDto = new ProductStockPriceDto()
                    {
                        ProductId = item.Id,
                        Price = 0,
                        BasePrice = 0,
                        MaximumSaleInOrder = 5,
                        StoreId = 14,
                        RepId = 29,
                        TaxId = 1,
                        StockStatus = false,
                        SalePrice = 0,
                        DiscountPrice = 0,
                        Quantity = 0
                    };
                    
                    await _mediator.Send(new ProductStockPriceCreateCommandReq(pStockPriceDto));
                }
               
            }


            return Unit.Value;
        }
    }


    public class GetProductFromWordpressQueryHandler : IRequestHandler<GetProductFromWordpressQueryReq, List<resultWordpress>>
    {

        private readonly IMediator _mediator;
        private readonly IProductRepository _productRep;

        private readonly IUserStoreRepository _userStore;
        private readonly IPropertyRepository _propertyRep;
        private readonly ICateguryRepository _categuryRep;
        private readonly IProductGallleryRepository _productGalleryRep;

        public GetProductFromWordpressQueryHandler(IMediator mediator,
            IProductRepository productRep,
            ICateguryRepository categuryRep,
            IProductGallleryRepository productGalleryRep,
            IPropertyRepository propertyRep,
            IUserStoreRepository userStore)
        {
            _mediator = mediator;
            _productRep = productRep;
            _categuryRep = categuryRep;
            _productGalleryRep = productGalleryRep;
            _propertyRep = propertyRep;
            _userStore = userStore;
        }
        public async Task<List<resultWordpress>> Handle(GetProductFromWordpressQueryReq request, CancellationToken cancellationToken)
        {
            HttpClient clients = new HttpClient();
            List<resultWordpress> resultWordpresses = new List<resultWordpress>();

            var path = $"https://parstechworld.ir/wp-json/wc/v3/products/{request.ProductId}?&consumer_secret=cs_8207eb826d0e6b280caaa8c21ccfe35817b4e79a&consumer_key=ck_c1c3b0cb857602948565ea902b6f153b69ac0561";
            try
            {
                HttpResponseMessage response = await clients.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var item = JsonConvert.DeserializeObject<WordpressDto>(res);

                    try
                    {
                        ProductDto product = new ProductDto()
                        {
                            Name = item.name,
                            Visit = item.id,
                            Code = item.sku,
                            BrandId = 4,
                            TaxId = 1,

                        };
                        if (item.description != null && item.description != "")
                        {
                            product.Description = item.description;
                        }
                        else
                        {
                            product.Description = "-";
                        }
                        switch (item.type)
                        {
                            case "simple":
                                product.TypeId = 1;
                                break;
                            case "variable":
                                product.TypeId = 2;
                                break;
                            default: product.TypeId = 3; break;

                        }


                        var createdProduct = await _mediator.Send(new ProductWordpressCreateCommandReq(product));
                        var counter = 0;
                        foreach (var image in item.images)
                        {

                            try
                            {
                                var fileName = $"{createdProduct.Visit}-{counter}";
                                var url = image.src;
                                var name = await ImageDownloadAndSave.DownloadImageAsync(fileName, new Uri(url));
                                ProductGalleryDto productGallery = new ProductGalleryDto()
                                {
                                    ProductId = createdProduct.Id,
                                    ImageName = name,
                                    Alt = image.alt,
                                    IsMain = true,
                                };
                                await _mediator.Send(new ProductGalleryCreateCommandReq(productGallery));
                                counter++;
                            }
                            catch (Exception e)
                            {
                                resultWordpress r1 = new resultWordpress()
                                {
                                    productId = item.id,
                                    type = "image",
                                    error = e.InnerException.ToString()

                                };
                                resultWordpresses.Add(r1);
                            }

                        }


                        var store = await _userStore.GetStoreByName(item.store.shop_name);
                        if (store != null)
                        {
                            try
                            {
                                ProductStockPriceDto productStockPriceDto = new ProductStockPriceDto()
                                {
                                    ProductId = createdProduct.Id,
                                    Price = 0,

                                    BasePrice = 0,
                                    MaximumSaleInOrder = 5,
                                    StoreId = store.Id,
                                    RepId = store.RepId,
                                    TaxId = 1,
                                };
                                switch (item.stock_status)
                                {
                                    case "instock": productStockPriceDto.StockStatus = true; break;
                                    case "outofstock": productStockPriceDto.StockStatus = false; break;
                                    default: productStockPriceDto.StockStatus = false; break;
                                }

                                if (item.regular_price != "")
                                {
                                    productStockPriceDto.SalePrice = long.Parse(item.regular_price);
                                }
                                else
                                {
                                    productStockPriceDto.SalePrice = 0;
                                }
                                if (item.sale_price != "")
                                {
                                    productStockPriceDto.DiscountPrice = long.Parse(item.sale_price);
                                }
                                else
                                {
                                    productStockPriceDto.DiscountPrice = 0;
                                }
                                if (item.stock_quantity != 0 && item.stock_quantity != null)
                                {
                                    productStockPriceDto.Quantity = item.stock_quantity.Value;
                                }
                                else
                                {
                                    productStockPriceDto.Quantity = 0;
                                }
                                await _mediator.Send(new ProductStockPriceCreateCommandReq(productStockPriceDto));
                            }
                            catch (Exception ex)
                            {

                                resultWordpress r1 = new resultWordpress()
                                {
                                    productId = item.id,
                                    type = "stockpric",
                                    error = ex.Message

                                };
                                resultWordpresses.Add(r1);
                            }

                        }
                        else
                        {
                            ProductStockPriceDto pStockPriceDto = new ProductStockPriceDto()
                            {
                                ProductId = createdProduct.Id,
                                Price = 0,
                                BasePrice = 0,
                                MaximumSaleInOrder = 5,
                                StoreId = 20,
                                RepId = 1,
                                TaxId = 1,
                            };
                            switch (item.stock_status)
                            {
                                case "instock": pStockPriceDto.StockStatus = true; break;
                                case "outofstock": pStockPriceDto.StockStatus = false; break;
                                default: pStockPriceDto.StockStatus = false; break;
                            }

                            if (item.regular_price != "")
                            {
                                pStockPriceDto.SalePrice = long.Parse(item.regular_price);
                            }
                            else
                            {
                                pStockPriceDto.SalePrice = 0;
                            }
                            if (item.sale_price != "")
                            {
                                pStockPriceDto.DiscountPrice = long.Parse(item.sale_price);
                            }
                            else
                            {
                                pStockPriceDto.DiscountPrice = 0;
                            }
                            if (item.stock_quantity != 0 && item.stock_quantity != null)
                            {
                                pStockPriceDto.Quantity = item.stock_quantity.Value;
                            }
                            else
                            {
                                pStockPriceDto.Quantity = 0;
                            }
                            await _mediator.Send(new ProductStockPriceCreateCommandReq(pStockPriceDto));
                        }

                        foreach (var category in item.categories)
                        {
                            try
                            {
                                var categury = await _categuryRep.GetCateguryByName(category.name);
                                if (categury != null)
                                {
                                    ProductCateguryDto productCategury = new ProductCateguryDto()
                                    {
                                        ProductId = createdProduct.Id,
                                        CateguryId = categury.GroupId,
                                    };
                                    await _mediator.Send(new ProductCateguryCreateCommandReq(productCategury));
                                }
                            }
                            catch (Exception e)
                            {
                                resultWordpress r1 = new resultWordpress()
                                {
                                    productId = item.id,
                                    type = "categury",
                                    error = e.InnerException.ToString()

                                };
                                resultWordpresses.Add(r1);
                            }
                        }

                        foreach (var attr in item.attributes)
                        {

                            try
                            {
                                if (await _propertyRep.ExistProperty(attr.name))
                                {
                                    var property = await _propertyRep.GetByName(attr.name);
                                    if (property != null)
                                    {
                                        ProductPropertyDto productProperty = new ProductPropertyDto()
                                        {
                                            ProductId = createdProduct.Id,
                                            PropertyId = property.Id,
                                            Value = attr.options.First()
                                        };
                                        await _mediator.Send(new ProductPropertyCreateCommandReq(productProperty));
                                    }

                                }
                                else
                                {
                                    PropertyDto property = new PropertyDto()
                                    {
                                        Caption = attr.name,
                                        CateguryId = 47,
                                        PropertyCateguryId = 4
                                    };
                                    var newproperty = await _mediator.Send(new PropertyCreateCommandReq(property));
                                    ProductPropertyDto productProperty = new ProductPropertyDto()
                                    {
                                        ProductId = createdProduct.Id,
                                        PropertyId = newproperty.Id,
                                        Value = attr.options.First()
                                    };
                                    await _mediator.Send(new ProductPropertyCreateCommandReq(productProperty));
                                }
                            }
                            catch (Exception e)
                            {
                                resultWordpress r1 = new resultWordpress()
                                {
                                    productId = item.id,
                                    type = "attribute",
                                    error = e.InnerException.ToString()

                                };
                                resultWordpresses.Add(r1);
                            }
                        }


                    }
                    catch (Exception e)
                    {
                        resultWordpress r1 = new resultWordpress()
                        {
                            productId = item.id,
                            type = "attribute",
                            error = e.InnerException.ToString()

                        };
                        resultWordpresses.Add(r1);
                    }


                }
            }
            catch (Exception e)
            {
                resultWordpress res = new resultWordpress()
                {
                    productId = 0,
                    type = "base",
                    error = e.InnerException.ToString()

                };
                resultWordpresses.Add(res);

            }
            return resultWordpresses;
        }
    }
}
