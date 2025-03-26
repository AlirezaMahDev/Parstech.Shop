using AutoMapper;

using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductGallery.Handlers.Commands;

public class ChangeMainGalleryCommandHandler : IRequestHandler<ChangeMainGalleryCommandReq, ResponseDto>
{
    private readonly IProductRepository _productRep;
    private readonly IProductGallleryRepository _productGallleryRep;
    private readonly IMapper _mapper;

    public ChangeMainGalleryCommandHandler(IProductGallleryRepository productGallleryRep,
        IProductRepository productRep, IMapper mapper)
    {
        _productGallleryRep = productGallleryRep;
        _productRep = productRep;
        _mapper = mapper;
    }
    public async Task<ResponseDto> Handle(ChangeMainGalleryCommandReq request, CancellationToken cancellationToken)
    {
        var galleries = await _productGallleryRep.GetGalleriesByProduct(request.productId);
        foreach (var gallery in galleries)
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
        ResponseDto Response=new();
        Response.IsSuccessed = true;
        Response.Message = "عملیات با موفقیت انجام شد";
        return Response;
    }
}