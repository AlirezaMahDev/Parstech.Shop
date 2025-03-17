using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Queries;

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
        Domain.Models.Product? product = await _productRep.GetAsync(request.productId);
        if (product.TypeId == 3)
        {
            Domain.Models.Product? parrent = await _productRep.GetAsync(product.ParentId.Value);
            List<Domain.Models.ProductGallery>? result = await _productGallleryRep.GetGalleriesByProduct(parrent.Id);
            return _mapper.Map<List<ProductGalleryDto>>(result);
        }
        else
        {
            List<Domain.Models.ProductGallery>? result =
                await _productGallleryRep.GetGalleriesByProduct(request.productId);
            return _mapper.Map<List<ProductGalleryDto>>(result);
        }
    }
}