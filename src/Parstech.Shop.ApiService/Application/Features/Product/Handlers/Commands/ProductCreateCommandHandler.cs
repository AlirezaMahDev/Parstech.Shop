using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Commands;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandReq, ProductDto>
{
    private IProductRepository _productRep;
    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _mediator;

    public ProductCreateCommandHandler(IProductRepository productRep,
        IUserStoreRepository userStoreRep,
        IMapper mapper,
        IMediator mediator)
    {
        _productRep = productRep;
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ProductDto> Handle(ProductCreateCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.Product? product = _mapper.Map<Domain.Models.Product>(request.ProductDto);


        product.ShortLink = await _mediator.Send(new ProductShortLinkGeneratorQueryReq());
        //product.RepId = Store.RepId;
        product.CreateDate = DateTime.Now;
        product.IsActive = false;
        //product.Visit = 0;

        Domain.Models.Product? productResult = await _productRep.AddAsync(product);

        //ProductGalleryDto gallery = new ProductGalleryDto()
        //{
        //    ProductId = productResult.Id,
        //    ImageName = "logoBlack.png",

        //    IsMain = true,
        //    Alt = productResult.Name
        //};
        //await _mediator.Send(new ProductGalleryCreateCommandReq(gallery));

        //var Store =await _userStoreRep.GetAsync(request.ProductDto.StoreId);
        //ProductStockPriceDto productStock = new ProductStockPriceDto()
        //{
        //    ProductId= productResult.Id,
        //    RepId= Store.RepId,
        //    StoreId=request.ProductDto.StoreId,
        //    Price=0,
        //    SalePrice=0,
        //    DiscountPrice=0,
        //    BasePrice=0,
        //    StockStatus=false,
        //    Quantity=0,
        //    MaximumSaleInOrder=0,
        //    TaxId=1
        //};
        //await _mediator.Send(new ProductStockPriceCreateCommandReq(productStock));


        return _mapper.Map<ProductDto>(productResult);
    }
}

public class ProductWordpresCreateCommandHandler : IRequestHandler<ProductWordpressCreateCommandReq, ProductDto>
{
    private IProductRepository _productRep;
    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _mediator;

    public ProductWordpresCreateCommandHandler(IProductRepository productRep,
        IUserStoreRepository userStoreRep,
        IMapper mapper,
        IMediator mediator)
    {
        _productRep = productRep;
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ProductDto> Handle(ProductWordpressCreateCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.Product? product = _mapper.Map<Domain.Models.Product>(request.ProductDto);


        product.ShortLink = await _mediator.Send(new ProductShortLinkGeneratorQueryReq());
        //product.RepId = Store.RepId;
        product.CreateDate = DateTime.Now;
        product.Visit = request.ProductDto.Visit;
        product.Code = request.ProductDto.Visit.ToString();
        if (product.TypeId != 5)
        {
            product.SingleSale = true;
        }

        Domain.Models.Product? productResult = await _productRep.AddAsync(product);

        //ProductGalleryDto gallery = new ProductGalleryDto()
        //{
        //    ProductId = productResult.Id,
        //    ImageName = "logoBlack.png",

        //    IsMain = true,
        //    Alt = productResult.Name
        //};
        //await _mediator.Send(new ProductGalleryCreateCommandReq(gallery));

        //var Store = await _userStoreRep.GetAsync(request.ProductDto.StoreId);
        //ProductStockPriceDto productStock = new ProductStockPriceDto()
        //{
        //    ProductId = productResult.Id,
        //    RepId = Store.RepId,
        //    StoreId = request.ProductDto.StoreId,
        //    Price = 0,
        //    SalePrice = 0,
        //    DiscountPrice = 0,
        //    BasePrice = 0,
        //    StockStatus = false,
        //    Quantity = 0,
        //    MaximumSaleInOrder = 0,
        //    TaxId = 1
        //};
        //await _mediator.Send(new ProductStockPriceCreateCommandReq(productStock));


        return _mapper.Map<ProductDto>(productResult);
    }
}