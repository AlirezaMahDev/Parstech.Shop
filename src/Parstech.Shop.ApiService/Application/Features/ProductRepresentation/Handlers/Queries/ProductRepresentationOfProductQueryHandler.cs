using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Handlers.Queries;

public class
    ProductRepresentationOfProductQueryHandler : IRequestHandler<ProductRepresentationOfProductQueryReq,
    ProductRepresentationDto>
{
    private readonly IProductRepresesntationRepository _productRepresentationRep;
    private readonly IMapper _mapper;

    public ProductRepresentationOfProductQueryHandler(IProductRepresesntationRepository productRepresentationRep,
        IMapper mapper)
    {
        _productRepresentationRep = productRepresentationRep;
        _mapper = mapper;
    }

    public async Task<ProductRepresentationDto> Handle(ProductRepresentationOfProductQueryReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.ProductRepresentation productRep =
            await _productRepresentationRep.GetProductRepresentationOfProduct(request.productId);
        return _mapper.Map<ProductRepresentationDto>(productRep);
    }
}