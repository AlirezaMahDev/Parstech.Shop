using MediatR;
using Parstech.Shop.Context.Application.DTOs.Categury;

namespace Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

public record ShowCateguriesQueryReq():IRequest<List<ParrentCateguryShowDto>>;