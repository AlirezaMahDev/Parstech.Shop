using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Handlers.Queries;

public class GalleriesOfProductQueryHandler : IRequestHandler<GalleriesOfProductQueryReq, List<ProductGalleryDto>>
{
    private readonly IProductRepository _productRep;
    private readonly IProductGallleryRepository _productGallleryRep;
    private readonly IMapper _mapper;

    public GalleriesOfProductQueryHandler(IProductGallleryRepository productGallleryRep,
        IProductRepository productRep,
        IMapper mapper)
    {
        _productGallleryRep = productGallleryRep;
        _productRep = productRep;
        _mapper = mapper;
    }

    public async Task<List<ProductGalleryDto>> Handle(GalleriesOfProductQueryReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.Product? product = await _productRep.GetAsync(request.productId);
        if (product.TypeId == 3)
        {
            Shared.Models.Product? parrent = await _productRep.GetAsync(product.ParentId.Value);
            List<Shared.Models.ProductGallery> result = await _productGallleryRep.GetGalleriesByProduct(parrent.Id);
            return _mapper.Map<List<ProductGalleryDto>>(result);
        }
        else
        {
            List<Shared.Models.ProductGallery> result =
                await _productGallleryRep.GetGalleriesByProduct(request.productId);
            return _mapper.Map<List<ProductGalleryDto>>(result);
        }
    }
}