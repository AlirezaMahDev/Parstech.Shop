using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Handlers.Commands;

public class ProductGalleryReadCommandHandler : IRequestHandler<ProductGalleryReadCommandReq, ProductGalleryDto>
{
    private readonly IProductGallleryRepository _productGallleryRep;
    private readonly IMapper _mapper;

    public ProductGalleryReadCommandHandler(IProductGallleryRepository productGallleryRep, IMapper mapper)
    {
        _productGallleryRep = productGallleryRep;
        _mapper = mapper;
    }

    public async Task<ProductGalleryDto> Handle(ProductGalleryReadCommandReq request,
        CancellationToken cancellationToken)
    {
        Domain.Models.ProductGallery? pgallery = await _productGallleryRep.GetAsync(request.id);
        return _mapper.Map<ProductGalleryDto>(pgallery);
    }
}