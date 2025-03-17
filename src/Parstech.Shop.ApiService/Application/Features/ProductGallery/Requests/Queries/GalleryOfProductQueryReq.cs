using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Queries;

public record GalleryOfProductQueryReq(int productId) : IRequest<ProductGalleryDto>;