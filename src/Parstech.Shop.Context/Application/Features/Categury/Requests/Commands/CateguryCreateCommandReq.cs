using MediatR;
using Parstech.Shop.Context.Application.DTOs.Categury;

namespace Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;

public record CateguryCreateCommandReq(CateguryDto CateguryDto) : IRequest<CateguryDto>;