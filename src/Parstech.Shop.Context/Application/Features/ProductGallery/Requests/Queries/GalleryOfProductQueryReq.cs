using MediatR;

using Parstech.Shop.Context.Application.DTOs.ProductGallery;

namespace Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Queries;

public record GalleryOfProductQueryReq(int productId) : IRequest<ProductGalleryDto>;