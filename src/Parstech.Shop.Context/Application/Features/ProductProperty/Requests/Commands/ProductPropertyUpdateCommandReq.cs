using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;

namespace Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Commands;

public record ProductPropertyUpdateCommandReq(ProductPropertyDto ProductPropertyDto) : IRequest<ProductPropertyDto>;