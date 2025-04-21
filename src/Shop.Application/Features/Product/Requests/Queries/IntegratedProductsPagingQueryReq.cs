using MediatR;
using Shop.Application.DTOs.Product;


namespace Shop.Application.Features.Product.Requests.Queries
{
    public record IntegratedProductsPagingQueryReq(ProductSearchParameterDto parameters,string userName) :IRequest<ProductPageingDto>;
    
}
