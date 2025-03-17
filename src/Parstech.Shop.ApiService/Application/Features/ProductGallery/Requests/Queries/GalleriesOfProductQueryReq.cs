using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Queries;

public record GalleriesOfProductQueryReq(int productId) : IRequest<List<ProductGalleryDto>>;