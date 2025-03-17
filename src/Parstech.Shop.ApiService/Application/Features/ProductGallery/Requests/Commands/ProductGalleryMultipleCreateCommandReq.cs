using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;

public record ProductGalleryMultipleCreateCommandReq(UploadViewModel items, int productId) : IRequest<ResponseDto>;