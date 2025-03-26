using MediatR;
using Parstech.Shop.Context.Application.DTOs.Categury;

namespace Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

public record GetCateguryByLatinameQueryReq(string latinName):IRequest<CateguryDto>;