using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.Features.ProductProperty.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductProperty.Handlers.Queries
{
    public class BasePropertiesOfProductQueryHandler : IRequestHandler<BasePropertiesOfProductQueryReq, List<BaseProductPropertyDto>>
    {
        private readonly IProductPropertyRepository _productPropertyRep;
        private readonly IPropertyRepository _propertyRep;
        private readonly IPropertyCateguryRepository _propertCactrguryRep;
        private readonly IMapper _mapper;
        public BasePropertiesOfProductQueryHandler(IProductPropertyRepository productPropertyRep,
            IPropertyRepository propertyRep, IPropertyCateguryRepository propertCactrguryRep, IMapper mapper)
        {
            _productPropertyRep = productPropertyRep;
            _propertCactrguryRep= propertCactrguryRep;
            _propertyRep = propertyRep;
            _mapper = mapper;
           
        }
        public async Task<List<BaseProductPropertyDto>> Handle(BasePropertiesOfProductQueryReq request, CancellationToken cancellationToken)
        {
            var properties = await _productPropertyRep.GetPropertiesByProduct(request.productId);
            var BaseProperties = new List<BaseProductPropertyDto>();
            foreach (var property in properties)
            {
                var propDto = _mapper.Map<ProductPropertyDto>(property);
                var Currentproperty = await _propertyRep.GetAsync(property.PropertyId);
                propDto.PropertyName = Currentproperty.Caption;
                var categury = await _propertCactrguryRep.GetAsync(Currentproperty.PropertyCateguryId);

                if (BaseProperties.Any(u => u.PropertyCategury == categury.Name))
                {
                    var current = BaseProperties.FirstOrDefault(u => u.PropertyCategury == categury.Name);
                    current.Properties.Add(propDto);
                }
                else
                {
                    BaseProductPropertyDto newBase = new BaseProductPropertyDto()
                    {
                        Id = categury.Id,
                        PropertyCategury = categury.Name,

                    };
                    newBase.Properties = new List<ProductPropertyDto>();
                    newBase.Properties.Add(propDto);
                    BaseProperties.Add(newBase);
                }
            }
            return BaseProperties;
        }
    }
}
