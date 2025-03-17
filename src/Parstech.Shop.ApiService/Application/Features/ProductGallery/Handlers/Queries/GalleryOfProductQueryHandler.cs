using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Handlers.Queries;

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
        Shared.Models.ProductGallery? result = await _productGallleryRep.GetGalleryByProduct(request.productId);
        return _mapper.Map<ProductGalleryDto>(result);
    }
}