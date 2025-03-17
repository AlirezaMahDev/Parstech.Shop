using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

public record TorobGetProductsQueryReq(int page) : IRequest<List<TorobProductDto>>;

public record TorobGetProductQueryReq(int productId, string url) : IRequest<TorobDto>;