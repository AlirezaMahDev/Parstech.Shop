using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Handlers.Queries;

public class
    BasePropertiesOfProductQueryHandler : IRequestHandler<BasePropertiesOfProductQueryReq, List<BaseProductPropertyDto>>
{
    private readonly IProductPropertyRepository _productPropertyRep;
    private readonly IPropertyRepository _propertyRep;
    private readonly IPropertyCateguryRepository _propertCactrguryRep;
    private readonly IMapper _mapper;

    public BasePropertiesOfProductQueryHandler(IProductPropertyRepository productPropertyRep,
        IPropertyRepository propertyRep,
        IPropertyCateguryRepository propertCactrguryRep,
        IMapper mapper)
    {
        _productPropertyRep = productPropertyRep;
        _propertCactrguryRep = propertCactrguryRep;
        _propertyRep = propertyRep;
        _mapper = mapper;
    }

    public async Task<List<BaseProductPropertyDto>> Handle(BasePropertiesOfProductQueryReq request,
        CancellationToken cancellationToken)
    {
        List<Shared.Models.ProductProperty> properties =
            await _productPropertyRep.GetPropertiesByProduct(request.productId);
        List<BaseProductPropertyDto> BaseProperties = new();
        foreach (Shared.Models.ProductProperty property in properties)
        {
            ProductPropertyDto? propDto = _mapper.Map<ProductPropertyDto>(property);
            Shared.Models.Property? Currentproperty = await _propertyRep.GetAsync(property.PropertyId);
            propDto.PropertyName = Currentproperty.Caption;
            Shared.Models.PropertyCategury? categury =
                await _propertCactrguryRep.GetAsync(Currentproperty.PropertyCateguryId);

            if (BaseProperties.Any(u => u.PropertyCategury == categury.Name))
            {
                BaseProductPropertyDto? current =
                    BaseProperties.FirstOrDefault(u => u.PropertyCategury == categury.Name);
                current.Properties.Add(propDto);
            }
            else
            {
                BaseProductPropertyDto newBase = new() { Id = categury.Id, PropertyCategury = categury.Name };
                newBase.Properties = new();
                newBase.Properties.Add(propDto);
                BaseProperties.Add(newBase);
            }
        }

        return BaseProperties;
    }
}