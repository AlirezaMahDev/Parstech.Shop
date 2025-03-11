using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.Features.ProductProperty.Requests.Queries;

namespace Shop.Application.Features.ProductProperty.Handlers.Queries
{
    public class PropertiesOfProductQueryHandler : IRequestHandler<PropertiesOfProductQueryReq, List<ProductPropertyDto>>
    {
        private readonly IProductPropertyRepository _productPropertyRep;
        private readonly IPropertyRepository _propertyRep;
        private readonly IMapper _mapper;

        public PropertiesOfProductQueryHandler(IProductPropertyRepository productPropertyRep, IPropertyRepository propertyRep, IMapper mapper)
        {
            _productPropertyRep = productPropertyRep;
            _propertyRep = propertyRep;
            _mapper = mapper;
        }
        public async Task<List<ProductPropertyDto>> Handle(PropertiesOfProductQueryReq request, CancellationToken cancellationToken)
        {
            var list =await _productPropertyRep.GetPropertiesByProduct(request.productId);
            List<ProductPropertyDto> Result = new List<ProductPropertyDto>();
            foreach (var productProperty in list)
            {
                var property =await _propertyRep.GetAsync(productProperty.PropertyId);
                var producPropertyDto = _mapper.Map<ProductPropertyDto>(productProperty);
                producPropertyDto.PropertyName = property.Caption;
                Result.Add(producPropertyDto);
            }
            return Result;
        }
    }
}
