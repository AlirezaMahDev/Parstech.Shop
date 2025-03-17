using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Services;

public class ProductGalleryGrpcService : ProductGalleryService.ProductGalleryServiceBase
{
    private readonly IMediator _mediator;

    public ProductGalleryGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<ProductGalleryResponse> GetProductGalleries(ProductGalleryRequest request,
        ServerCallContext context)
    {
        try
        {
            void galleries = await _mediator.Send(new ProductGalleriesQueryReq(request.ProductId));

            var response = new ProductGalleryResponse();
            foreach (var gallery in galleries)
            {
                response.Galleries.Add(new ProductGallery
                {
                    Id = gallery.Id,
                    ProductId = gallery.ProductId,
                    ImageName = gallery.ImageName ?? string.Empty,
                    Alt = gallery.Alt ?? string.Empty,
                    IsMain = gallery.IsMain
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}