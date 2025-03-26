using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Commands;

public record ChangeMainGalleryCommandReq(int galleryId,int productId):IRequest<ResponseDto>;