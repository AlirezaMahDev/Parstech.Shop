using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record GetLatestProductsQueryReq(int take, int productTypeId) : IRequest<List<ProductListShowDto>>;