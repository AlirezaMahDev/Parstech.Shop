using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Handlers.Commands;

public class ChangeMainGalleryCommandHandler : IRequestHandler<ChangeMainGalleryCommandReq, ResponseDto>
{
    private readonly IProductRepository _productRep;
    private readonly IProductGallleryRepository _productGallleryRep;
    private readonly IMapper _mapper;

    public ChangeMainGalleryCommandHandler(IProductGallleryRepository productGallleryRep,
        IProductRepository productRep,
        IMapper mapper)
    {
        _productGallleryRep = productGallleryRep;
        _productRep = productRep;
        _mapper = mapper;
    }

    public async Task<ResponseDto> Handle(ChangeMainGalleryCommandReq request, CancellationToken cancellationToken)
    {
        List<Shared.Models.ProductGallery> galleries =
            await _productGallleryRep.GetGalleriesByProduct(request.productId);
        foreach (Shared.Models.ProductGallery gallery in galleries)
        {
            if (gallery.Id != request.galleryId)
            {
                if (gallery.IsMain)
                {
                    gallery.IsMain = false;
                    await _productGallleryRep.UpdateAsync(gallery);
                }
            }
            else
            {
                if (!gallery.IsMain)
                {
                    gallery.IsMain = true;
                    await _productGallleryRep.UpdateAsync(gallery);
                }
            }
        }

        ResponseDto Response = new();
        Response.IsSuccessed = true;
        Response.Message = "عملیات با موفقیت انجام شد";
        return Response;
    }
}