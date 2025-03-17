using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;

public record ChangeMainGalleryCommandReq(int galleryId, int productId) : IRequest<ResponseDto>;