using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;

public record CategurySelectListQueryReq() : IRequest<List<CategurySelectDto>>;