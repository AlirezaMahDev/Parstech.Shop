using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.ProductProperty.Requests.Commands;

namespace Shop.Application.Features.ProductProperty.Handlers.Commands
{
    public class ProductPropertyCreateCommandHandler : IRequestHandler<ProductPropertyCreateCommandReq, ResponseDto>
    {
        private readonly IProductPropertyRepository _productPropertyRep;
        private readonly IMapper _mapper;

        public ProductPropertyCreateCommandHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
        {
            _productPropertyRep = productPropertyRep;
            _mapper = mapper;
        }
        public async Task<ResponseDto> Handle(ProductPropertyCreateCommandReq request, CancellationToken cancellationToken)
        {
            ResponseDto result=new ResponseDto();
            var PProperty = _mapper.Map<Domain.Models.ProductProperty>(request.ProductPropertyDto);
            if(await _productPropertyRep.ExistPropertyForProduct(request.ProductPropertyDto.ProductId,request.ProductPropertyDto.PropertyId))
            {
                result.IsSuccessed = false;
                result.Message = "ویژگی قبلا به محصول اضافه شده است";
                result.Object = _mapper.Map<ProductPropertyDto>(PProperty);
            }
            else
            {
                result.IsSuccessed = true;
                await _productPropertyRep.AddAsync(PProperty);
                result.Object = _mapper.Map<ProductPropertyDto>(PProperty);
            }
           
            return result;
        }
    }
}
