using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Handlers.Queries;

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
        Shared.Models.ProductProperty? pproperty = await _productPropertyRep.GetpropertyByProduct(request.productId);
        return _mapper.Map<ProductPropertyDto>(pproperty);
    }
}