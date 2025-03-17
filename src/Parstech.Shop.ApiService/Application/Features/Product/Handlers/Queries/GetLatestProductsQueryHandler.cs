using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class GetLatestProductsQueryHandler : IRequestHandler<GetLatestProductsQueryReq, List<ProductListShowDto>>
{
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly IMapper _mapper;
    private readonly IProductGallleryRepository _productGallleryRep;

    public GetLatestProductsQueryHandler(IProductRepository productRep,
        IMapper mapper,
        IProductGallleryRepository productGallleryRep,
        IProductStockPriceRepository productStockRep)
    {
        _productRep = productRep;
        _mapper = mapper;
        _productGallleryRep = productGallleryRep;
        _productStockRep = productStockRep;
    }

    public async Task<List<ProductListShowDto>> Handle(GetLatestProductsQueryReq request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Shared.Models.Product> allProducts = await _productRep.GetAll();
        IEnumerable<Shared.Models.Product> products =
            allProducts.Where(z => z.TypeId == request.productTypeId && z.IsActive);
        List<ProductListShowDto> productDto = new();
        foreach (Shared.Models.Product product in products)
        {
            ProductListShowDto x = new();
            x = _mapper.Map<ProductListShowDto>(product);
            //x.DiscountPrice = (product.Price - product.DiscountPrice);
            Shared.Models.ProductGallery? pic = await _productGallleryRep.GetMainImageOfProduct(product.Id);
            x.Image = pic.ImageName;

            productDto.Add(x);
        }

        return productDto.OrderByDescending(u => u.Id).Take(request.take).ToList();
    }
}