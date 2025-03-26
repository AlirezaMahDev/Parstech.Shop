using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;
using Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductProperty.Handlers.Queries;

public class PropertyOfProductQueryHandler : IRequestHandler<PropertyOfProductQueryReq, ProductPropertyDto>
{
    private readonly IProductPropertyRepository _productPropertyRep;
    private readonly IMapper _mapper;

    public PropertyOfProductQueryHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
    {
        _productPropertyRep = productPropertyRep;
        _mapper = mapper;
    }
    public async Task<ProductPropertyDto> Handle(PropertyOfProductQueryReq request, CancellationToken cancellationToken)
    {
        var pproperty =await _productPropertyRep.GetpropertyByProduct(request.productId);
        return _mapper.Map<ProductPropertyDto>(pproperty);
    }
}