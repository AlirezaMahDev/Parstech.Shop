using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;

public record ProductGalleryDeleteCommandReq(int id) : IRequest<Unit>;