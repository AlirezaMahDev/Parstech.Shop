using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;

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
        Domain.Models.ProductGallery? pgallery = _mapper.Map<Domain.Models.ProductGallery>(request.ProductGalleryDto);
        Domain.Models.ProductGallery? result = await _productGallleryRep.UpdateAsync(pgallery);
        return _mapper.Map<ProductGalleryDto>(result);
    }
}