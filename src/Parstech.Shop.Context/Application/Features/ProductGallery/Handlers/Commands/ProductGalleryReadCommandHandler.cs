using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;
using Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductGallery.Handlers.Commands;

public class ProductGalleryReadCommandHandler : IRequestHandler<ProductGalleryReadCommandReq, ProductGalleryDto>
{
    private readonly IProductGallleryRepository _productGallleryRep;
    private readonly IMapper _mapper;

    public ProductGalleryReadCommandHandler(IProductGallleryRepository productGallleryRep, IMapper mapper)
    {
        _productGallleryRep = productGallleryRep;
        _mapper = mapper;
    }
    public async Task<ProductGalleryDto> Handle(ProductGalleryReadCommandReq request, CancellationToken cancellationToken)
    {
        var pgallery =await _productGallleryRep.GetAsync(request.id);
        return _mapper.Map<ProductGalleryDto>(pgallery);
    }
}