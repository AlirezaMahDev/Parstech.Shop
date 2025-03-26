using MediatR;

using Parstech.Shop.Context.Application.DTOs.Api;

namespace Parstech.Shop.Context.Application.Features.Api.Requests.Queries;

public record GetCateguryFromWordpressQueryReq(int page):IRequest<List<resultWordpress>>;