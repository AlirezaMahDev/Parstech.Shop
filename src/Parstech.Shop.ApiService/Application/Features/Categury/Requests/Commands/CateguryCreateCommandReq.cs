using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Requests.Commands;

public record CateguryCreateCommandReq(CateguryDto CateguryDto) : IRequest<CateguryDto>;