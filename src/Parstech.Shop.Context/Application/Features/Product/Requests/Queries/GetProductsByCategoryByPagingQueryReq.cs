using MediatR;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record GetProductsByCategoryByPagingQueryReq(ParameterDto Parameter, int productTypeId, int categoryId) : IRequest<ProductPageingDto>;