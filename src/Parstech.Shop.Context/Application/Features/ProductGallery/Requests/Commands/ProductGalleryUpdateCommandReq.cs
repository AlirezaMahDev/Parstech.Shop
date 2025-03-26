using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;

namespace Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Commands;

public record ProductGalleryUpdateCommandReq(ProductGalleryDto ProductGalleryDto) : IRequest<ProductGalleryDto>;