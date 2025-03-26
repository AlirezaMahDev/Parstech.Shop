using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;


namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record IntegratedProductsPagingQueryReq(ProductSearchParameterDto parameters,string userName) :IRequest<ProductPageingDto>;