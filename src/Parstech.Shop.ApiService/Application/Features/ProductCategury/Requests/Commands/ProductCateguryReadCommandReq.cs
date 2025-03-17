using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;

public record ProductCateguryReadCommandReq(int id) : IRequest<ProductCateguryDto>;