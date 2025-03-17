using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;

public record ProductGalleryCreateCommandReq(ProductGalleryDto ProductGalleryDto) : IRequest<ResponseDto>;