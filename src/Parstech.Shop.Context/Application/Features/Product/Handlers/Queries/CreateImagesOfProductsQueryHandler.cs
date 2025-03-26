using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

public class CreateImagesOfProductsQueryHandler : IRequestHandler<CreateImagesOfProductsQueryReq, Unit>
{
    private readonly IProductRepository _productRep;
    private readonly IProductGallleryRepository _productGalleryRep;
    public CreateImagesOfProductsQueryHandler(IProductRepository productRep, IProductGallleryRepository productGalleryRep)
    {
        _productRep = productRep;
        _productGalleryRep = productGalleryRep;
    }
    public async Task<Unit> Handle(CreateImagesOfProductsQueryReq request, CancellationToken cancellationToken)
    {
        var list =await _productRep.GetAll();
        foreach (var item in list)
        {
            Domain.Models.ProductGallery gallery=new();
            if(await _productRep.IsChild(item.Id))
            {
                var parrent=await _productRep.GetAsync(item.ParentId.Value);
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