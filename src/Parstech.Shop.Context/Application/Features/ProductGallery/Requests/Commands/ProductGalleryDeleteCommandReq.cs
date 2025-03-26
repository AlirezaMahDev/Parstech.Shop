using MediatR;

namespace Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Commands;

public record ProductGalleryDeleteCommandReq(int id) : IRequest<Unit>;