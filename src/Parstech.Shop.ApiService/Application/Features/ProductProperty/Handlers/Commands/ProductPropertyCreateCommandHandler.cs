using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Handlers.Commands;

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
        ResponseDto result = new();
        Domain.Models.ProductProperty? PProperty =
            _mapper.Map<Domain.Models.ProductProperty>(request.ProductPropertyDto);
        if (await _productPropertyRep.ExistPropertyForProduct(request.ProductPropertyDto.ProductId,
                request.ProductPropertyDto.PropertyId))
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