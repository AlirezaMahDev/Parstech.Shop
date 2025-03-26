using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Commands;

public record ProductGalleryMultipleCreateCommandReq(UploadViewModel items,int productId) :IRequest<ResponseDto>;