using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;
using Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductProperty.Handlers.Commands;

public class ProductPropertyUpdateCommandHandler : IRequestHandler<ProductPropertyUpdateCommandReq, ProductPropertyDto>
{
    private readonly IProductPropertyRepository _productPropertyRep;
    private readonly IMapper _mapper;

    public ProductPropertyUpdateCommandHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
    {
        _productPropertyRep = productPropertyRep;
        _mapper = mapper;
    }
    public async Task<ProductPropertyDto> Handle(ProductPropertyUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var pproperty = _mapper.Map<Domain.Models.ProductProperty>(request.ProductPropertyDto);
        var result=await _productPropertyRep.UpdateAsync(pproperty);
        return _mapper.Map<ProductPropertyDto>(result);
    }
}