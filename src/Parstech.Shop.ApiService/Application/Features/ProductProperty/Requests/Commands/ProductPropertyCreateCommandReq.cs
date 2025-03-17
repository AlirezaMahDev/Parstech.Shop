using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Commands;

public record ProductPropertyCreateCommandReq(ProductPropertyDto ProductPropertyDto) : IRequest<ResponseDto>;