using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

public record GetCateguryFromWordpressQueryReq(int page) : IRequest<List<resultWordpress>>;