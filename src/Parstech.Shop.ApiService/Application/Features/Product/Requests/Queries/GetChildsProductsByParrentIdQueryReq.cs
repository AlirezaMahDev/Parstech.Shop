using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record GetChildsProductsByParrentIdQueryReq(int parrentId) : IRequest<List<ProductDto>>;