using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class CreateImagesOfProductsQueryHandler : IRequestHandler<CreateImagesOfProductsQueryReq, Unit>
{
    private readonly IProductRepository _productRep;
    private readonly IProductGallleryRepository _productGalleryRep;

    public CreateImagesOfProductsQueryHandler(IProductRepository productRep,
        IProductGallleryRepository productGalleryRep)
    {
        _productRep = productRep;
        _productGalleryRep = productGalleryRep;
    }

    public async Task<Unit> Handle(CreateImagesOfProductsQueryReq request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Shared.Models.Product> list = await _productRep.GetAll();
        foreach (Shared.Models.Product item in list)
        {
            Shared.Models.ProductGallery gallery = new();
            if (await _productRep.IsChild(item.Id))
            {
                Shared.Models.Product? parrent = await _productRep.GetAsync(item.ParentId.Value);
                gallery.IsMain = true;
                gallery.ProductId = item.Id;
                gallery.ImageName = $"{parrent.Code}.jpg";
                gallery.Alt = item.Name;
            }
            else
            {
                gallery.IsMain = true;
                gallery.ProductId = item.Id;
                gallery.ImageName = $"{item.Code}.jpg";
                gallery.Alt = item.Name;
            }

            await _productGalleryRep.AddAsync(gallery);
        }

        return Unit.Value;
    }
}