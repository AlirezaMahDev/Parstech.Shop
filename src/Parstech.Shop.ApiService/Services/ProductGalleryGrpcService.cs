using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.ProductGallery;
using Shop.Application.Features.Product.Requests.Queries;

namespace Shop.ApiService.Services
{
    public class ProductGalleryGrpcService : ProductGalleryService.ProductGalleryServiceBase
    {
        private readonly IMediator _mediator;
        
        public ProductGalleryGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public override async Task<ProductGalleryResponse> GetProductGalleries(ProductGalleryRequest request, ServerCallContext context)
        {
            try
            {
                var galleries = await _mediator.Send(new ProductGalleriesQueryReq(request.ProductId));
                
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
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
} 