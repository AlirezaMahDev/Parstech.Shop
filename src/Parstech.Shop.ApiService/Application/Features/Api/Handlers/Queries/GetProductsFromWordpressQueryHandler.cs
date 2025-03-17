using MediatR;

using Newtonsoft.Json;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Property.Requests.Commands;
using Parstech.Shop.ApiService.Application.Images;

using Attribute = Parstech.Shop.ApiService.Application.DTOs.Attribute;

namespace Parstech.Shop.ApiService.Application.Features.Api.Handlers.Queries;

public class
    GetProductsFromWordpressQueryHandler : IRequestHandler<GetProductsFromWordpressQueryReq, List<resultWordpress>>
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
        IUserStoreRepository userStore,
        IProductStockPriceRepository productStockRep)
    {
        _mediator = mediator;
        _productRep = productRep;
        _categuryRep = categuryRep;
        _productGalleryRep = productGalleryRep;
        _propertyRep = propertyRep;
        _userStore = userStore;
        _productStockRep = productStockRep;
    }

    public async Task<List<resultWordpress>> Handle(GetProductsFromWordpressQueryReq request,
        CancellationToken cancellationToken)
    {
        HttpClient clients = new();
        List<resultWordpress> resultWordpresses = new();

        //var path = $"https://parstechworld.ir/wp-json/wc/v3/products?&consumer_secret=cs_8207eb826d0e6b280caaa8c21ccfe35817b4e79a&consumer_key=ck_c1c3b0cb857602948565ea902b6f153b69ac0561&per_page=71&page={request.page}&order=asc";
        string path =
            $"https://parstechworld.ir/wp-json/wc/v3/products?&consumer_secret=cs_8207eb826d0e6b280caaa8c21ccfe35817b4e79a&consumer_key=ck_c1c3b0cb857602948565ea902b6f153b69ac0561&per_page=100&page={request.page}&order=desc";
        try
        {
            HttpResponseMessage response = await clients.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                List<WordpressDto>? Result = JsonConvert.DeserializeObject<List<WordpressDto>>(res);

                foreach (WordpressDto item in Result)
                {
                    Domain.Models.Product currentProduct = await _productRep.GetProductByVosit(item.id);
                    if (currentProduct.Id != 0)
                    {
                        List<Domain.Models.ProductStockPrice> ps =
                            await _productStockRep.GetAllByProductId(currentProduct.Id);
                        if (ps.Count > 0)
                        {
                            Domain.Models.ProductStockPrice? FirstPs = ps.FirstOrDefault();
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

                            string shopname = "";
                            if (item.store.shop_name == "")
                            {
                                shopname = item.store.name;
                            }
                            else
                            {
                                shopname = item.store.shop_name;
                            }

                            Domain.Models.UserStore store = await _userStore.GetStoreByName(shopname);
                            if (store != null)
                            {
                                FirstPs.StoreId = store.Id;
                                FirstPs.RepId = store.RepId;
                                foreach (Domain.Models.ProductStockPrice child in ps)
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
                            ProductDto product = new()
                            {
                                Name = item.name,
                                Visit = item.id,
                                Code = item.sku,
                                BrandId = 4,
                                TaxId = 1
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
                            int counter = 0;
                            foreach (Image image in item.images)
                            {
                                try
                                {
                                    string fileName = $"{createdProduct.Visit}-{counter}";
                                    string url = image.src;
                                    string name = await ImageDownloadAndSave.DownloadImageAsync(fileName, new(url));
                                    ProductGalleryDto productGallery = new()
                                    {
                                        ProductId = createdProduct.Id,
                                        ImageName = name,
                                        Alt = image.alt,
                                        IsMain = true
                                    };
                                    await _mediator.Send(new ProductGalleryCreateCommandReq(productGallery));
                                    counter++;
                                }
                                catch (Exception e)
                                {
                                    resultWordpress r1 = new() { productId = item.id, type = "image", error = "ERROR" };
                                    resultWordpresses.Add(r1);
                                }
                            }


                            Domain.Models.UserStore store = await _userStore.GetStoreByName(item.store.shop_name);
                            if (store != null)
                            {
                                try
                                {
                                    ProductStockPriceDto productStockPriceDto = new()
                                    {
                                        ProductId = createdProduct.Id,
                                        Price = 0,
                                        BasePrice = 0,
                                        MaximumSaleInOrder = 5,
                                        StoreId = store.Id,
                                        RepId = store.RepId,
                                        TaxId = 1
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
                                    resultWordpress r1 = new()
                                    {
                                        productId = item.id, type = "stockpric", error = ex.Message
                                    };
                                    resultWordpresses.Add(r1);
                                }
                            }
                            else
                            {
                                ProductStockPriceDto pStockPriceDto = new()
                                {
                                    ProductId = createdProduct.Id,
                                    Price = 0,
                                    BasePrice = 0,
                                    MaximumSaleInOrder = 5,
                                    StoreId = 20,
                                    RepId = 1,
                                    TaxId = 1
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

                            foreach (Category category in item.categories)
                            {
                                try
                                {
                                    Domain.Models.Categury? categury =
                                        await _categuryRep.GetCateguryByName(category.name);
                                    if (categury != null)
                                    {
                                        ProductCateguryDto productCategury = new()
                                        {
                                            ProductId = createdProduct.Id, CateguryId = categury.GroupId
                                        };
                                        await _mediator.Send(new ProductCateguryCreateCommandReq(productCategury));
                                    }
                                    else
                                    {
                                    }
                                }
                                catch (Exception e)
                                {
                                    resultWordpress r1 = new()
                                    {
                                        productId = item.id, type = "categury", error = e.InnerException.ToString()
                                    };
                                    resultWordpresses.Add(r1);
                                }
                            }

                            foreach (Attribute attr in item.attributes)
                            {
                                try
                                {
                                    if (await _propertyRep.ExistProperty(attr.name))
                                    {
                                        Domain.Models.Property? property = await _propertyRep.GetByName(attr.name);
                                        if (property != null)
                                        {
                                            ProductPropertyDto productProperty = new()
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
                                        PropertyDto property = new()
                                        {
                                            Caption = attr.name, CateguryId = 826, PropertyCateguryId = 4
                                        };
                                        void newproperty = await _mediator.Send(new PropertyCreateCommandReq(property));
                                        ProductPropertyDto productProperty = new()
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
                                    resultWordpress r1 = new()
                                    {
                                        productId = item.id, type = "attribute", error = e.InnerException.ToString()
                                    };
                                    resultWordpresses.Add(r1);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            resultWordpress r1 = new()
                            {
                                productId = item.id, type = "attribute", error = e.InnerException.ToString()
                            };
                            resultWordpresses.Add(r1);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            resultWordpress res = new()
            {
                productId = 0, type = "base", error = "ERROR"
                //error = e.InnerException.ToString()
            };
            resultWordpresses.Add(res);
        }

        return resultWordpresses;
    }
}

public class
    GetvariationsFromWordpressQueryHandler : IRequestHandler<GetvariationsFromWordpressQueryReq, List<resultWordpress>>
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
        _productStockRep = productStockRep;
    }

    public async Task<List<resultWordpress>> Handle(GetvariationsFromWordpressQueryReq request,
        CancellationToken cancellationToken)
    {
        HttpClient clients = new();
        List<resultWordpress> resultWordpresses = new();

        List<Domain.Models.Product> variables = await _productRep.GetAllParentVariableProduct(null);
        foreach (Domain.Models.Product variable in variables)
        {
            string path =
                $"https://parstechworld.ir/wp-json/wc/v3/products/{variable.Visit}/variations?&consumer_secret=cs_8207eb826d0e6b280caaa8c21ccfe35817b4e79a&consumer_key=ck_c1c3b0cb857602948565ea902b6f153b69ac0561";
            try
            {
                HttpResponseMessage response = await clients.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    List<variationRoot>? Result = JsonConvert.DeserializeObject<List<variationRoot>>(res);

                    foreach (variationRoot item in Result)
                    {
                        try
                        {
                            variationAttribute? variationname = item.attributes.FirstOrDefault();
                            if (variationname != null)
                            {
                                ProductDto product = new()
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
                                    ShortLink = variable.ShortLink
                                };


                                void createdProduct = await _mediator.Send(new ProductCreateCommandReq(product));


                                List<Domain.Models.ProductStockPrice> VariableStockPrices =
                                    await _productStockRep.GetAllByProductId(variable.Id);
                                if (VariableStockPrices.Count > 0)
                                {
                                    Domain.Models.ProductStockPrice? firstVariablestock =
                                        VariableStockPrices.FirstOrDefault();
                                    ProductStockPriceDto productStockPriceDto = new()
                                    {
                                        ProductId = createdProduct.Id,
                                        Price = 0,
                                        BasePrice = 0,
                                        MaximumSaleInOrder = 5,
                                        StoreId = firstVariablestock.StoreId,
                                        RepId = firstVariablestock.RepId,
                                        TaxId = 1
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
                                    ProductStockPriceDto pStockPriceDto = new()
                                    {
                                        ProductId = createdProduct.Id,
                                        Price = 0,
                                        BasePrice = 0,
                                        MaximumSaleInOrder = 5,
                                        StoreId = 14,
                                        RepId = 29,
                                        TaxId = 1
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
                            resultWordpress r1 = new()
                            {
                                productId = item.id, type = "attribute", error = e.InnerException.ToString()
                            };
                            resultWordpresses.Add(r1);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                resultWordpress res = new() { productId = 0, type = "base", error = e.InnerException.ToString() };
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
        HttpClient clients = new();
        List<resultWordpress> resultWordpresses = new();

        IReadOnlyList<Domain.Models.Product> products = await _productRep.GetAll();
        foreach (Domain.Models.Product item in products)
        {
            int ps = await _productStockRep.GetFirstProductStockPriceIdFromProductId(item.Id);
            if (ps == 0)
            {
                ProductStockPriceDto pStockPriceDto = new()
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

public class
    GetProductFromWordpressQueryHandler : IRequestHandler<GetProductFromWordpressQueryReq, List<resultWordpress>>
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

    public async Task<List<resultWordpress>> Handle(GetProductFromWordpressQueryReq request,
        CancellationToken cancellationToken)
    {
        HttpClient clients = new();
        List<resultWordpress> resultWordpresses = new();

        string path =
            $"https://parstechworld.ir/wp-json/wc/v3/products/{request.ProductId}?&consumer_secret=cs_8207eb826d0e6b280caaa8c21ccfe35817b4e79a&consumer_key=ck_c1c3b0cb857602948565ea902b6f153b69ac0561";
        try
        {
            HttpResponseMessage response = await clients.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                WordpressDto? item = JsonConvert.DeserializeObject<WordpressDto>(res);

                try
                {
                    ProductDto product = new()
                    {
                        Name = item.name,
                        Visit = item.id,
                        Code = item.sku,
                        BrandId = 4,
                        TaxId = 1
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


                    void createdProduct = await _mediator.Send(new ProductWordpressCreateCommandReq(product));
                    int counter = 0;
                    foreach (Image image in item.images)
                    {
                        try
                        {
                            string fileName = $"{createdProduct.Visit}-{counter}";
                            string url = image.src;
                            string name = await ImageDownloadAndSave.DownloadImageAsync(fileName, new(url));
                            ProductGalleryDto productGallery = new()
                            {
                                ProductId = createdProduct.Id, ImageName = name, Alt = image.alt, IsMain = true
                            };
                            await _mediator.Send(new ProductGalleryCreateCommandReq(productGallery));
                            counter++;
                        }
                        catch (Exception e)
                        {
                            resultWordpress r1 = new()
                            {
                                productId = item.id, type = "image", error = e.InnerException.ToString()
                            };
                            resultWordpresses.Add(r1);
                        }
                    }


                    Domain.Models.UserStore store = await _userStore.GetStoreByName(item.store.shop_name);
                    if (store != null)
                    {
                        try
                        {
                            ProductStockPriceDto productStockPriceDto = new()
                            {
                                ProductId = createdProduct.Id,
                                Price = 0,
                                BasePrice = 0,
                                MaximumSaleInOrder = 5,
                                StoreId = store.Id,
                                RepId = store.RepId,
                                TaxId = 1
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
                            resultWordpress r1 = new() { productId = item.id, type = "stockpric", error = ex.Message };
                            resultWordpresses.Add(r1);
                        }
                    }
                    else
                    {
                        ProductStockPriceDto pStockPriceDto = new()
                        {
                            ProductId = createdProduct.Id,
                            Price = 0,
                            BasePrice = 0,
                            MaximumSaleInOrder = 5,
                            StoreId = 20,
                            RepId = 1,
                            TaxId = 1
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

                    foreach (Category category in item.categories)
                    {
                        try
                        {
                            Domain.Models.Categury? categury = await _categuryRep.GetCateguryByName(category.name);
                            if (categury != null)
                            {
                                ProductCateguryDto productCategury = new()
                                {
                                    ProductId = createdProduct.Id, CateguryId = categury.GroupId
                                };
                                await _mediator.Send(new ProductCateguryCreateCommandReq(productCategury));
                            }
                        }
                        catch (Exception e)
                        {
                            resultWordpress r1 = new()
                            {
                                productId = item.id, type = "categury", error = e.InnerException.ToString()
                            };
                            resultWordpresses.Add(r1);
                        }
                    }

                    foreach (Attribute attr in item.attributes)
                    {
                        try
                        {
                            if (await _propertyRep.ExistProperty(attr.name))
                            {
                                Domain.Models.Property? property = await _propertyRep.GetByName(attr.name);
                                if (property != null)
                                {
                                    ProductPropertyDto productProperty = new()
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
                                PropertyDto property = new()
                                {
                                    Caption = attr.name, CateguryId = 47, PropertyCateguryId = 4
                                };
                                void newproperty = await _mediator.Send(new PropertyCreateCommandReq(property));
                                ProductPropertyDto productProperty = new()
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
                            resultWordpress r1 = new()
                            {
                                productId = item.id, type = "attribute", error = e.InnerException.ToString()
                            };
                            resultWordpresses.Add(r1);
                        }
                    }
                }
                catch (Exception e)
                {
                    resultWordpress r1 = new()
                    {
                        productId = item.id, type = "attribute", error = e.InnerException.ToString()
                    };
                    resultWordpresses.Add(r1);
                }
            }
        }
        catch (Exception e)
        {
            resultWordpress res = new() { productId = 0, type = "base", error = e.InnerException.ToString() };
            resultWordpresses.Add(res);
        }

        return resultWordpresses;
    }
}