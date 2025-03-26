using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;
using Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductProperty.Handlers.Commands;

public class ProductPropertyReadCommandHandler : IRequestHandler<ProductPropertyReadCommandReq, ProductPropertyDto>
{
    private readonly IProductPropertyRepository _productPropertyRep;
    private readonly IMapper _mapper;

    public ProductPropertyReadCommandHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
    {
        _productPropertyRep = productPropertyRep;
        _mapper = mapper;
    }
    public async Task<ProductPropertyDto> Handle(ProductPropertyReadCommandReq request, CancellationToken cancellationToken)
    {
        var pproperty = await _productPropertyRep.GetAsync(request.id);
        return _mapper.Map<ProductPropertyDto>(pproperty);
    }
}