using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

public record GetCateguryFromWordpressQueryReq(int page) : IRequest<List<resultWordpress>>;