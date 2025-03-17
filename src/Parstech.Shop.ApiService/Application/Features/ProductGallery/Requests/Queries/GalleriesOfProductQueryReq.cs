using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Queries;

public record GalleriesOfProductQueryReq(int productId) : IRequest<List<ProductGalleryDto>>;