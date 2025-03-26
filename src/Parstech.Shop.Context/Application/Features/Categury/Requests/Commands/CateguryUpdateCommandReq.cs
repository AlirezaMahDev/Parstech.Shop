using MediatR;
using Parstech.Shop.Context.Application.DTOs.Categury;

namespace Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;

public record CateguryUpdateCommandReq(CateguryDto CateguryDto) : IRequest<CateguryDto>;