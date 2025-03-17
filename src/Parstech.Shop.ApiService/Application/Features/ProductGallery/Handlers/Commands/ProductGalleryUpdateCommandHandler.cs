using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Handlers.Commands;

public class ProductGalleryUpdateCommandHandler : IRequestHandler<ProductGalleryUpdateCommandReq, ProductGalleryDto>
{
    private readonly IProductGallleryRepository _productGallleryRep;
    private readonly IMapper _mapper;

    public ProductGalleryUpdateCommandHandler(IProductGallleryRepository productGallleryRep, IMapper mapper)
    {
        _productGallleryRep = productGallleryRep;
        _mapper = mapper;
    }

    public async Task<ProductGalleryDto> Handle(ProductGalleryUpdateCommandReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.ProductGallery? pgallery = _mapper.Map<Shared.Models.ProductGallery>(request.ProductGalleryDto);
        Shared.Models.ProductGallery result = await _productGallleryRep.UpdateAsync(pgallery);
        return _mapper.Map<ProductGalleryDto>(result);
    }
}