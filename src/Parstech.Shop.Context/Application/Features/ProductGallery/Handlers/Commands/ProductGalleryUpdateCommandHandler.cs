using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;
using Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductGallery.Handlers.Commands;

public class ProductGalleryUpdateCommandHandler : IRequestHandler<ProductGalleryUpdateCommandReq, ProductGalleryDto>
{
    private readonly IProductGallleryRepository _productGallleryRep;
    private readonly IMapper _mapper;

    public ProductGalleryUpdateCommandHandler(IProductGallleryRepository productGallleryRep, IMapper mapper)
    {
        _productGallleryRep = productGallleryRep;
        _mapper = mapper;
    }
    public async Task<ProductGalleryDto> Handle(ProductGalleryUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var pgallery = _mapper.Map<Domain.Models.ProductGallery>(request.ProductGalleryDto);
        var result =await _productGallleryRep.UpdateAsync(pgallery);
        return _mapper.Map<ProductGalleryDto>(result);
    }
}