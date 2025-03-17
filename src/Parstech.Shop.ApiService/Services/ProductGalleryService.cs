using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Queries;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Services;

public class ProductGalleryService : ProductGalleryServiceBase
{
    private readonly IMediator _mediator;
    private readonly IProductGalleryRepository _galleryRepository;

    public ProductGalleryService(IMediator mediator, IProductGalleryRepository galleryRepository)
    {
        _mediator = mediator;
        _galleryRepository = galleryRepository;
    }

    public override async Task<ProductGalleryResponse> GetProductGalleries(ProductGalleryRequest request,
        ServerCallContext context)
    {
        try
        {
            void galleries = await _mediator.Send(new GalleriesOfProductQueryReq(request.ProductId));

            var response = new ProductGalleryResponse();
            response.Galleries.AddRange(galleries.Select(g => new ProductGallery
            {
                Id = g.Id,
                ProductId = g.ProductId,
                ImageName = g.ImageName,
                Alt = g.Alt,
                IsMain = g.IsMain
            }));

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}