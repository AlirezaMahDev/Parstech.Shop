using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record IntegratedProductsPagingQueryReq(ProductSearchParameterDto parameters, string userName)
    : IRequest<ProductPageingDto>;