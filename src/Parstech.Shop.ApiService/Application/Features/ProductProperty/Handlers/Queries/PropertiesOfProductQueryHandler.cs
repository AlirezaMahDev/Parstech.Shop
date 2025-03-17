using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Handlers.Queries;

public class PropertiesOfProductQueryHandler : IRequestHandler<PropertiesOfProductQueryReq, List<ProductPropertyDto>>
{
    private readonly IProductPropertyRepository _productPropertyRep;
    private readonly IPropertyRepository _propertyRep;
    private readonly IMapper _mapper;

    public PropertiesOfProductQueryHandler(IProductPropertyRepository productPropertyRep,
        IPropertyRepository propertyRep,
        IMapper mapper)
    {
        _productPropertyRep = productPropertyRep;
        _propertyRep = propertyRep;
        _mapper = mapper;
    }

    public async Task<List<ProductPropertyDto>> Handle(PropertiesOfProductQueryReq request,
        CancellationToken cancellationToken)
    {
        List<Shared.Models.ProductProperty> list = await _productPropertyRep.GetPropertiesByProduct(request.productId);
        List<ProductPropertyDto> Result = new();
        foreach (Shared.Models.ProductProperty productProperty in list)
        {
            Shared.Models.Property? property = await _propertyRep.GetAsync(productProperty.PropertyId);
            ProductPropertyDto? producPropertyDto = _mapper.Map<ProductPropertyDto>(productProperty);
            producPropertyDto.PropertyName = property.Caption;
            Result.Add(producPropertyDto);
        }

        return Result;
    }
}