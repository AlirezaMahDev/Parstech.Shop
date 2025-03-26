using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;
using Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductGallery.Handlers.Queries;

public class GalleryOfProductQueryHandler : IRequestHandler<GalleryOfProductQueryReq, ProductGalleryDto>
{
    private readonly IProductGallleryRepository _productGallleryRep;
    private readonly IMapper _mapper;

    public GalleryOfProductQueryHandler(IProductGallleryRepository productGallleryRep, IMapper mapper)
    {
        _productGallleryRep = productGallleryRep;
        _mapper = mapper;
    }
    public async Task<ProductGalleryDto> Handle(GalleryOfProductQueryReq request, CancellationToken cancellationToken)
    {
        var result =await _productGallleryRep.GetGalleryByProduct(request.productId);
        return _mapper.Map<ProductGalleryDto>(result);
    }
}