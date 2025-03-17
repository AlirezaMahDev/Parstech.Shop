using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;

public record ProductCateguryUpdateCommandReq(ProductCateguryDto ProductCateguryDto) : IRequest<ProductCateguryDto>;