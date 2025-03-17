using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record GetChildsProductsByParrentIdQueryReq(int parrentId) : IRequest<List<ProductDto>>;