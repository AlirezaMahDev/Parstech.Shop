using MediatR;

using Parstech.Shop.Context.Application.DTOs.Api;

namespace Parstech.Shop.Context.Application.Features.Api.Requests.Queries;

public record TorobGetProductsQueryReq(int page):IRequest<List<TorobProductDto>>;
public record TorobGetProductQueryReq(int productId,string url):IRequest<TorobDto>;