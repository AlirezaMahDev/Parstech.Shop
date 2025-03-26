using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

public class AddVariationForProductQueryHanlder : IRequestHandler<AddVariationForProductQueryReq, bool>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockPriceRep;
    public AddVariationForProductQueryHanlder(IProductRepository productRep, IProductStockPriceRepository productStockPriceRep, IMapper mapper)
    {
        _mapper = mapper;
        _productRep = productRep;
        _productStockPriceRep = productStockPriceRep;
    }
    public async Task<bool> Handle(AddVariationForProductQueryReq request, CancellationToken cancellationToken)
    {

        var product = await _productRep.GetAsync(request.productId);
            
        ProductDto newProduct = new()
        {
            BrandId=product.BrandId,
            ShortLink=product.ShortLink,
            Score=product.Score,
            CreateDate=DateTime.Now,
            TaxId=product.TaxId,
            Description=product.Description,
            ShortDescription=product.ShortDescription,
            Name = $"{product.Name}-{request.variationName}",
            ParentId = request.productId,
            TypeId = 3,
            Code = product.Code,
            VariationName = request.variationName,
        };
        var np=_mapper.Map<Domain.Models.Product>(newProduct);
        var res = await _productRep.AddAsync(np);

        //ProductDto productDto = new ProductDto()
        //{

        //};

        var parentStocks = await _productStockPriceRep.GetAllByProductId(request.productId);
        if (parentStocks.Count > 0)
        {
            foreach (var Stock in parentStocks)
            {
                Stock.Id = 0;
                Stock.ProductId = res.Id;
                Stock.SalePrice = 0;
                Stock.DiscountPrice = 0;
                Stock.Quantity = 0;
                await _productStockPriceRep.AddAsync(Stock);
            }
        }

        return true;

    }
}