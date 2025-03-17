using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Queries;

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
        Domain.Models.ProductGallery? result = await _productGallleryRep.GetGalleryByProduct(request.productId);
        return _mapper.Map<ProductGalleryDto>(result);
    }
}