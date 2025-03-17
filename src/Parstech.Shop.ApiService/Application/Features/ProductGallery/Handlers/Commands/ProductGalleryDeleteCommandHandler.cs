using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Handlers.Commands;

public class ProductGalleryDeleteCommandHandler : IRequestHandler<ProductGalleryDeleteCommandReq, Unit>
{
    private readonly IProductGallleryRepository _productGalleryRep;
    private readonly IMapper _mapper;

    public ProductGalleryDeleteCommandHandler(IProductGallleryRepository productGalleryRep, IMapper mapper)
    {
        _productGalleryRep = productGalleryRep;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(ProductGalleryDeleteCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.ProductGallery? pgallery = await _productGalleryRep.GetAsync(request.id);

        string tempFile = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot/Shared/Images/Products",
            pgallery.ImageName);
        using (FileStream fs = new(tempFile, FileMode.Open)) { }

        try
        {
            File.Delete(tempFile);
        }
        catch (Exception e)
        {
        }

        await _productGalleryRep.DeleteAsync(pgallery);
        return Unit.Value;
    }
}