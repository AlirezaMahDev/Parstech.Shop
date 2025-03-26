using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Commands;

public record ProductPropertyCreateCommandReq(ProductPropertyDto ProductPropertyDto) : IRequest<ResponseDto>;