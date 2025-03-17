using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Commands;

public record ProductPropertyCreateCommandReq(ProductPropertyDto ProductPropertyDto) : IRequest<ResponseDto>;